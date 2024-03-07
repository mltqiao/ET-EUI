namespace ET
{
    [FriendClassAttribute(typeof(ET.RoleInfoDB))]
    public static class RoleInfoSystem
    {
        public static GateRoleInfo ToMessage(this RoleInfoDB self)
        {
            return new GateRoleInfo() { UnitId = self.Id, Level = self.Level, Name = self.Name };
        }
    }
}