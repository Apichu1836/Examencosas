using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public SpawnPoint playerSpawnPoint;
    void Start()
    {
        SetupScene();
    }

    void SetupScene()
    {
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        if (playerSpawnPoint != null)
        {
            GameObject player = playerSpawnPoint.SpawnObject();
            CameraManager.Instance.virtualCamera.Follow = player.transform;
        }
    }
}
