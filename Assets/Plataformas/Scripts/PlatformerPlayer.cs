using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerPlayer : MonoBehaviour
{
    public float speed = 4.5f;
    private Rigidbody2D _body;
    private Animator _anim;

    public float jumpForce = 10f;

    private BoxCollider2D _box;

    private (Vector2, Vector2) getGroundCheckCorners()
    {
        Vector3 max = _box.bounds.max;
        Vector3 min = _box.bounds.min;
        Vector2 corner1 = new Vector2(max.x, min.y - .1f);
        Vector2 corner2 = new Vector2(min.x, min.y - .2f);
        return (corner1, corner2);
    }

    public Collider2D getGroundObject
    {
        get
        {
            var (corner1, corner2) = getGroundCheckCorners();
            return Physics2D.OverlapArea(corner1, corner2);
        }
    }
    public bool grounded
    {
        get
        {
            return (getGroundObject != null);
        }
    }
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _anim= GetComponent<Animator>();
        _box = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        float deltaX = Input.GetAxisRaw("Horizontal") * speed;

        Vector2 movement = new Vector2(deltaX, _body.velocity.y);
        _body.velocity = movement;

        _body.gravityScale = (
            grounded && 
            Mathf.Approximately(Mathf.Abs(deltaX), 0f) && Mathf.Abs(_body.velocity.y) < 0.1f)
            ? 0f : 1f;

        if (grounded && Input.GetButtonDown("Jump"))
        {
            Jump(jumpForce);
        }


        MovingPlatform platform = getGroundObject?.GetComponent<MovingPlatform>();
        Vector3 pScale = Vector3.one;
        if (platform != null)
        {
            transform.parent = platform.transform;
            pScale = platform.transform.localScale;
        }
        else
        {
            transform.parent = null;
        }

        _anim.SetFloat("speed", Mathf.Abs(deltaX));
        if (!Mathf.Approximately(deltaX, 0f))
        {
            transform.localScale = new Vector3(Mathf.Sign(deltaX)/pScale.x, 1f/pScale.y, 1f);
        }

        _anim.SetBool("jumping", !grounded);

    }

    public void Jump(float force, bool resetVerticalVelocity = false)
    {
        if (resetVerticalVelocity)
        {
            _body.velocity = new Vector2(_body.velocity.x, 0f);
        }
        _body.AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }
    private void OnDrawGizmos()
    {
        if (_box != null)
        {
            if (grounded) 
                Gizmos.color = Color.green;
            else
                Gizmos.color = Color.red;
            var (c1, c2) = getGroundCheckCorners();
            Gizmos.DrawCube((c1 + c2) * 0.5f, c2 - c1);
        }
    }

}