using ILRuntime.Mono.Cecil.Cil;

namespace ET
{
    public class AfterCreateZoneScene_AddComponent: AEvent<EventType.AfterCreateZoneScene>
    {
        protected override  void Run(EventType.AfterCreateZoneScene args)
        {
            Scene zoneScene = args.ZoneScene;
            UIComponent uiComponent = zoneScene.AddComponent<UIComponent>();
            zoneScene.AddComponent<UIPathComponent>();
            zoneScene.AddComponent<UIEventComponent>();
            zoneScene.AddComponent<RedDotComponent>();
            zoneScene.AddComponent<ResourcesLoaderComponent>();
        }

        public async ETTask Test(Scene zoneScene)
        {
            Log.Debug("Start Test");
            
            await TimerComponent.Instance.WaitAsync(3000);
            
            Log.Debug("3 seconds passed");
            
            int result = await this.Test2();
            
            Log.Debug(result.ToString());
        }

        public async ETTask<int> Test2()
        {
            await TimerComponent.Instance.WaitAsync(1000);
            return 100;
        }
    }
}