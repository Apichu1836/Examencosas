using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eject : MonoBehaviour
{
    public float springForce = 30.0f;
    private Animator _anim;
    public bool used = false;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {   
        used = !used;
        if (other.CompareTag("Player"))
        {
            
            PlatformerPlayer player = other.GetComponent<PlatformerPlayer>();
            player.Jump(springForce, true);
            _anim.SetTrigger("extend");
        }
    }
}
