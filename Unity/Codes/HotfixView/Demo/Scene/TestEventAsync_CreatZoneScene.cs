using ET.EventType;

namespace ET
{
    public class TestEventAsync_CreatZoneScene : AEventAsync<EventType.TestEventAsync>
    {
        protected async override ETTask Run(TestEventAsync a)
        {
            Log.Debug("Enter TestEventAsync");

            await TimerComponent.Instance.WaitAsync(1000);
            
            Log.Debug("After TestEventAsync");
        }
    }
}