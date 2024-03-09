using System;

namespace ET
{
    [FriendClassAttribute(typeof(ET.AccountZoneDB))]
    [FriendClassAttribute(typeof(ET.RoleInfoDB))]
    [FriendClassAttribute(typeof(ET.GateUser))]
    [FriendClassAttribute(typeof(ET.GateQueueComponent))]
    public class C2G_Enter2MapHandler : AMRpcHandler<C2G_Enter2Map, G2C_Enter2Map>
    {
        protected override async ETTask Run(Session session, C2G_Enter2Map request, G2C_Enter2Map response, Action reply)
        {
            GateUser gateUser = session.GetComponent<SessionUserComponent>()?.User;

            if (gateUser == null)
            {
                response.Error = ErrorCode.ERR_Login_NoneGateUser;
                reply();
                return;
            }

            AccountZoneDB accountZoneDB = gateUser.GetComponent<AccountZoneDB>();

            if (accountZoneDB == null)
            {
                response.Error = ErrorCode.ERR_Login_NoneAccountZone;
                reply();
                return;
            }

            long instanceId = accountZoneDB.InstanceId;
            long unitId = request.UnitId;
            string account = accountZoneDB.Account;

            using (await gateUser.GetGateUserLock())
            {
                if (instanceId != accountZoneDB.InstanceId)
                {
                    response.Error = ErrorCode.ERR_Login_NoneAccountZone;
                    reply();
                    return;
                }

                RoleInfoDB targetRoleInfo = accountZoneDB.GetChild<RoleInfoDB>(unitId);
                if (targetRoleInfo == null || targetRoleInfo.IsDeleted)
                {
                    response.Error = ErrorCode.ERR_Login_NoRoleDB;
                    reply();
                    return;
                }

                // 正常的选择角色流程
                accountZoneDB.LastRoleId = unitId;

                Queue2G_Enqueue queue2GEnqueue = (Queue2G_Enqueue)await MessageHelper.CallActor(accountZoneDB.LoginZoneId, SceneType.Queue, new G2Queue_Enqueue()
                {
                    Account = account,
                    UnitId = unitId,
                    GateActorId = session.DomainScene().InstanceId
                });

                if (queue2GEnqueue.Error != ErrorCode.ERR_Success)
                {
                    response.Error = queue2GEnqueue.Error;
                    reply();
                    return;
                }

                response.InQueue = queue2GEnqueue.NeedQueue;
                if (queue2GEnqueue.NeedQueue)
                {
                    gateUser.State = GateUserState.InQueue;
                    GateQueueComponent gateQueueComponent = gateUser.GetComponent<GateQueueComponent>();
                    if (gateQueueComponent == null)
                    {
                        gateQueueComponent = gateUser.AddComponent<GateQueueComponent>();
                    }

                    gateQueueComponent.UnitId = unitId;
                    gateQueueComponent.Index = queue2GEnqueue.Index;
                    gateQueueComponent.Count = queue2GEnqueue.Count;
                    response.Index = queue2GEnqueue.Index;
                    response.Count = queue2GEnqueue.Count;
                }

                reply();

                DBComponent db = session.GetDirectDB();
                await db.Save(accountZoneDB);

                if (!queue2GEnqueue.NeedQueue)
                {
                    Log.Console($"-> 测试 账号{account} 免排队直接进入游戏");
                    //游戏角色进入Map场景服务器
                }
            }

            await ETTask.CompletedTask;
        }
    }
}