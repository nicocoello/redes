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
    GameObject _obj;
    public void SetNick(string name, GameObject obj)
    {
        playerNicknames.text = name;
        _obj = obj;
        photonView.RPC("UpdateNickName", RpcTarget.OthersBuffered, name);
    }    
    private void Update()
    {
        //Si no tengo el objeto no hago nada
        if (_obj == null) return;
        if (photonView.IsMine)
        {
         transform.position = _obj.transform.position + offset;
        }
    }
    [PunRPC]
    public void UpdateNickName(string name)
    {
        playerNicknames.text = name;        
    }
}
