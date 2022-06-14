using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class HybridController : MonoBehaviour
{
    [SerializeField] ServerManager _server;
    [SerializeField] Player _localPlayer;
    void Start()
    {
        _localPlayer = PhotonNetwork.LocalPlayer;
        //llamo al masterclient del server manager
        Player clientServer = _server.GetPlayerServer;
        _server.photonView.RPC("InstantiatePlayer", clientServer, _localPlayer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
