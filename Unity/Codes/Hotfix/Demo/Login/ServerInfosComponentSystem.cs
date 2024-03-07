namespace ET
{
    [FriendClassAttribute(typeof(ET.ServerInfosComponent))]
    [FriendClassAttribute(typeof(ET.ServerInfo))]
    public static class ServerInfosComponentSystem
    {
        public static void ClearServerInfo(this ServerInfosComponent self)
        {
            foreach (var serverInfo in self.ServerInfosList)
            {
                serverInfo?.Dispose();
            }

            self.ServerInfosList.Clear();
        }

        public static void AddServerInfo(this ServerInfosComponent self, ServerListInfo serverListInfo)
        {
            ServerInfo serverInfo = self.AddChild<ServerInfo>();
            serverInfo.ServerZone = serverListInfo.Zone;
            serverInfo.Name = serverListInfo.Name;
            serverInfo.Status = serverListInfo.Status;
            self.ServerInfosList.Add(serverInfo);
        }
    }
}