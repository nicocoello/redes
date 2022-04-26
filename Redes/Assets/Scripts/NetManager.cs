﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;


public class NetManager : MonoBehaviourPunCallbacks
{
    public Button connectButton;
    public InputField inputFieldRoom;
    public InputField inputFieldPlayer;
    private void Start()
    {
        connectButton.interactable = false;
        PhotonNetwork.ConnectUsingSettings();        
    }
    //Primer paso es conectar al master
    public override void OnConnectedToMaster()
    {
        print("ConnectedToMaster");
        PhotonNetwork.JoinLobby();
    }
    //Segundo paso es conectar al lobby
    public override void OnJoinedLobby()
    {
        print("ConnectedToLobby");
        connectButton.interactable = true;
    }
    //Tercer paso es Unirse o Crear una sala en base a su input field.
    public void Connect()
    {
        if (string.IsNullOrWhiteSpace(inputFieldRoom.text) || string.IsNullOrEmpty(inputFieldRoom.text)) return;
        if (string.IsNullOrWhiteSpace(inputFieldPlayer.text) || string.IsNullOrEmpty(inputFieldPlayer.text)) return;
        PhotonNetwork.NickName = inputFieldPlayer.text;        
        RoomOptions options = new RoomOptions();
        options.IsOpen = true;
        options.IsVisible = true;
        options.MaxPlayers = 10;
        PhotonNetwork.JoinOrCreateRoom(inputFieldRoom.text, options, TypedLobby.Default);
        connectButton.interactable = false;
    }
    public override void OnCreatedRoom()
    {
        print("Create Room");
    }
    public override void OnJoinedRoom()
    {
        print("Join Room");
        PhotonNetwork.LoadLevel("Game");
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        connectButton.interactable = true;
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        connectButton.interactable = true;
    }
}