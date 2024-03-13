
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ComponentOf(typeof(UIBaseWindow))]
	[EnableMethod]
	public  class DlgSceneLoadingViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Slider E_LoadingSlider
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_LoadingSlider == null )
     			{
		    		this.m_E_LoadingSlider = UIFindHelper.FindDeepChild<UnityEngine.UI.Slider>(this.uiTransform.gameObject,"E_Loading");
     			}
     			return this.m_E_LoadingSlider;
     		}
     	}

		public TMPro.TextMeshProUGUI E_LoadingValueTextMeshProUGUI
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_LoadingValueTextMeshProUGUI == null )
     			{
		    		this.m_E_LoadingValueTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject,"E_Loading/E_LoadingValue");
     			}
     			return this.m_E_LoadingValueTextMeshProUGUI;
     		}
     	}

		public UnityEngine.RectTransform EG_RotateRoundRectTransform
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EG_RotateRoundRectTransform == null )
     			{
		    		this.m_EG_RotateRoundRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject,"EG_RotateRound");
     			}
     			return this.m_EG_RotateRoundRectTransform;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_LoadingSlider = null;
			this.m_E_LoadingValueTextMeshProUGUI = null;
			this.m_EG_RotateRoundRectTransform = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Slider m_E_LoadingSlider = null;
		private TMPro.TextMeshProUGUI m_E_LoadingValueTextMeshProUGUI = null;
		private UnityEngine.RectTransform m_EG_RotateRoundRectTransform = null;
		public Transform uiTransform = null;
	}
}
