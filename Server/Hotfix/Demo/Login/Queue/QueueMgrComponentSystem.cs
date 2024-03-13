namespace ET
{
    public class QueueMgrComponentAwakeSystem: AwakeSystem<QueueMgrComponent>
    {
        public override void Awake(QueueMgrComponent self)
        {
            // 启动放人计时器
            self.Timer_Tick = TimerComponent.Instance.NewRepeatedTimer(ConstValue.Queue_TickTime, TimerType.QueueTickTime, self);
            // 启动清空掉线保护时长计时器
            self.Timer_ClearProtect = TimerComponent.Instance.NewRepeatedTimer(ConstValue.Queue_ClearProtect, TimerType.QueueClearProtect, self);
            // 启动排队排名更新计时器
            self.Timer_Update = TimerComponent.Instance.NewRepeatedTimer(ConstValue.Queue_TickUpdate, TimerType.QueueUpdateTime, self);
        }
    }
    
    public class QueueMgrComponentDestroySystem: DestroySystem<QueueMgrComponent>
    {
        public override void Destroy(QueueMgrComponent self)
        {
            TimerComponent.Instance.Remove(ref self.Timer_Tick);
            TimerComponent.Instance.Remove(ref self.Timer_ClearProtect);
            TimerComponent.Instance.Remove(ref self.Timer_Update);
            
            self.Online.Clear();
            self.Protects.Clear();
            self.Queue.Clear();
        }
    }
    
    [Timer(TimerType.QueueTickTime)]
    public class QueueTickTime_TimerHandler : ATimer<QueueMgrComponent>
    {
        public override void Run(QueueMgrComponent t)
        {
            t.Tick();
        }
    }

    [Timer(TimerType.QueueClearProtect)]
    public class QueueClearProtect_TimerHandler: ATimer<QueueMgrComponent>
    {
        public override void Run(QueueMgrComponent t)
        {
            t.ClearProtect();
        }
    }

    [Timer(TimerType.QueueUpdateTime)]
    public class QueueUpdateTime_TimerHandler: ATimer<QueueMgrComponent>
    {
        public override void Run(QueueMgrComponent t)
        {
            t.UpdateQueue();
        }
    }

    /// <summary>
    /// 尝试进入排队服务器，返回true代表需要排队，false可以直接进入场景Map服务器
    /// </summary>
    [FriendClassAttribute(typeof(ET.QueueMgrComponent))]
    [FriendClassAttribute(typeof(ET.QueueInfo))]
    public static class QueueMgrComponentSystem
    {
        public static bool TryEnqueue(this QueueMgrComponent self, string account, long unitId, long gateActorId)
        {
            // 掉线保护状态中重新发送进入排队请求时，移除掉线保护状态
            if (self.Protects.ContainsKey(unitId))
            {
                self.Protects.Remove(unitId);

                // 排队时掉线
                if (self.Queue.ContainsKey(unitId))
                {
                    return true;
                }

                // 游戏中掉线
                return false;
            }

            // 玩家不在线
            if (self.Online.Contains(unitId))
            {
                return false;
            }

            // 已经在排队中 但重复发送消息
            if (self.Queue.ContainsKey(unitId))
            {
                return true;
            }

            self.Enqueue(account, unitId, gateActorId);
            
            return true;
        }

        public static void Enqueue(this QueueMgrComponent self, string account, long unitId, long gateActorId)
        {
            // 是否在排队状态中 在的话return
            if (self.Queue.ContainsKey(unitId))
            {
                return;
            }

            QueueInfo queueInfo = self.AddChild<QueueInfo>();
            queueInfo.Account = account;
            queueInfo.UnitId = unitId;
            queueInfo.GateActorId = gateActorId;
            queueInfo.Index = self.Queue.Count + 1;
            self.Queue.AddLast(unitId, queueInfo);
        }

        public static int GetIndex(this QueueMgrComponent self, long unitId)
        {
            return self.Queue[unitId]?.Index ?? 1;
        }

        public static void Tick(this QueueMgrComponent self)
        {
            // 满人
            if (self.Online.Count >= ConstValue.Queue_MaxOnline)
            {
                return;
            }

            // 没满时放人 每次放TickCount个人
            for (int i = 0; i < ConstValue.Queue_TickCount; i++)
            {
                // 没人排队时
                if (self.Queue.Count <= 0)
                {
                    return;
                }

                // 有人排队时
                QueueInfo queueInfo = self.Queue.First;
                
                // 发送消息 请求进入Map场景服务器
                self.EnterMap(queueInfo.UnitId).Coroutine();
            }
        }

        public static async ETTask EnterMap(this QueueMgrComponent self, long unitId)
        {
            // 当前角色是否可以变为在线
            // 已经在线
            if (self.Online.Add(unitId))
            {
                return;
            }
            // 没在线 可以在线 移出排队
            QueueInfo queueInfo = self.Queue.Remove(unitId);

            if (queueInfo != null)
            {
                // 通知Gate可以进入Map场景服务器
                G2Queue_EnterMap g2QueueEnterMap = (G2Queue_EnterMap)await MessageHelper.CallActor(queueInfo.GateActorId, new Queue2G_EnterMap()
                {
                    Account = queueInfo.Account, UnitId = queueInfo.UnitId
                });

                // 根据Response移除在线状态
                if (g2QueueEnterMap.NeedRemove)
                {
                    self.Online.Remove(unitId);
                }
                // 释放queueInfo实体
                queueInfo.Dispose();
            }
            
            await ETTask.CompletedTask;
        }

        public static void UpdateQueue(this QueueMgrComponent self)
        {
            using (DictionaryPoolComponent<long, Queue2G_UpdateInfo> dict = DictionaryPoolComponent<long, Queue2G_UpdateInfo>.Creat())
            {
                using (var enumerator = self.Queue.GetEnumerator())
                {
                    int i = 1;
                    while (enumerator.MoveNext())
                    {
                        QueueInfo queueInfo = enumerator.Current;
                        queueInfo.Index = i;
                        i++;

                        Queue2G_UpdateInfo queue2GUpdateInfo;
                        if (!dict.TryGetValue(queueInfo.GateActorId, out queue2GUpdateInfo))
                        {
                            queue2GUpdateInfo = new Queue2G_UpdateInfo() { Count = self.Queue.Count };
                            dict.Add(queueInfo.GateActorId, queue2GUpdateInfo);
                        }
                        queue2GUpdateInfo.Account.Add(queueInfo.Account);
                        queue2GUpdateInfo.Index.Add(queueInfo.Index);
                    }
                }

                foreach (var info in dict)
                {
                    MessageHelper.SendActor(info.Key, info.Value);
                }
            }
        }

        public static void Disconnect(this QueueMgrComponent self, long unitId, bool isProtect)
        {
            // 掉线保护
            if (isProtect)
            {
                if (self.Online.Contains(unitId) || self.Queue.ContainsKey(unitId))
                {
                    self.Protects.AddLast(unitId, new ProtectInfo() { UnitId = unitId, Time = TimeHelper.ServerNow() });
                }

            }
            // 不保护
            else
            {
                self.Online.Remove(unitId);
                self.Queue.Remove(unitId);
                self.Protects.Remove(unitId);
            }
        }

        public static void ClearProtect(this QueueMgrComponent self)
        {
            long targetTime = TimeHelper.ServerNow() - ConstValue.Queue_ProtectTime;

            while (self.Protects.Count > 0)
            {
                ProtectInfo protectInfo = self.Protects.First;

                if (self.Protects.First.Time > targetTime)
                {
                    break;
                }
                
                self.Disconnect(protectInfo.UnitId, false);
            }
        }
    }
}