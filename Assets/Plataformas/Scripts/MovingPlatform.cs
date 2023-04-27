using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public GameObject finalPosition;
    public float speed = 0.5f;
    private Vector3 _startPos;
    private float _trackPercent = 0.0f;
    private int _direction = 1;

    void Start()
    {
        _startPos = transform.position;
    }
    void Update()
    {
        if (finalPosition == null) return;
        _trackPercent += _direction * speed * Time.deltaTime;
        Vector3 pos = _startPos + (finalPosition.transform.position - _startPos) * _trackPercent;
        pos.z = _startPos.z;
        transform.position = pos;
        if ((_direction == 1 && _trackPercent > 0.9f) ||
             (_direction == -1 && _trackPercent < 0.1f))
        {
            _direction = -_direction;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (Application.isPlaying)
            Gizmos.DrawLine(_startPos, finalPosition.transform.position);
        else
            Gizmos.DrawLine(transform.position, finalPosition.transform.position);
    }


}


