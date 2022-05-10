﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CharacterView : MonoBehaviourPun
{
    Rigidbody2D _rb;
    Animator _anim;
    private void Awake()
    {      
         _rb = GetComponent<Rigidbody2D>();
         _anim = GetComponent<Animator>();
    }
    private void Start()
    {   
        //OthersBuffered es para todos los demas que se conecten y para los que estan en ese momento.
        CallChangeColor(Color.yellow, RpcTarget.OthersBuffered);
        CallChangeColor(Color.blue,photonView.Owner);        
    }
    private void Update()
    {
        //All porque es una informacion que se va pisando constantemente.
        //El original obtiene la velocidad y le dice a los demas cual es para que la seteen.
        if (!photonView.IsMine) return;
        float speed = _rb.velocity.magnitude;
        photonView.RPC("SetSpeed", RpcTarget.All,speed);        
        
    }
    public void CallChangeColor(Color color, RpcTarget targets)
    {
        Vector3 infoColor = new Vector3(color.r, color.g, color.b);
        photonView.RPC("ChangeColor", targets, infoColor);
    }
    public void CallChangeColor(Color color, Player target)
    {
        Vector3 infoColor = new Vector3(color.r, color.g, color.b);
        photonView.RPC("ChangeColor", target, infoColor);
    }
    [PunRPC]
    public void SetSpeed(float speed)
    {
        _anim.SetFloat("Speed", speed);
    }  


}
