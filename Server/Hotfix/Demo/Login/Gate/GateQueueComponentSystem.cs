namespace ET
{
    public class GateQueueDestroySystem : DestroySystem<GateQueueComponent>
    {
        public override void Destroy(GateQueueComponent self)
        {
            self.UnitId = default;
            self.Index = default;
            self.Count = default;
        }
    }
    
    
    public static class GateQueueComponentSystem
    {
        
    }
}