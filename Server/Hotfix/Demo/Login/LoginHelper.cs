using System;

namespace ET
{
    [FriendClassAttribute(typeof(ET.AccountZoneDB))]
    [FriendClassAttribute(typeof(ET.GateUser))]
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
                // TODO 通知排队服务器 Map场景服务器角色进行下线
            }

            if (dispose)
            {
                self.DomainScene().GetComponent<GateUserMgrComponent>()?.Remove(accountZoneDB?.Account);
            }
            else
            {
                self.State = GateUserState.InGate;
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
                session.Send(new A2C_Disconnect(){ Error = ErrorCode.ERR_Login_MultiLogin });
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
    }
}