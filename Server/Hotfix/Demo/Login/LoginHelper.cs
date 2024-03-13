using System;

namespace ET
{
    [FriendClassAttribute(typeof(ET.AccountZoneDB))]
    [FriendClassAttribute(typeof(ET.GateUser))]
    [FriendClassAttribute(typeof(ET.GateMapComponent))]
    [FriendClassAttribute(typeof(ET.SessionPlayerComponent))]
    [FriendClassAttribute(typeof(ET.UnitGateComponent))]
    [FriendClassAttribute(typeof(ET.RoleInfoDB))]
    public static class LoginHelper
    {
        public static async ETTask<CoroutineLock> GetGateUserLock(string account)
        {
            if (string.IsNullOrEmpty(account))
            {
                throw new Exception("GetGateUserLock but account is Null!");
            }

            return await CoroutineLockComponent.Instance.Wait(CoroutineLockType.GateUserLock, account.GetLongHashCode());
        }

        public static ETTask<CoroutineLock> GetGateUserLock(this GateUser gateUser)
        {
            AccountZoneDB accountZoneDB = gateUser.GetComponent<AccountZoneDB>();
            return GetGateUserLock(accountZoneDB.Account);
        }

        public static async ETTask OfflineWithLock(this GateUser self, bool dispose = true)
        {
            if (self == null || self.IsDisposed)
            {
                return;
            }

            long instanceId = self.InstanceId;

            using (await self.GetGateUserLock())
            {
                if (instanceId != self.InstanceId)
                {
                    return;
                }

                await self.Offline(dispose);
            }
        }

        public static async ETTask Offline(this GateUser self, bool dispose = true)
        {
            if (self == null || self.IsDisposed)
            {
                return;
            }

            AccountZoneDB accountZoneDB = self.GetComponent<AccountZoneDB>();
            if (accountZoneDB != null)
            {
                MessageHelper.SendActor(self.DomainZone(), SceneType.Queue, new G2Queue_Disconnect() { UnitId = accountZoneDB.LastRoleId });
                // TODO 通知Map场景服务器角色进行下线
            }

            if (dispose)
            {
                self.DomainScene().GetComponent<GateUserMgrComponent>()?.Remove(accountZoneDB?.Account);
            }
            else
            {
                self.State = GateUserState.InGate;
                self.RemoveComponent<GateQueueComponent>();
            }

            await ETTask.CompletedTask;
        }

        public static void OfflineSession(this GateUser self)
        {
            Log.Console($"-> 账号{self.GetComponent<AccountZoneDB>()?.Account} 被顶号 {self.SessionInstanceId} 对外下线");

            Session session = self.Session;
            if (session != null)
            {
                // 发送给原先连接的客户端一条顶号下线消息
                session.Send(new A2C_Disconnect() { Error = ErrorCode.ERR_Login_MultiLogin });
                // 不再处理后续消息
                session.RemoveComponent<SessionUserComponent>();

                session.Disconnent().Coroutine();
            }

            self.SessionInstanceId = 0;

            // 防止后续玩家不登录 添加计时器 对内下线
            self.RemoveComponent<GateUserDisconnectComponent>();
            self.AddComponent<GateUserDisconnectComponent, long>(ConstValue.Login_GateUserDisconnectTime);
        }

        public static async ETTask Disconnent(this Session self)
        {
            if (self == null)
            {
                return;
            }

            long instanceId = self.InstanceId;

            await TimerComponent.Instance.WaitAsync(1000);

            if (instanceId != self.InstanceId)
            {
                return;
            }

            self.Dispose();
        }

        public static StartSceneConfig GetGateConfig(int zone, string account)
        {
            int modeCount = (int)((ulong)account.GetLongHashCode() % (uint)StartSceneConfigCategory.Instance.Gates[zone].Count);
            StartSceneConfig gateConfig = StartSceneConfigCategory.Instance.Gates[zone][modeCount];
            return gateConfig;
        }

        public static async ETTask EnterMap(this GateUser self)
        {
            AccountZoneDB accountZoneDB = self.GetComponent<AccountZoneDB>();
            Log.Console($"-> 测试 账号 {accountZoneDB.Account} 进入游戏");

            StartSceneConfig startSceneConfig = StartSceneConfigCategory.Instance.GetBySceneName(self.DomainZone(), "Map1");

            self.RemoveComponent<GateQueueComponent>();

            if (self.State == GateUserState.InMap)
            {
                M2C_StartSceneChange m2CStartSceneChange = new M2C_StartSceneChange() { SceneInstanceId = startSceneConfig.InstanceId, SceneName = startSceneConfig.Name };
                self.Session.Send(m2CStartSceneChange);
                self.Session.AddComponent<SessionPlayerComponent>().PlayerId = accountZoneDB.LastRoleId;
                MessageHelper.CallLocationActor(accountZoneDB.LastRoleId, new G2M_SecondLogin() { }).Coroutine();
                return;
            }

            self.State = GateUserState.InMap;
            // 用于传送的临时Map实体
            GateMapComponent gateMapComponent = self.AddComponent<GateMapComponent>();
            gateMapComponent.Scene = await SceneFactory.Create(gateMapComponent, "GateMap", SceneType.Map);

            Unit unit = UnitFactory.Create(gateMapComponent.Scene, accountZoneDB.LastRoleId, UnitType.Player);

            unit.AddComponent<UnitGateComponent, long>(self.InstanceId).Name = accountZoneDB.CurRole.Name;
            self.Session.AddComponent<SessionPlayerComponent>().PlayerId = accountZoneDB.LastRoleId;

            await TransferHelper.Transfer(unit, startSceneConfig.InstanceId, startSceneConfig.Name);

            await ETTask.CompletedTask;
        }
    }
}