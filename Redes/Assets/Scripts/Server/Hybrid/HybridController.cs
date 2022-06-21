using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon.Voice.Unity;
using Photon.Voice.PUN;


public class HybridController : MonoBehaviourPun
{
    [SerializeField] ServerManager _server;
    [SerializeField] Player _localPlayer;
    HybridCharacter _character;
    public float inputHorizontal;
    bool _isLocked;
    Recorder _recorder;
    private void Awake()
    {
        var chatManager = FindObjectOfType<ChatManager>();
        if (chatManager)
        {
            //me ahorro crear la funcion
            chatManager.OnSelect += () => _isLocked = true;
            chatManager.OnDeselect += () => _isLocked = false;
        }
        _recorder = PhotonVoiceNetwork.Instance.PrimaryRecorder;
    }
    void Start()
    {
        _localPlayer = PhotonNetwork.LocalPlayer;
        //llamo al masterclient del server manager
        Player clientServer = _server.GetPlayerServer;
        //_server.photonView.RPC("InstantiatePlayer", _server.GetPlayerServer, _localPlayer);
        _server.photonView.RPC("InstantiatePlayer", _localPlayer,_localPlayer);
        _server.photonView.RPC("RequestGetPlayer", _localPlayer, _localPlayer);
    }
    
    void Update()
    {
        if (_character == null) return;
        if (_isLocked) return;
        //Movimiento
        inputHorizontal = Input.GetAxis("Horizontal");
        Vector2 dir = new Vector2(inputHorizontal, 0);
        _server.photonView.RPC("RequestMove", _localPlayer, _localPlayer, dir);
        //Jump
        if (_character.isGrounded == true && Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            _server.photonView.RPC("RequestJump", _localPlayer, _localPlayer);
        }
        //Mic
        if (_recorder != null)
        {
            if (Input.GetKey(KeyCode.V))
            {
                _recorder.TransmitEnabled = true;
            }
            else
            {
                _recorder.TransmitEnabled = false;
            }
        }
    }
    public HybridCharacter SetCharacter
    {
        set
        {
            _character = value;
        }
    }
}
