using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class DestroyingBox : MonoBehaviourPun

{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            var character = collision.GetComponent<Character>();
            if (character != null)
            {
                var photonViewCharacter = character.GetComponent<PhotonView>();
                var Contact = photonViewCharacter.Owner;
                photonView.RPC("SpeedPower", RpcTarget.All, Contact);
            }
        }
    }
    [PunRPC]
    void SpeedPower(Player player)
    {
        Destroy(gameObject);
    }
}
