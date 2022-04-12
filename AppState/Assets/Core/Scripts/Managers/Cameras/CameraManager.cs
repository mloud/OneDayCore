using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

namespace OneDay.Core.Cameras
{
    public class CameraManager : ABaseManager
    {
        [SerializeField] List<CinemachineVirtualCamera> cameras;
        public void SwitchToCamera(string name)
        {
            Debug.Assert(cameras.Find(x => x.Name == name), $"No such camera with name {name} exists");
            foreach(var camera in cameras)
            {
                camera.enabled = camera.Name == name;
            }
        }
    }
}