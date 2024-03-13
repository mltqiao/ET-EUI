
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ComponentOf(typeof(UIBaseWindow))]
	[EnableMethod]
	public  class DlgMainViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.RectTransform EG_TotalBGRectTransform
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EG_TotalBGRectTransform == null )
     			{
		    		this.m_EG_TotalBGRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject,"EG_TotalBG");
     			}
     			return this.m_EG_TotalBGRectTransform;
     		}
     	}

		public UnityEngine.RectTransform EG_JoyStickRectTransform
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EG_JoyStickRectTransform == null )
     			{
		    		this.m_EG_JoyStickRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject,"EG_TotalBG/EG_JoyStick");
     			}
     			return this.m_EG_JoyStickRectTransform;
     		}
     	}

		public UnityEngine.UI.Image E_JoyStickImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_JoyStickImage == null )
     			{
		    		this.m_E_JoyStickImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EG_TotalBG/EG_JoyStick/E_JoyStick");
     			}
     			return this.m_E_JoyStickImage;
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
		    		this.m_E_Play3Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EG_TotalBG/Player_Joystick_Right/E_Play3");
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
		    		this.m_E_Play2Button = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EG_TotalBG/Player_Joystick_Right/E_Play2");
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
		    		this.m_E_Play2Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EG_TotalBG/Player_Joystick_Right/E_Play2");
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
		    		this.m_E_Play1Button = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EG_TotalBG/Player_Joystick_Right/E_Play1");
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
		    		this.m_E_Play1Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EG_TotalBG/Player_Joystick_Right/E_Play1");
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
		    		this.m_E_Play0Button = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EG_TotalBG/Player_Joystick_Right/E_Play0");
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
		    		this.m_E_Play0Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EG_TotalBG/Player_Joystick_Right/E_Play0");
     			}
     			return this.m_E_Play0Image;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_EG_TotalBGRectTransform = null;
			this.m_EG_JoyStickRectTransform = null;
			this.m_E_JoyStickImage = null;
			this.m_E_Play3Image = null;
			this.m_E_Play2Button = null;
			this.m_E_Play2Image = null;
			this.m_E_Play1Button = null;
			this.m_E_Play1Image = null;
			this.m_E_Play0Button = null;
			this.m_E_Play0Image = null;
			this.uiTransform = null;
		}

		private UnityEngine.RectTransform m_EG_TotalBGRectTransform = null;
		private UnityEngine.RectTransform m_EG_JoyStickRectTransform = null;
		private UnityEngine.UI.Image m_E_JoyStickImage = null;
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
