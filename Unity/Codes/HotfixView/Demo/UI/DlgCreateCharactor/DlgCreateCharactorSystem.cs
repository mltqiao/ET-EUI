using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	[FriendClass(typeof(DlgCreateCharactor))]
	public static  class DlgCreateCharactorSystem
	{

		public static void RegisterUIEvent(this DlgCreateCharactor self)
		{
			self.View.E_CreateButton.AddListenerAsync(self.OnCreateRoleClickHandler);
		}

		public static void ShowWindow(this DlgCreateCharactor self, Entity contextData = null)
		{
		}

		public static async ETTask OnCreateRoleClickHandler(this DlgCreateCharactor self)
		{
			string name = self.View.E_CharactorNameTMP_InputField.text;

			if (string.IsNullOrEmpty(name))
			{
				return;
			}

			try
			{
				int errorCode = await LoginHelper.CreateRole(self.ZoneScene(), name);
				if (errorCode != ErrorCode.ERR_Success)
				{
					return;
				}
				
				self.ZoneScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_CreateCharactor);
				self.ZoneScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_CharactorSelect);
			}
			catch (Exception e)
			{
				Log.Error(e.ToString());
			}
		}

	}
}
