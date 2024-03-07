namespace ET
{
    [ComponentOf(typeof(GateUser))]
    [ChildType(typeof(RoleInfoDB))]
    public class AccountZoneDB : Entity, IAwake, IDestroy
    {
        public string Account;
        public int LoginZoneId;
    }
}