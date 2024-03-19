namespace ET
{
    public static class UnitHelper
    {
        public static Unit GetMyUnitFromZoneScene(Scene zoneScene)
        {
            PlayerComponent playerComponent = zoneScene.GetComponent<PlayerComponent>();
            Scene currentScene = zoneScene.GetComponent<CurrentScenesComponent>().Scene;
            return currentScene.GetComponent<UnitComponent>().Get(playerComponent.MyId);
        }
        
        public static Unit GetMyUnitFromCurrentScene(Scene currentScene)
        {
            PlayerComponent playerComponent = currentScene.Parent.Parent.GetComponent<PlayerComponent>();
            return currentScene.GetComponent<UnitComponent>().Get(playerComponent.MyId);
        }

        public static bool IsMyUnit(this Unit unit)
        {
            if (unit == null || unit.IsDisposed)
            {
                return false;
            }

            UnitComponent unitComponent = unit.ZoneScene()?.CurrentScene()?.GetComponent<UnitComponent>();

            if (unitComponent == null || unitComponent.IsDisposed)
            {
                return false;
            }

            if (unitComponent.MyUnit == null)
            {
                return false;
            }

            return unitComponent.MyUnit == null? false : unitComponent.MyUnit.Id == unit.Id;
        }
    }
}