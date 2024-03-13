using Cinemachine;
using UnityEngine;

namespace ET
{
    [ComponentOf(typeof(Scene))]
    public class CameraFollowComponent : Entity, IAwake, IUpdate, IDestroy
    {
        public Camera MainCamera;

        public CinemachineBrain CinemachineBrain;

        public CinemachineVirtualCamera CinemachineVirtualCamera;

        public CinemachineFramingTransposer CinemachineFramingTransposer;

        public float CameraDistance;

        public float CinemachineTargetYaw;

        public float CinemachineTagetPitch;

        public bool IsEnableRotate = false;
    }
}