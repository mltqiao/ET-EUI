namespace ET
{
    [RobotCase(RobotCaseType.LoginTest)]
    [FriendClassAttribute(typeof(ET.RobotCase))]
    public class RobotCase_LoginTest : IRobotCase
    {
        public async ETTask Run(RobotCase robotCase)
        {
            string[] strs = robotCase.CommandLine.Split(' ');
            if (strs.Length < 4)
            {
                Log.Console("命令行格式有误, Run 2 起始值 数量");
                return;
            }

            int start = int.Parse(strs[2]);
            int count = int.Parse(strs[3]);
            for (int i = 0; i < count; i++)
            {
                await this.CreateRobot(robotCase, start + i);
            }

            await ETTask.CompletedTask;
            await TimerComponent.Instance.WaitAsync(9999999999);
        }

        private async ETTask CreateRobot(RobotCase robotCase, int i)
        {
            Scene scene = await robotCase.NewRobot(i);
            await TimerComponent.Instance.WaitAsync(3000);
            scene.AddComponent<AIComponent, int>(2);
        }
    }
}
