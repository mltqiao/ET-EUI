namespace ET
{
    public class AccountDBSystem : DestroySystem<AccountDB>
    {
        public override void Destroy(AccountDB self)
        {
            self.Account = null;
            self.Password = null;
        }
    }
}