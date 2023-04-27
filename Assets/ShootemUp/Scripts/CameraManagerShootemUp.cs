using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;
[RequireComponent(typeof(CinemachineImpulseSource))]
public class CameraManagerShootemUp : SingletonShootemUp<CameraManagerShootemUp>
{
    private CinemachineImpulseSource cameraShaker;
    void Start()
    {
        cameraShaker =
          GetComponent<CinemachineImpulseSource>();
    }
    public void ShakeCamera(int force)
    {
        cameraShaker.GenerateImpulse(force);
    }
}
