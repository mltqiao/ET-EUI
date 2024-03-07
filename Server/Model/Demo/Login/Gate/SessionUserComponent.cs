namespace ET
{
    /// <summary>
    /// 当前Session对应的GateUser(Session)
    /// </summary>
    [ComponentOf(typeof(Session))]
    public class SessionUserComponent : Entity, IAwake<long>, IDestroy
    {
        public long GateUserInstanceId;
        
        public GateUser User => Game.EventSystem.Get(this.GateUserInstanceId) as GateUser;
    }
}