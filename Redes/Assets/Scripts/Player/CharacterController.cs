using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CharacterController : MonoBehaviourPun
{
    Animator _anim;   
    Character _character;
    private void Awake()
    {
        //Para que haya un unico controller
        if(!photonView.IsMine) Destroy(this);       
        _character = GetComponent<Character>();
        _anim = GetComponent <Animator>();
    }
   
    void Update()
    {
        //Movimiento
        var x = Input.GetAxis("Horizontal");        
        Vector2 dir = new Vector2(x, 0);
        _character.Move(dir.normalized);
        
        //Jump
        if (_character.isGrounded == true && Input.GetKeyDown(KeyCode.Space)||Input.GetKeyDown(KeyCode.W))
        {            
            _character.Jump();
            _anim.SetBool("IsJumping", true);
        }        
        else
        {
            _anim.SetBool("IsJumping", false);
        }
    }
    
}

     
 
 