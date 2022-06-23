using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class BadFloor : MonoBehaviourPun
{
    public HybridCharacter _char;
    public Transform respawn;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var character = collision.GetComponent<HybridCharacter>();
        _char = character;
        if (PhotonNetwork.IsMasterClient)
        {


            if (character != null)
            {
                var photonViewCharacter = character.GetComponent<PhotonView>();
                var Tped = photonViewCharacter.Owner;
                //Si el jugador triggerea con los pinches lo manda al inicio nuevamente
                photonView.RPC("TeleportBack", RpcTarget.All, Tped);
            }
        }
    }
    [PunRPC]
    void TeleportBack(Player player)
    {
        if (player == PhotonNetwork.LocalPlayer)
        {
            _char.transform.position = respawn.transform.position;
        }
        else
        {
            _char.transform.position = respawn.transform.position;
        }
        //Cambiar por ID
    }
}
