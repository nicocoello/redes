using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ServerManager : MonoBehaviourPun
{
    public string folderPrefabs = "";
    [SerializeField] GameObject _prefab;
    [SerializeField] Transform _spawnPoint;
    [SerializeField] Transform _point;
    Player _server;
    Dictionary<Player, Character> _characters = new Dictionary<Player, Character>();
    private void Awake()
    {
        //El servidor sera el masterclient
        _server = PhotonNetwork.MasterClient;
    }
    [PunRPC]
    public void InstantiatePlayer(Player client)
    {
        GameObject obj = PhotonNetwork.Instantiate(folderPrefabs + "/" + _prefab.name, _spawnPoint.position, Quaternion.identity);
        Character character = obj.GetComponent<Character>();
        _characters[client] = character;
        //PREGUNTAR AL PROFE SI ESTO ESTA BIEN
        var nick = PhotonNetwork.Instantiate("PlayerNickName", _point.position, _point.rotation);
        nick.GetComponent<PlayerNickNames>().SetNick(PhotonNetwork.LocalPlayer.NickName, obj);
    }
    
    public Player GetPlayerServer => _server;
}
