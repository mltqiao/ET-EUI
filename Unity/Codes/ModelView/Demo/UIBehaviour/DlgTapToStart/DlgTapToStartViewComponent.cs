
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ComponentOf(typeof(UIBaseWindow))]
	[EnableMethod]
	public  class DlgTapToStartViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Button E_TapToStartButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_TapToStartButton == null )
     			{
		    		this.m_E_TapToStartButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"TapToStart/E_TapToStart");
     			}
     			return this.m_E_TapToStartButton;
     		}
     	}

		public UnityEngine.UI.Image E_TapToStartImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_TapToStartImage == null )
     			{
		    		this.m_E_TapToStartImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"TapToStart/E_TapToStart");
     			}
     			return this.m_E_TapToStartImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_TapToStartButton = null;
			this.m_E_TapToStartImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_TapToStartButton = null;
		private UnityEngine.UI.Image m_E_TapToStartImage = null;
		public Transform uiTransform = null;
	}
}
