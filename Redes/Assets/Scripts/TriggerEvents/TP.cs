using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class TP : MonoBehaviourPun
{   
    public Character _char;
    public Transform destiny;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var character = collision.GetComponent<Character>();
        _char = character;
        if (PhotonNetwork.IsMasterClient)
        {
            

            if (character != null)
            {
                var photonViewCharacter = character.GetComponent<PhotonView>();
                var Tped = photonViewCharacter.Owner;
                //Al triggerear transporta de un tp al otro al jugador
                photonView.RPC("Teleport", RpcTarget.All, Tped);
                
            }
        }
    }
    [PunRPC]
    void Teleport(Player player)
    {
        if (player == PhotonNetwork.LocalPlayer)
        {
            _char.transform.position = destiny.transform.position;            
        }
        else
        {
            _char.transform.position = destiny.transform.position; 
        }
    }
}

