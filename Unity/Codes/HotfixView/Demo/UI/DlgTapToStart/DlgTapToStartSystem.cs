using System;

namespace ET
{
	[FriendClass(typeof(DlgTapToStart))]
	public static  class DlgTapToStartSystem
	{
		public static void RegisterUIEvent(this DlgTapToStart self)
		{
			self.View.E_TapToStartButton.AddListenerAsync(self.OnTapToStartClickHandler);
		}

		public static void ShowWindow(this DlgTapToStart self, Entity contextData = null)
		{
			
		}

		public static async ETTask OnTapToStartClickHandler(this DlgTapToStart self)
		{
			try
			{
				int errorCode = await LoginHelper.GetRoleInfos(self.ZoneScene());
				if (errorCode != ErrorCode.ERR_Success)
				{
					return;
				}
				self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_TapToStart);
				self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_CharactorSelect);

			}
			catch (Exception e)
			{
				Log.Error(e.ToString());
			}
		}

	}
}
