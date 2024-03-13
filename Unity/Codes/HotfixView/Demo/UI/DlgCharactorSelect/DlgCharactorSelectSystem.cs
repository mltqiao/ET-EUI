using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    [FriendClassAttribute(typeof(ET.DlgCharactorSelect))]
    [FriendClassAttribute(typeof(ET.RoleInfo))]
    [FriendClassAttribute(typeof(ET.RoleInfosComponent))]
    public static class DlgCharactorSelectSystem
    {

        public static void RegisterUIEvent(this DlgCharactorSelect self)
        {
            self.View.E_CharactorListLoopHorizontalScrollRect.AddItemRefreshListener(self.OnLoopRefreshHandler);
            self.View.E_EnterGameButton.AddListenerAsync(self.OnEnterGameClickHandler);
        }

        public static void ShowWindow(this DlgCharactorSelect self, Entity contextData = null)
        {
            self.AddUIScrollItems(ref self.ScrollItemCharactors, 4);
            self.View.E_CharactorListLoopHorizontalScrollRect.SetVisible(true, 4);
        }

        public static void HideWindow(this DlgCharactorSelect self)
        {
            self.RemoveUIScrollItems(ref self.ScrollItemCharactors);
        }

        public static void OnLoopRefreshHandler(this DlgCharactorSelect self, Transform transform, int index)
        {

            Scroll_Item_charactor scrollItemCharactor = self.ScrollItemCharactors[index].BindTrans(transform);

            RoleInfo roleInfo = self.ZoneScene().GetComponent<RoleInfosComponent>().GetRoleInfoByIndex(index);

            if (roleInfo == null)
            {
                scrollItemCharactor.EG_AddRoleRectTransform.SetVisible(true);
                scrollItemCharactor.EG_RoleRectTransform.SetVisible(false);
                scrollItemCharactor.E_SelectedFrameImage.SetVisible(false);
                scrollItemCharactor.E_AddRoleButton.AddListener(self.OnAddRoleClickHandler);
            }
            else
            {
                scrollItemCharactor.EG_RoleRectTransform.SetVisible(true);
                scrollItemCharactor.EG_AddRoleRectTransform.SetVisible(false);
                scrollItemCharactor.E_RoleLevelTextMeshProUGUI.text = $"Lv.{roleInfo.Level}";
                scrollItemCharactor.E_RoleNameTextMeshProUGUI.text = roleInfo.Name;
                // 删除按钮事件
                scrollItemCharactor.E_DeleteRoleButton.AddListenerAsync(() =>
                {
                    return self.OnDeleteRoleClickHandler(roleInfo.Id);
                });
                // 选中按钮事件
                scrollItemCharactor.E_TouchButton.AddListenerWithId(self.OnTouchClickHandler, roleInfo.Id);
                scrollItemCharactor.E_SelectedFrameImage.SetVisible(roleInfo.Id == self.ZoneScene().GetComponent<RoleInfosComponent>().CurrentUnitId);
            }
        }

        public static void OnAddRoleClickHandler(this DlgCharactorSelect self)
        {
            self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_CharactorSelect);
            self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_CreateCharactor);
        }

        public static async ETTask OnDeleteRoleClickHandler(this DlgCharactorSelect self, long roleId)
        {
            try
            {
                int errorCode = await LoginHelper.DeleteRole(self.ZoneScene(), roleId);
                if (errorCode != ErrorCode.ERR_Success)
                {
                    return;
                }
                self.View.E_CharactorListLoopHorizontalScrollRect.RefreshCells();
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
            }
        }

        public static void OnTouchClickHandler(this DlgCharactorSelect self, long unitId)
        {
            self.ZoneScene().GetComponent<RoleInfosComponent>().CurrentUnitId = unitId;
            self.View.E_CharactorListLoopHorizontalScrollRect.RefreshCells();
        }

        public static async ETTask OnEnterGameClickHandler(this DlgCharactorSelect self)
        {
            try
            {
                int errorCode = await LoginHelper.EnterMap(self.ZoneScene());
                if (errorCode != ErrorCode.ERR_Success)
                {
                    return;
                }
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
            }
        }
    }
}
