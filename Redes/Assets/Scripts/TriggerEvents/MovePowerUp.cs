using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MovePowerUp : MonoBehaviourPun
{
    bool _hasBoosted;
    public Character _char;  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var character = collision.GetComponent<Character>();
        _char = character;
        if (PhotonNetwork.IsMasterClient &&!_hasBoosted)
        {
            _hasBoosted = true;
            
            if (character != null)
            {
                var photonViewCharacter = character.GetComponent<PhotonView>();
                var boosted = photonViewCharacter.Owner;
                //Si el jugador agarra el power up aumenta su velocidad
                photonView.RPC("SpeedPower", RpcTarget.All,boosted);                
            }
        }                
    }
    [PunRPC]
    void SpeedPower(Player player) 
    {
        if (player == PhotonNetwork.LocalPlayer)
        {
            _char.characterSpeed = 4;
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }    
    }
}
