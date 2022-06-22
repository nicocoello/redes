using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class HybridCharacter : MonoBehaviourPun
{
    float jumpCd = 1f;
    float timeSinceLastJump = 0f;
    public float characterSpeed;
    public float jumpForce;
    Rigidbody2D _rb;
    public bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {         
        timeSinceLastJump += Time.deltaTime;
        //Bool Grounded
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
    }
    public void Move(Vector2 dir)
    {
        dir *= characterSpeed;
        dir.y = _rb.velocity.y;
        _rb.velocity = dir;
    }

    public void Jump()
    {
        //Jump & Jump CD
        if (timeSinceLastJump > jumpCd)
        {
            timeSinceLastJump = 0;
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
    [PunRPC]
    public void ChangeColor(Vector3 infoColor)
    {
        Color color = new Color(infoColor.x, infoColor.y, infoColor.z);
        GetComponent<Renderer>().material.color = color;
    }
}
