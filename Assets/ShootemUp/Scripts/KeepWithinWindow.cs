using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepWithinWindow : MonoBehaviour
{
    private float objectWidth;
    private float objectHeight;
    void Start()
    {
        SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();
        objectWidth = sr.bounds.extents.x; //extents = width / 2
        objectHeight = sr.bounds.extents.y; //extents = height / 2
    }
    void LateUpdate()
    {
        Vector2 camPos = Camera.main.transform.position;
        Vector2 camSize = new Vector2(Camera.main.aspect, 1.0f) * Camera.main.orthographicSize;
        Vector3 myPos = transform.position;
        myPos.x = Mathf.Clamp(myPos.x, camPos.x - camSize.x + objectWidth, camPos.x + camSize.x - objectWidth);
        myPos.y = Mathf.Clamp(myPos.y, camPos.y - camSize.y + objectHeight, camPos.y + camSize.y - objectHeight);
        transform.position = myPos;
    }
}

