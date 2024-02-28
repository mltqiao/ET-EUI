using System.Diagnostics;
using ET.EventType;

namespace ET
{
    public static class SceneFactory
    {
        public static Scene CreateZoneScene(int zone, string name, Entity parent)
        {
            Scene zoneScene = EntitySceneFactory.CreateScene(Game.IdGenerater.GenerateInstanceId(), zone, SceneType.Zone, name, parent);
            zoneScene.AddComponent<ZoneSceneFlagComponent>();
            zoneScene.AddComponent<NetKcpComponent, int>(SessionStreamDispatcherType.SessionStreamDispatcherClientOuter);
			zoneScene.AddComponent<CurrentScenesComponent>();
            zoneScene.AddComponent<ObjectWait>();
            zoneScene.AddComponent<PlayerComponent>();
            
            Game.EventSystem.Publish(new EventType.AfterCreateZoneScene() {ZoneScene = zoneScene});
            
            
            // Log.Debug("Before TestEvent Publish");
            Game.EventSystem.Publish(new EventType.TestEvent() {ZoneScene = zoneScene});
            // Log.Debug("After TestEvent Publish");
            
            // Log.Debug("Before TestEventAsync Publish");
            //Game.EventSystem.PublishAsync(new EventType.TestEventAsync() {ZoneScene = zoneScene}).Coroutine();
            // Log.Debug("After TestEventAsync Publish");

            Log.Debug("Before TestEventClass Publish");
            TestEventClass.Instance.ZoneScene = zoneScene;
            Game.EventSystem.PublishClass(TestEventClass.Instance);
            Log.Debug("After TestEventClass Publish");
            
            return zoneScene;
        }
        
        public static Scene CreateCurrentScene(long id, int zone, string name, CurrentScenesComponent currentScenesComponent)
        {
            Scene currentScene = EntitySceneFactory.CreateScene(id, IdGenerater.Instance.GenerateInstanceId(), zone, SceneType.Current, name, currentScenesComponent);
            currentScenesComponent.Scene = currentScene;
            
            Game.EventSystem.Publish(new EventType.AfterCreateCurrentScene() {CurrentScene = currentScene});
            return currentScene;
        }
        
        
    }
}