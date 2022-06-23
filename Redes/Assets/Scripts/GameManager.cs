using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    public int playersAmount;
    public GameObject wall;
    //Countdown
    float _currentTime;
    float _updateTime;   
    public float updatingTime;
    public float startingTime = 10f;
    public Text countDownText;   


    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            var playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
            if (playerCount >= playersAmount)
            {  
                //Una vez que haya 2 jugadores se destruye la pared invisible
                photonView.RPC("StartGame", RpcTarget.All);             
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
    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            _currentTime = startingTime;
        }
        else
        {
            var client = PhotonNetwork.LocalPlayer;
            photonView.RPC("RequestTime", RpcTarget.MasterClient,client);
        }
    }
    
    private void Update()
    {
        _currentTime -= Time.deltaTime;
        countDownText.text = _currentTime.ToString("0");
        if (PhotonNetwork.IsMasterClient)
        {
            _updateTime -= Time.deltaTime;
            if(_updateTime <= 0)
            {
                photonView.RPC("UpdateTime", RpcTarget.Others, _currentTime);
                _updateTime = updatingTime;
            }
            if (_currentTime <= 0)
            {
                _currentTime = 0;
                Time.timeScale = 0f;
            }
        }
        
    }
    [PunRPC]
    public void UpdateTime(float time)
    {
        _currentTime = time;
    }
    [PunRPC]
    public void RequestTime(Player client)
    {
        photonView.RPC("UpdateTime", client, _currentTime);
    }
}
