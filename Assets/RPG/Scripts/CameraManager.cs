using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;
public class CameraManager : Singleton<CameraManager>
{
    [HideInInspector]
    public CinemachineVirtualCamera virtualCamera;

    void Awake()
    {
        GameObject vCamGO = GameObject.FindWithTag("VirtualCamera");
        virtualCamera = vCamGO.GetComponent<CinemachineVirtualCamera>();
    }
}

