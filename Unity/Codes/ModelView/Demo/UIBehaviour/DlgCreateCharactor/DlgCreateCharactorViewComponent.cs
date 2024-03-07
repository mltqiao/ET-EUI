
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ComponentOf(typeof(UIBaseWindow))]
	[EnableMethod]
	public  class DlgCreateCharactorViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Button E_CreateButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CreateButton == null )
     			{
		    		this.m_E_CreateButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Create");
     			}
     			return this.m_E_CreateButton;
     		}
     	}

		public UnityEngine.UI.Image E_CreateImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CreateImage == null )
     			{
		    		this.m_E_CreateImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Create");
     			}
     			return this.m_E_CreateImage;
     		}
     	}

		public UnityEngine.UI.Button E_BackButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_BackButton == null )
     			{
		    		this.m_E_BackButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Back");
     			}
     			return this.m_E_BackButton;
     		}
     	}

		public UnityEngine.UI.Image E_CharactorNameImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CharactorNameImage == null )
     			{
		    		this.m_E_CharactorNameImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_CharactorName");
     			}
     			return this.m_E_CharactorNameImage;
     		}
     	}

		public TMPro.TMP_InputField E_CharactorNameTMP_InputField
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CharactorNameTMP_InputField == null )
     			{
		    		this.m_E_CharactorNameTMP_InputField = UIFindHelper.FindDeepChild<TMPro.TMP_InputField>(this.uiTransform.gameObject,"E_CharactorName");
     			}
     			return this.m_E_CharactorNameTMP_InputField;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_CreateButton = null;
			this.m_E_CreateImage = null;
			this.m_E_BackButton = null;
			this.m_E_CharactorNameImage = null;
			this.m_E_CharactorNameTMP_InputField = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_CreateButton = null;
		private UnityEngine.UI.Image m_E_CreateImage = null;
		private UnityEngine.UI.Button m_E_BackButton = null;
		private UnityEngine.UI.Image m_E_CharactorNameImage = null;
		private TMPro.TMP_InputField m_E_CharactorNameTMP_InputField = null;
		public Transform uiTransform = null;
	}
}
