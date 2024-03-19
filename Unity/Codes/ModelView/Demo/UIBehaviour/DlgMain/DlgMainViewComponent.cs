
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ComponentOf(typeof(UIBaseWindow))]
	[EnableMethod]
	public  class DlgMainViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.EventSystems.EventTrigger E_JoyStickBgEventTrigger
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_JoyStickBgEventTrigger == null )
     			{
		    		this.m_E_JoyStickBgEventTrigger = UIFindHelper.FindDeepChild<UnityEngine.EventSystems.EventTrigger>(this.uiTransform.gameObject,"E_TotalBG/E_JoyStickBg");
     			}
     			return this.m_E_JoyStickBgEventTrigger;
     		}
     	}

		public UnityEngine.UI.Image E_JoyStickCenterImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_JoyStickCenterImage == null )
     			{
		    		this.m_E_JoyStickCenterImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_TotalBG/E_JoyStickBg/E_JoyStickCenter");
     			}
     			return this.m_E_JoyStickCenterImage;
     		}
     	}

		public UnityEngine.UI.Image E_Play3Image
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_Play3Image == null )
     			{
		    		this.m_E_Play3Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_TotalBG/Player_Joystick_Right/E_Play3");
     			}
     			return this.m_E_Play3Image;
     		}
     	}

		public UnityEngine.UI.Button E_Play2Button
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_Play2Button == null )
     			{
		    		this.m_E_Play2Button = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_TotalBG/Player_Joystick_Right/E_Play2");
     			}
     			return this.m_E_Play2Button;
     		}
     	}

		public UnityEngine.UI.Image E_Play2Image
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_Play2Image == null )
     			{
		    		this.m_E_Play2Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_TotalBG/Player_Joystick_Right/E_Play2");
     			}
     			return this.m_E_Play2Image;
     		}
     	}

		public UnityEngine.UI.Button E_Play1Button
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_Play1Button == null )
     			{
		    		this.m_E_Play1Button = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_TotalBG/Player_Joystick_Right/E_Play1");
     			}
     			return this.m_E_Play1Button;
     		}
     	}

		public UnityEngine.UI.Image E_Play1Image
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_Play1Image == null )
     			{
		    		this.m_E_Play1Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_TotalBG/Player_Joystick_Right/E_Play1");
     			}
     			return this.m_E_Play1Image;
     		}
     	}

		public UnityEngine.UI.Button E_Play0Button
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_Play0Button == null )
     			{
		    		this.m_E_Play0Button = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_TotalBG/Player_Joystick_Right/E_Play0");
     			}
     			return this.m_E_Play0Button;
     		}
     	}

		public UnityEngine.UI.Image E_Play0Image
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_Play0Image == null )
     			{
		    		this.m_E_Play0Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_TotalBG/Player_Joystick_Right/E_Play0");
     			}
     			return this.m_E_Play0Image;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_JoyStickBgEventTrigger = null;
			this.m_E_JoyStickCenterImage = null;
			this.m_E_Play3Image = null;
			this.m_E_Play2Button = null;
			this.m_E_Play2Image = null;
			this.m_E_Play1Button = null;
			this.m_E_Play1Image = null;
			this.m_E_Play0Button = null;
			this.m_E_Play0Image = null;
			this.uiTransform = null;
		}

		private UnityEngine.EventSystems.EventTrigger m_E_JoyStickBgEventTrigger = null;
		private UnityEngine.UI.Image m_E_JoyStickCenterImage = null;
		private UnityEngine.UI.Image m_E_Play3Image = null;
		private UnityEngine.UI.Button m_E_Play2Button = null;
		private UnityEngine.UI.Image m_E_Play2Image = null;
		private UnityEngine.UI.Button m_E_Play1Button = null;
		private UnityEngine.UI.Image m_E_Play1Image = null;
		private UnityEngine.UI.Button m_E_Play0Button = null;
		private UnityEngine.UI.Image m_E_Play0Image = null;
		public Transform uiTransform = null;
	}
}
