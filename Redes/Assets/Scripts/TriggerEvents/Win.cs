using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class Win : MonoBehaviourPun
{
    bool _hasWinner;
    public GameObject winnerScreen;
    public GameObject loserScreen;
    public Text textNickWinner;    


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //si soy el master y no hay un ganador, lo reproduzco
        if (PhotonNetwork.IsMasterClient && !_hasWinner)
        {
            _hasWinner = true;
            var character = collision.GetComponent<Character>();
            if(character!=null)
            {
                
                var photonViewCharacter = character.GetComponent<PhotonView>();
                var winner = photonViewCharacter.Owner;
                photonView.RPC("Victory", RpcTarget.All, winner);
            }
        }
    }
    [PunRPC]
    void Victory(Player player)
    {
        Time.timeScale = 0f;
        if (player== PhotonNetwork.LocalPlayer)
        {
            winnerScreen.SetActive(true);
            loserScreen.SetActive(false);
            textNickWinner.text = player.NickName;
        }
        else
        {
            loserScreen.SetActive(true);
            winnerScreen.SetActive(false);            
        }        
    }
}
