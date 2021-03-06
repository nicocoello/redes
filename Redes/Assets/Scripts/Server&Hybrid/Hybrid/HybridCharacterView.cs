using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class HybridCharacterView : MonoBehaviourPun
{
    Rigidbody2D _rb;
    Animator _anim;
    HybridCharacter _char;    
    SpriteRenderer _sprite;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();       
        _char = GetComponent<HybridCharacter>();
        _sprite = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        if (photonView.IsMine)
        {
            //OthersBuffered es para todos los demas que se conecten y para los que estan en ese momento.
            CallChangeColor(Color.yellow, RpcTarget.OthersBuffered);
            CallChangeColor(Color.blue, photonView.Owner);
        }        
    }
    private void Update()
    {
        //All porque es una informacion que se va pisando constantemente.
        //El original obtiene la velocidad y le dice a los demas cual es para que la seteen.       
        if (photonView.IsMine)
        {
            //Flip        
            float speedX = _rb.velocity.x;
            photonView.RPC("SetFlip", RpcTarget.All, speedX);
            //Move
            float speed = _rb.velocity.magnitude;
            photonView.RPC("SetSpeed", RpcTarget.All, speed);
            //Jump
            bool isJumping = _char.isGrounded;
            photonView.RPC("SetJump", RpcTarget.All, isJumping);
        }        
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
    [PunRPC]
    public void SetJump(bool isJumping)
    {
        if (isJumping == true)
        {
            _anim.SetBool("IsJumping", false);
        }
        else
        {
            _anim.SetBool("IsJumping", true);
        }
    }
    [PunRPC]
    public void SetFlip(float speedX)
    {

        if (speedX < 0)
        {
            _sprite.flipX = true;
        }
        else
        {
            _sprite.flipX = false;
        }
    }
}
