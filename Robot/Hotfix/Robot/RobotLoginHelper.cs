using System.IO;

namespace ET
{
    [FriendClassAttribute(typeof(ET.RoleInfosComponent))]
    public static class RobotLoginHelper
    {
        public static async ETTask<int> Login(Scene zoneScene, string account, string password)
        {
            string[] line = File.ReadAllLines(@"..\Excel\ServerAddress.txt");
            string address = line[0];

            // 账号登录
            int errorCode = await LoginHelper.Login(zoneScene, address, account, password);
            if (errorCode != ErrorCode.ERR_Success)
            {
                Log.Error($"登录测试 LoginHelper.Login Error {errorCode}");
                return errorCode;
            }

            // 获取游戏区服列表
            errorCode = await LoginHelper.GetServerList(zoneScene);
            if (errorCode != ErrorCode.ERR_Success)
            {
                Log.Error($"登录测试 LoginHelper.GetServerList Error {errorCode}");
                return errorCode;
            }

            // 登录区服
            errorCode = await LoginHelper.LoginZone(zoneScene, 1);
            if (errorCode != ErrorCode.ERR_Success)
            {
                Log.Error($"登录测试 LoginHelper.LoginZone Error {errorCode}");
                return errorCode;
            }

            // 获取当前区服角色列表
            errorCode = await LoginHelper.GetRoleInfos(zoneScene);
            if (errorCode != ErrorCode.ERR_Success)
            {
                Log.Error($"登录测试 LoginHelper.GetRoleInfos Error {errorCode}");
                return errorCode;
            }
            // 无角色时创建角色
            if (zoneScene.GetComponent<RoleInfosComponent>().RoleInfos.Count <= 0)
            {
                errorCode = await LoginHelper.CreateRole(zoneScene, $"{RandomHelper.RandomNumber(0, 99999)}");
                if (errorCode != ErrorCode.ERR_Success)
                {
                    Log.Error($"登录测试 LoginHelper.CreateRole Error {errorCode}");
                    return errorCode;
                }
            }
            // 选择角色进入场景
            zoneScene.GetComponent<RoleInfosComponent>().CurrentUnitId = zoneScene.GetComponent<RoleInfosComponent>().RoleInfos[0].Id;
            errorCode = await LoginHelper.EnterMap(zoneScene);
            if (errorCode != ErrorCode.ERR_Success)
            {
                Log.Error($"登录测试 LoginHelper.EnterMap Error {errorCode}");
            }
            
            return errorCode;
        }
    }
}
