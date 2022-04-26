using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    float timeToStart = 2f;
    public int playersAmount;
    public GameObject wall;
    
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            var playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
            if (playerCount >= playersAmount)
            {
                //Inicio la corrutina
                StartCoroutine(WaitToStart());
                //Si el master client ya comenzo el juego dejo la sala cerrada
                PhotonNetwork.CurrentRoom.IsOpen = false;
                PhotonNetwork.CurrentRoom.IsVisible = false;
            }
        }
        
    }   
    IEnumerator WaitToStart()
    {
        //Luego del time to start arranca el juego
        yield return new WaitForSeconds(timeToStart);
        photonView.RPC("StartGame", RpcTarget.All);
    }
    [PunRPC]
    void StartGame()
    {
        Destroy(wall);
    }
}
