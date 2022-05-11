using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    float timeToStart = 2f;
    public int playersAmount;
    public GameObject wall;
    //Countdown
    float _currentTime;
    public float startingTime = 10f;
    public Text countDownText;


    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            var playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
            if (playerCount >= playersAmount)
            {                
                photonView.RPC("StartGame", RpcTarget.All);                
                photonView.RPC("Start", RpcTarget.All);
                photonView.RPC("Update", RpcTarget.All);
                //Si el master client ya comenzo el juego dejo la sala cerrada
                PhotonNetwork.CurrentRoom.IsOpen = false;
                PhotonNetwork.CurrentRoom.IsVisible = false;
            }           
        }
    }    
    [PunRPC]
    void StartGame()
    {
        Destroy(wall);        
    }
    [PunRPC]
    private void Start()
    {       
        _currentTime = startingTime;     
    }
    [PunRPC]
    private void Update()
    {
        _currentTime -= 1 * Time.deltaTime;
        countDownText.text = _currentTime.ToString("0");
        if (_currentTime <= 0)
        {
            _currentTime = 0;
            Time.timeScale = 0f;
        }
    }
}
