using System;
using UnityEngine;

namespace Common.UnityLogic.Ecs.Components.Camera
{
    [Serializable]
    public struct CameraFollowingComponent
    {
        public Vector3 Offset;
    }
}