﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CharacterController : MonoBehaviourPun
{
    
    Character _character;
    private void Awake()
    {
        //Para que haya un unico controller
        if(!photonView.IsMine) Destroy(this);       
        _character = GetComponent<Character>();
    }
   
    void Update()
    {
        //Movimiento y salto
        var x = Input.GetAxis("Horizontal");        
        Vector2 dir = new Vector2(x, 0);
        _character.Move(dir.normalized);
        if(Input.GetKeyDown(KeyCode.Space)||Input.GetKeyDown(KeyCode.W))
        {            
            _character.Jump();
        }
    }
   
}