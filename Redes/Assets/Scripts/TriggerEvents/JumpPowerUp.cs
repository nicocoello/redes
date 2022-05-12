using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class JumpPowerUp : MonoBehaviourPun
{
    bool _hasBoosted;
    public Character _char;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var character = collision.GetComponent<Character>();
        _char = character;
        if (PhotonNetwork.IsMasterClient && !_hasBoosted)
        {
            _hasBoosted = true;

            if (character != null)
            {
                var photonViewCharacter = character.GetComponent<PhotonView>();
                var boosted = photonViewCharacter.Owner;
                photonView.RPC("SpeedPowerJump", RpcTarget.All, boosted);
            }
        }
    }
    [PunRPC]
    void SpeedPowerJump(Player player)
    {
        if (player == PhotonNetwork.LocalPlayer)
        {
            _char.jumpForce = 15;
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    
}
}
