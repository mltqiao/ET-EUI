namespace ET
{
    [ComponentOf(typeof(Car))]
    public class EngineComponent: Entity, IAwake, IUpdate, IDestroy
    {

    }
}