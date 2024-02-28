namespace ET
{
    public class EngineComponentAwakeSystem: AwakeSystem<EngineComponent>
    {
        public override void Awake(EngineComponent self)
        {
            Log.Debug("EngineComponent Awake!");
        }
    }

    public class EngineComponentUpdateSystem: UpdateSystem<EngineComponent>
    {
        public override void Update(EngineComponent self)
        {
            Log.Debug("EngineComponent Update!");
        }
    }

    public class EngineComponentDestorySystem: DestroySystem<EngineComponent>
    {
        public override void Destroy(EngineComponent self)
        {
            Log.Debug("EngineComponent Destory!");
        }
    }
    
    public static class EngineComponentSystem
    {
        public static void StartUp(this EngineComponent self)
        {
            Log.Debug("Engine StartUp!");
        }
    }
}