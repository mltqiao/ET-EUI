using ET.EventType;

namespace ET
{
    public class TestEvent_CreatZoneScene : AEvent<EventType.TestEvent>
    {
        protected override void Run(TestEvent a)
        {
            Log.Debug("Event TestEvent");
        }
    }
}