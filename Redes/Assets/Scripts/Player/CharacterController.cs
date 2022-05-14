using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CharacterController : MonoBehaviourPun
{
    
    Character _character;
    public bool isClicking;
    public bool isFlipping;
    public float inputHorizontal;  
    private void Awake()
    {
        //Para que haya un unico controller
        if(!photonView.IsMine) Destroy(this);       
        _character = GetComponent<Character>();       
    }

    void Update()
    {
        //Movimiento
        inputHorizontal = Input.GetAxis("Horizontal");
        Vector2 dir = new Vector2(inputHorizontal, 0);
        _character.Move(dir.normalized);
        //Flip        
        if (inputHorizontal > 0)
        {
            _character.transform.localScale = new Vector3(3, 3, 3);
            isFlipping = false;
        }
        if (inputHorizontal < 0)
        {
            _character.transform.localScale = new Vector3(-3, 3, 3);
            isFlipping = true;
        }
        //Jump
        if (_character.isGrounded == true && Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            _character.Jump();
        }
    }
        
}

     
 
 