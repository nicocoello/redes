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
    //Guardo al cliente como servidor
    Player _server;
    Dictionary<Player, HybridCharacter> _characters = new Dictionary<Player, HybridCharacter>();
    private void Awake()
    {
        //El servidor sera el masterclient
        _server = PhotonNetwork.MasterClient;
    }
    [PunRPC]
    public void InstantiatePlayer(Player client)
    {
        GameObject obj = PhotonNetwork.Instantiate(folderPrefabs + "/" + _prefab.name, _spawnPoint.position, Quaternion.identity);
        HybridCharacter character = obj.GetComponent<HybridCharacter>();
        _characters[client] = character;
        int ID = character.photonView.ViewID;
        photonView.RPC("RequestRegisterPlayer", RpcTarget.Others, client, ID);
        //Nicknames
        var nick = PhotonNetwork.Instantiate("PlayerNickName", _point.position, _point.rotation);
        nick.GetComponent<PlayerNickNames>().SetNick(PhotonNetwork.LocalPlayer.NickName, ID);
    }  
    //Esto es para que todos los clientes tengan registro de los demas jugadores
    [PunRPC]
    public void RequestRegisterPlayer(Player client, int ID)
    {
        PhotonView pv = PhotonView.Find(ID);
        if (pv == null) return;        
        var character = pv.GetComponent<HybridCharacter>();
        if (character == null) return;
        _characters[client] = character;
    }
    [PunRPC]
    public void RequestMove(Player client, Vector2 dir)
    {
        if (_characters.ContainsKey(client))
        {
            var character = _characters[client];
            character.Move(dir.normalized);            
        }
    }
    [PunRPC]
    public void RequestJump(Player client)
    {
        if (_characters.ContainsKey(client))
        {
            var character = _characters[client];
            character.Jump();
        }
    }
    [PunRPC]
    public void RequestGetPlayer(Player client)
    {
        if (_characters.ContainsKey(client))
        {
            var character = _characters[client];
            var ID = character.photonView.ViewID;
            photonView.RPC("SetPlayer", client, ID);
        }
    }
    [PunRPC]
    public void SetPlayer(int ID)
    {

        PhotonView pv = PhotonView.Find(ID);
        //Si da null retorno para evitar errores
        if (pv == null) return;
        //Obtengo tanto el caracter como el controlador
        var character = pv.GetComponent<HybridCharacter>();
        if (character == null) return;
        var controller = GameObject.FindObjectOfType<HybridController>();
        //Le paso el character al controlador
        if (controller == null) return;
        controller.SetCharacter = character;
    }
    
    //Obtengo al player privado con esta propiedad
    public Player GetPlayerServer => _server;
}
