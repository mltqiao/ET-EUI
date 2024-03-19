using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ET
{
	[Timer(TimerType.JoyMoveTimer)]
	public class JoyTimer: ATimer<DlgMain>
	{
		public override void Run(DlgMain self)
		{
			try
			{
				self.JoyMoveUpdate();
			}
			catch (Exception e)
			{
				Log.Error($"move timer error : {self.Id}\n{e}");
			}
		}
	}
	
	[FriendClass(typeof(DlgMain))]
	public static  class DlgMainSystem
	{

		public static void RegisterUIEvent(this DlgMain self)
		{
		 self.View.E_JoyStickBgEventTrigger.RegisterEvent(EventTriggerType.PointerDown, self.OnJoySticPointerDown);
		 self.View.E_JoyStickBgEventTrigger.RegisterEvent(EventTriggerType.PointerUp, self.OnJoySticPointerUp);
		 self.View.E_JoyStickBgEventTrigger.RegisterEvent(EventTriggerType.Drag, self.OnJoySticPointerMove);

		 self.originPos = self.View.E_JoyStickCenterImage.transform.localPosition;
		}

		public static void ShowWindow(this DlgMain self, Entity contextData = null)
		{
			self.joyMoveTimerId = TimerComponent.Instance.NewFrameTimer(TimerType.JoyMoveTimer, self);
		}

		public static void HideWindow(this DlgMain self) 
		{
			TimerComponent.Instance.Remove(ref self.joyMoveTimerId);
		}

		public static void OnJoySticPointerDown(this DlgMain self, BaseEventData baseEventData)
		{
			PointerEventData eventData = baseEventData as PointerEventData;
			Vector2 localPos;

			RectTransformUtility.ScreenPointToLocalPointInRectangle(
				self.View.E_JoyStickBgEventTrigger.transform as RectTransform,
				eventData.position,
				eventData.pressEventCamera,
				out localPos
			);

			self.moveDir = (localPos - self.originPos).normalized;
			self.isUpdate = true;
			// 修改位置
			self.View.E_JoyStickCenterImage.transform.localPosition = localPos;

			Vector3 dir = new Vector3(self.moveDir.x, 0f, self.moveDir.y).normalized;
			Vector3 camPos = Camera.main.transform.rotation * dir;
			Vector3 finalMoveDir = new Vector3(camPos.x, 0f, camPos.z).normalized;
			self.ZoneScene().CurrentScene().GetComponent<OperaComponent>().JoyMove(finalMoveDir);
			self.coolTime = 0f;
		}
		
		public static void OnJoySticPointerUp(this DlgMain self, BaseEventData baseEventData)
		{
			self.View.E_JoyStickCenterImage.transform.localPosition = self.originPos;
			self.isUpdate = false;
			self.moveDir = Vector2.zero;
			self.coolTime = 0f;
			self.ZoneScene().CurrentScene().GetComponent<OperaComponent>().Stop();
		}
		
		public static void OnJoySticPointerMove(this DlgMain self, BaseEventData baseEventData)
		{
			PointerEventData eventData = baseEventData as PointerEventData;
			Vector2 localPos;
			
			// 拖拽的实现
			RectTransformUtility.ScreenPointToLocalPointInRectangle(
				self.View.E_JoyStickBgEventTrigger.transform as RectTransform,
				eventData.position,
				eventData.pressEventCamera,
				out localPos
			);

			if ((localPos - self.originPos).magnitude >= 138.5f)
			{
				localPos = self.originPos + ((localPos - self.originPos).normalized * 138.5f);
			}

			self.moveDir = (localPos - self.originPos).normalized;
			
			// 修改位置
			self.View.E_JoyStickCenterImage.transform.localPosition = localPos;
		}

		public static void JoyMoveUpdate(this DlgMain self)
		{
			if (!self.isUpdate)
			{
				return;
			}

			if (self.moveDir == Vector2.zero)
			{
				self.lastMoveDir = Vector2.zero;
				return;
			}

			self.coolTime += Time.deltaTime;
			Vector3 camPos = (Camera.main.transform.rotation * new Vector3(self.moveDir.x, 0f, self.moveDir.y));
			Vector3 finalMoveDir = new Vector3(camPos.x, 0f, camPos.z).normalized;
			if (self.moveDir != self.lastMoveDir)
			{
				if (self.coolTime >= 0.2f)
				{
					self.ZoneScene().CurrentScene().GetComponent<OperaComponent>().JoyMove(finalMoveDir);
					self.coolTime = 0f;
				}
			}
			else
			{
				if (self.coolTime >= 0.3f)
				{
					self.ZoneScene().CurrentScene().GetComponent<OperaComponent>().JoyMove(finalMoveDir);
					self.coolTime = 0f;
				}
			}
			self.lastMoveDir = self.moveDir;
		}
	}
}
