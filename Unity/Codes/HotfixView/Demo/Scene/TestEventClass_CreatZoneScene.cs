using ET.EventType;

namespace ET
{
    public class TestEventClass_CreatZoneScene : AEventClass<TestEventClass>
    {
        protected override void Run(object a)
        {
            EventType.TestEventClass args = a as EventType.TestEventClass;
            
            Log.Debug("Enter TestEventClass");
        }
    }
}