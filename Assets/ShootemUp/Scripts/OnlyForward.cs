using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;

public class OnlyForward : CinemachineExtension
{
    private Vector3 prevPos = new Vector3(-1000.0f, 0.0f, 0.0f);

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam,
       CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Body)
        {
            var pos = state.RawPosition;
            pos.x = Mathf.Max(pos.x, prevPos.x);
            state.RawPosition = prevPos = pos;
        }
    }
}

