namespace ET
{
    public class GateUserDisconnectComponentAwakeSystem : AwakeSystem<GateUserDisconnectComponent, long>
    {
        public override void Awake(GateUserDisconnectComponent self, long time)
        {
            self.Timer = TimerComponent.Instance.NewOnceTimer(TimeHelper.ServerNow() + time, TimerType.GateUserDisconnet, self);
        }
    }
    [Timer(TimerType.GateUserDisconnet)]
    public class GateUserDisconnect_TimerHandler : ATimer<GateUserDisconnectComponent>
    {
        public override void Run(GateUserDisconnectComponent t)
        {
            t.GetParent<GateUser>().OfflineWithLock().Coroutine();
        }
    }
    
    public class GateUserDisconnectComponentDestroySystem : DestroySystem<GateUserDisconnectComponent>
    {
        public override void Destroy(GateUserDisconnectComponent self)
        {
            TimerComponent.Instance.Remove(ref self.Timer);
        }
    }

    public static class GateUserDisconnectComponentSystem
    {
        
    }
}