namespace ET
{

    public class CarAwakeSystem : AwakeSystem<Car>
    {
        public override void Awake(Car self)
        {
            self.Awake();
        }
    }

    public class CarUpdateSystem : UpdateSystem<Car>
    {
        public override void Update(Car self)
        {
            Log.Debug("Car Update!");
        }
    }

    public class CarDestorySystem : DestroySystem<Car>
    {
        public override void Destroy(Car self)
        {
            Log.Debug("Car Destory!");
        }
    }
    
    public static class CarSystem
    {
        public static void Awake(this Car self)
        {
            Log.Debug("Car Awake!");
        }
        
        public static void Run(this Car self)
        {
            self.GetComponent<EngineComponent>().StartUp();
            Log.Debug("Car Start Run!");
        }

        public static void Stop(this Car self)
        {
            Log.Debug("Car Stop!");
        }
    }
}