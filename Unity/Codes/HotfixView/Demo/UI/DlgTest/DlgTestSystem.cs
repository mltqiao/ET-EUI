using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	[FriendClass(typeof(DlgTest))]
	public static  class DlgTestSystem
	{

		public static void RegisterUIEvent(this DlgTest self)
		{
			self.View.E_CloseButton.AddListener(self.OnCloseButtonClickHandler);
		}

		public static void ShowWindow(this DlgTest self, Entity contextData = null)
		{
			self.View.E_TestImage.color = Color.black;
			self.View.EG_TestTwoRectTransform.SetVisible(false);
		}

		public static void OnCloseButtonClickHandler(this DlgTest self)
		{
			//self.ZoneScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_Test);
			self.ZoneScene().GetComponent<UIComponent>().CloseWindow(WindowID.WindowID_Test);
		}

	}
}
