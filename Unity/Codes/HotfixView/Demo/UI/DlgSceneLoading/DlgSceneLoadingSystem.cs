using System.Collections;
using System.Collections.Generic;
using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	[Timer(TimerType.SceneLoading)]
	public class SceneLoadintTime: ATimer<DlgSceneLoading>
	{
		public override void Run(DlgSceneLoading t)
		{
			t.RefreshLoadProcess();
		}
	}
	
	[FriendClass(typeof(DlgSceneLoading))]
	public static  class DlgSceneLoadingSystem
	{

		public static void RegisterUIEvent(this DlgSceneLoading self)
		{
		 
		}

		public static void ShowWindow(this DlgSceneLoading self, Entity contextData = null)
		{
			self.View.E_LoadingSlider.value = 0;
			self.View.E_LoadingValueTextMeshProUGUI.text = "Loading.....";

			self.TimerId = TimerComponent.Instance.NewRepeatedTimer(100, TimerType.SceneLoading, self);
			self.View.EG_RotateRoundRectTransform.DORotate(new Vector3(0, 0, 360), 3f, RotateMode.FastBeyond360)
					.SetLoops(-1, LoopType.Yoyo);
		}

		public static void HideWindow(this DlgSceneLoading self)
		{
			TimerComponent.Instance.Remove(ref self.TimerId);
			self.View.EG_RotateRoundRectTransform.DOKill();
		}

		public static void RefreshLoadProcess(this DlgSceneLoading self)
		{
			SceneChangeComponent sceneChangeComponent = Game.Scene.GetComponent<SceneChangeComponent>();
			if (sceneChangeComponent == null)
			{
				return;
			}

			int process = sceneChangeComponent.Process();

			self.View.E_LoadingSlider.value = process;
			self.View.E_LoadingValueTextMeshProUGUI.text = $"Loading.....{process}";
		}

		 

	}
}
