using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class PlayerNickNames : MonoBehaviourPun
{
    public TextMeshPro playerNicknames;
    public Vector3 offset;  
    PhotonView _id;
    GameObject _obj;
    public void SetNick(string name, int ID)
    {        
        photonView.RPC("UpdateNickName", RpcTarget.AllBuffered, name,ID);
    }    
    private void Update()
    {
        //Si no tengo el objeto no hago nada
        if (_obj == null) return;       
        transform.position = _obj.transform.position + offset;        
    }
    [PunRPC]
    public void UpdateNickName(string name, int ID)
    {
        PhotonView pv = PhotonView.Find(ID);
        _obj = pv.gameObject;
        playerNicknames.text = name;        
    }
}
