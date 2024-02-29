using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	[FriendClass(typeof(DlgTapToStart))]
	public static  class DlgTapToStartSystem
	{

		public static void RegisterUIEvent(this DlgTapToStart self)
		{
			self.View.E_TapToStartButton.AddListener(self.OnTapToStartClickHandler);
		}

		public static void ShowWindow(this DlgTapToStart self, Entity contextData = null)
		{
		}

		public static void OnTapToStartClickHandler(this DlgTapToStart self)
		{
			UIComponent uiComponent = self.ZoneScene().GetComponent<UIComponent>();
			uiComponent.ShowWindow(WindowID.WindowID_AccountLogin);
			self.ZoneScene().GetComponent<UIComponent>().CloseWindow(WindowID.WindowID_TapToStart);
		}

	}
}
