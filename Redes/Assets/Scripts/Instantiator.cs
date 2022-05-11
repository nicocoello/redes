using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Instantiator : MonoBehaviour
{
    public Transform point;
    public Transform spawnPoint;
    public Transform playerPos;
    public string prefabName;
    void Start()
    {
        //Instancio los diferentes prefabs de la carpeta resources
       var obj = PhotonNetwork.Instantiate(prefabName,spawnPoint.position,Quaternion.identity);
       var nick = PhotonNetwork.Instantiate("PlayerNickName", point.position,point.rotation);
       var cam = PhotonNetwork.Instantiate("Camera",playerPos.position,playerPos.rotation);
       nick.GetComponent<PlayerNickNames>().SetNick(PhotonNetwork.LocalPlayer.NickName,obj);
       cam.GetComponent<CameraFollow>().SetFollow(obj);
    }
   
}
   