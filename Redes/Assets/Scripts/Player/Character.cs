using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Character : MonoBehaviour
{
    float jumpCd = 1f;
    float timeSinceLastJump = 0f;
    public float characterSpeed;
    public float jumpForce;    
    Rigidbody2D _rb;
    //Jump
    public bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask ground;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();        
    }
    private void Update()
    {
        timeSinceLastJump += Time.deltaTime;
        //Jump
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, ground);
    }
    public void Move(Vector2 dir)
    {
        dir *= characterSpeed;
        dir.y = _rb.velocity.y;
        _rb.velocity = dir;
    }
    public void Jump()
    {
        if(timeSinceLastJump > jumpCd)
        {
            timeSinceLastJump = 0;
            _rb.velocity = Vector2.up * jumpForce;
            
        }       
    }
    //Setea que la funcion se va a reproducir en la red
    [PunRPC]
    public void ChangeColor(Vector3 infoColor)
    {
        Color color = new Color(infoColor.x, infoColor.y,infoColor.z);
        GetComponent<Renderer>().material.color = color;
    }    
}
