using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class JumpPowerUp : MonoBehaviourPun
{
    bool _hasBoosted;
    public HybridCharacter _char;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var character = collision.GetComponent<HybridCharacter>();
        _char = character;
        if (PhotonNetwork.IsMasterClient && !_hasBoosted)
        {
            _hasBoosted = true;

            if (character != null)
            {
                var photonViewCharacter = character.GetComponent<PhotonView>();
                var boosted = photonViewCharacter.Owner;
                //Si el jugador agarra el power up aumenta su jump force
                photonView.RPC("JumpPowerJump", RpcTarget.All, boosted);
            }
        }
    }
    [PunRPC]
    void JumpPowerJump(Player player)
    {
        if (player == PhotonNetwork.LocalPlayer)
        {
            _char.jumpForce = 11;
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }    
}
}
