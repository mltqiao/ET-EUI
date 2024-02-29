using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	[FriendClass(typeof(DlgAccountLogin))]
	public static  class DlgAccountLoginSystem
	{

		public static void RegisterUIEvent(this DlgAccountLogin self)
		{
			self.View.E_LoginButton.AddListenerAsync(self.OnCloseButtonClickHandler);
		}

		public static void ShowWindow(this DlgAccountLogin self, Entity contextData = null)
		{
			self.View.E_ServerAddressDropdown.options.Clear();
			
			string[] lines = File.ReadAllLines(@"..\Excel\ServerAddress.txt");
			foreach (var line in lines)
			{
				Dropdown.OptionData op = new Dropdown.OptionData();
				op.text = line;
				self.View.E_ServerAddressDropdown.options.Add(op);
			}

			self.View.E_AccountTMP_InputField.text = PlayerPrefs.GetString("Account", null);
			self.View.E_PasswordTMP_InputField.text = PlayerPrefs.GetString("Password", null);
		}

		public static async ETTask OnCloseButtonClickHandler(this DlgAccountLogin self)
		{
			string account = self.View.E_AccountTMP_InputField.text;
			string password = self.View.E_PasswordTMP_InputField.text;
			string address = self.View.E_ServerAddressDropdown.options[self.View.E_ServerAddressDropdown.value].text;

			try
			{
				await LoginHelper.Login(self.ZoneScene(), address, account, password);
				
				PlayerPrefs.SetString("Account",account);
				PlayerPrefs.SetString("Password",password);
			}
			catch (Exception e)
			{
				Log.Error(e.ToString());
				throw;
			}
			self.ZoneScene().GetComponent<UIComponent>().CloseWindow(WindowID.WindowID_AccountLogin);
			await ETTask.CompletedTask;
		}

	}
}
