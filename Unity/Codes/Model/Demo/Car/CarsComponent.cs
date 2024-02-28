namespace ET
{
    [ComponentOf(typeof(Scene))]
    [ChildType(typeof(Car))]
    public class CarsComponent : Entity, IAwake
    {
        
    }
}