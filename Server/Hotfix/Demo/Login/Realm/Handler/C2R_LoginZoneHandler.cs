using System;

namespace ET
{
    [FriendClassAttribute(typeof(ET.AccountDB))]
    [FriendClassAttribute(typeof(ET.RealmAccountComponent))]
    public class C2R_LoginZoneHandler : AMRpcHandler<C2R_LoginZone, R2C_LoginZone>
    {
        protected override async ETTask Run(Session session, C2R_LoginZone request, R2C_LoginZone response, Action reply)
        {
            RealmAccountComponent realmAccountComponent = session.GetComponent<RealmAccountComponent>();
            if (realmAccountComponent == null)
            {
                response.Error = ErrorCode.ERR_Login_AccountNotLogin;
                reply();
                return;
            }

            if (!StartZoneConfigCategory.Instance.Contain(request.Zone))
            {
                response.Error = ErrorCode.ERR_Login_ZoneNotExist;
                reply();
                return;
            }
            string account = realmAccountComponent.Info.Account;
            
            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginZone, account.GetLongHashCode()))
            {
                StartSceneConfig startSceneConfig = LoginHelper.GetGateConfig(request.Zone, account);
                response.GateAddress = startSceneConfig.InnerIPOutPort.ToString();

                G2R_GetGateKey g2RGetGateKey = (G2R_GetGateKey)await MessageHelper.CallActor(startSceneConfig.InstanceId, new R2G_GetGateKey()
                {
                    Info = new LoginGateInfo()
                    {
                        Account = account,
                        LoginZone = request.Zone
                    }
                });
                if (g2RGetGateKey.Error != ErrorCode.ERR_Success)
                {
                    response.Error = g2RGetGateKey.Error;
                    reply();
                    return;
                }
                response.GateKey = g2RGetGateKey.GateKey;
                
                reply();
                session?.Disconnent().Coroutine();
            }

            await ETTask.CompletedTask;
        }
    }
}