namespace ET
{
    public class UnitInfoComponentAwakeSystem : AwakeSystem<UnitInfoComponent, UnitInfo>
    {
        public override void Awake(UnitInfoComponent self, UnitInfo unitInfo)
        {
            self.Name = unitInfo.Name;
        }
    }
    
    public static class UnitInfoComponentSystem
    {
        
    }
}