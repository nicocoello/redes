using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CameraFollow : MonoBehaviourPun
{
    
    //Objeto que quiero seguir
    GameObject _obj;

    // z -10
    public Vector3 offset;

    //Cuanto delay en el follow
    [Range(1, 10)]
    public float smoothing;
   
    private void Update()
    {
        //Si no tengo el objeto no hago nada
        if (_obj == null) return;
        if (photonView.IsMine)
        {
            transform.position = _obj.transform.position + offset;
        }
    }
    public void SetFollow(GameObject obj)
    {
        _obj = obj;
        photonView.RPC("Follow", RpcTarget.AllBuffered);
    }
    [PunRPC]
    public void Follow()
    {        
        Vector3 targetPosition = transform.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.fixedDeltaTime);
        transform.position = smoothPosition;
    }
    
   
}
