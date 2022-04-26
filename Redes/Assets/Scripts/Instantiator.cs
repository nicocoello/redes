using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Instantiator : MonoBehaviour
{
    public Transform point;
    public string prefabName;
    void Start()
    {
        //Instancio los diferentes prefabs
       var obj = PhotonNetwork.Instantiate(prefabName,Vector3.zero,Quaternion.identity);
       var nick = PhotonNetwork.Instantiate("PlayerNickName", point.position,point.rotation);              
       nick.GetComponent<PlayerNickNames>().SetNick(PhotonNetwork.LocalPlayer.NickName,obj);
    }
   
}
   