using System.Collections;
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
        //Para que se conecte usando las settings seteadas en el photonserver
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
        options.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom(inputFieldRoom.text, options, TypedLobby.Default);
        connectButton.interactable = false;
    }
    public override void OnCreatedRoom()
    {
        print("Create Room");
    }
    //Una vez unido a la sala cargo el nivel
    public override void OnJoinedRoom()
    {
        print("Join Room");
        PhotonNetwork.LoadLevel("Game");
    }
    //Si falla algo o me desconecto tengo que volver a habilitar el boton para conectarme
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        connectButton.interactable = true;
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        connectButton.interactable = true;
    }
}