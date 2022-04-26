using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CharacterView : MonoBehaviourPun
{
    private void Awake()
    {
        if (!photonView.IsMine) Destroy(this);
    }
    private void Start()
    {     
        CallChangeColor(Color.red, RpcTarget.OthersBuffered);
        CallChangeColor(Color.green,photonView.Owner);        
    }
    public void CallChangeColor(Color color, RpcTarget targets)
    {
        Vector3 infoColor = new Vector3(color.r, color.g, color.b);
        photonView.RPC("ChangeColor", targets, infoColor);
    }
    public void CallChangeColor(Color color, Player target)
    {
        Vector3 infoColor = new Vector3(color.r, color.g, color.b);
        photonView.RPC("ChangeColor", target, infoColor);
    }
}
