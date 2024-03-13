using Cinemachine;
using UnityEngine;

namespace ET
{
    public class CameraFollowComponentAwakeSystem : AwakeSystem<CameraFollowComponent>
    {
        public override void Awake(CameraFollowComponent self)
        {
            self.Awake();
        }
    }
    
    public class CameraFollowComponentUpdateSystem : UpdateSystem<CameraFollowComponent>
    {
        public override void Update(CameraFollowComponent self)
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                {
                    self.IsEnableRotate = true;
                }
            }

            if (Input.GetMouseButtonUp(1))
            {
                self.IsEnableRotate = false;
            }

            if (self.IsEnableRotate)
            {
                self.RotateCameraView();
            }
            
            self.ScrollCameraView();
        }
    }

    public class CameraFollowComponentDestroySystem: DestroySystem<CameraFollowComponent>
    {
        public override void Destroy(CameraFollowComponent self)
        {
            GameObject.Destroy(self.CinemachineFramingTransposer);
            GameObject.Destroy(self.CinemachineVirtualCamera);
            GameObject.Destroy(self.CinemachineBrain);
            self.MainCamera = null;
        }
    }
    
    [FriendClassAttribute(typeof(ET.CameraFollowComponent))]
    public static class CameraFollowComponentSystem
    {
        public static void Awake(this CameraFollowComponent self)
        {
            self.MainCamera = Camera.main;
            self.CinemachineBrain = self.MainCamera.gameObject.AddComponent<CinemachineBrain>();
            self.CinemachineVirtualCamera = self.MainCamera.gameObject.AddComponent<CinemachineVirtualCamera>();
            self.CinemachineFramingTransposer = self.CinemachineVirtualCamera.AddCinemachineComponent<CinemachineFramingTransposer>();

            Unit unit = UnitHelper.GetMyUnitFromCurrentScene(self.DomainScene());

            self.CinemachineVirtualCamera.Follow = unit.GetComponent<GameObjectComponent>().GameObject.transform;
            self.CinemachineVirtualCamera.LookAt = unit.GetComponent<GameObjectComponent>().GameObject.transform;
            
            self.RotateCameraView();
        }

        public static void RotateCameraView(this CameraFollowComponent self)
        {
            float mouseX = Input.GetAxis("Mouse X") * 200;
            float mouseY = Input.GetAxis("Mouse Y") * 200;

            self.CinemachineTargetYaw += mouseX * Time.fixedDeltaTime;
            self.CinemachineTagetPitch -= mouseY * Time.fixedDeltaTime;

            self.CinemachineTargetYaw = self.ClampAngle(self.CinemachineTargetYaw, float.MinValue, float.MaxValue);
            self.CinemachineTagetPitch = self.ClampAngle(self.CinemachineTagetPitch, 1f, 80f);

            Quaternion targetRotation = Quaternion.Euler(self.CinemachineTagetPitch, self.CinemachineTargetYaw, 0);
            self.MainCamera.transform.rotation = targetRotation;
        }

        public static float ClampAngle(this CameraFollowComponent self, float angle, float min, float max)
        {
            if (angle < -360f)
            {
                angle += 360f;
            }

            if (angle > 360f)
            {
                angle -= 360f;
            }

            return Mathf.Clamp(angle, min, max);
        }

        public static void ScrollCameraView(this CameraFollowComponent self)
        {
            self.CameraDistance -= Input.GetAxis("Mouse ScrollWheel") * 5f;
            self.CameraDistance = Mathf.Clamp(self.CameraDistance, 5f, 20f);
            self.CinemachineFramingTransposer.m_CameraDistance = Mathf.Lerp(self.CinemachineFramingTransposer.m_CameraDistance, self.CameraDistance, Time.deltaTime * 10f);
        }
    }
}