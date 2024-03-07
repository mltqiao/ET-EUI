using System;
using System.Collections.Generic;

namespace ET
{
    [FriendClassAttribute(typeof(ET.AccountDB))]
    [FriendClassAttribute(typeof(ET.RealmAccountComponent))]
    public class C2R_AccountLoginHandler : AMRpcHandler<C2R_AccountLogin, R2C_AccountLogin>
    {
        protected override async ETTask Run(Session session, C2R_AccountLogin request, R2C_AccountLogin response, Action reply)
        {
            session.RemoveComponent<SessionAcceptTimeoutComponent>();

            int modCount = (int)((ulong)request.Account.GetLongHashCode() % (uint)StartSceneConfigCategory.Instance.Realms.Count);
            if (session.DomainScene().InstanceId != StartSceneConfigCategory.Instance.Realms[modCount].InstanceId)
            {
                response.Error = ErrorCode.ERR_RealmAddressError;
                reply();

                session?.Disconnent().Coroutine();
                return;
            }

            // 重复登录
            RealmAccountComponent realmAccountComponent = session.GetComponent<RealmAccountComponent>();
            if (realmAccountComponent != null)
            {
                response.Error = ErrorCode.ERR_Login_RepeatedLogin;
                reply();
                return;
            }

            // 输入了空账号密码
            if (string.IsNullOrEmpty(request.Account) || string.IsNullOrEmpty(request.Password))
            {
                response.Error = ErrorCode.ERR_Login_AccountError;
                reply();
                return;
            }

            string account = request.Account;
            string password = request.Password;

            //异步并按队列执行查询和写入
            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.AccountLogin, account.GetLongHashCode()))
            {
                AccountDB accountDB = null;
                List<AccountDB> list = await session.GetDirectDB().Query<AccountDB>(db => db.Account == account);
                if (list.Count > 0)
                {
                    accountDB = list[0];
                }

                // 是否正式部署
                if (Game.Options.Develop == 0)
                {
                    if (accountDB == null)
                    {
                        response.Error = ErrorCode.ERR_Login_AccountNotExist;
                        reply();
                        return;
                    }

                    if (accountDB.Password != password)
                    {
                        response.Error = ErrorCode.ERR_Login_PasswordWrong;
                        reply();
                        return;
                    }
                }
                else
                {
                    if (accountDB == null)
                    {
                        accountDB = session.AddChild<AccountDB>();
                        accountDB.Account = account;
                        accountDB.Password = password;
                        await session.GetDirectDB().Save(accountDB);
                    }
                }

                // 登录成功后添加RealmAccountComponent防止重复登录
                realmAccountComponent = session.AddComponent<RealmAccountComponent>();
                realmAccountComponent.Info = accountDB;
                realmAccountComponent.AddChild(accountDB);
            }
            
            reply();

            await ETTask.CompletedTask;
        }
    }
}