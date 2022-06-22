using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Voice.Unity;
using Photon.Voice.PUN;
using Photon.Pun;
using Photon.Realtime;

public class SpeakerUI : MonoBehaviourPun
{
    public Image prefab;
    public Speaker speaker;
    Image _refImg;
    Camera _cam;
    bool _lastPlay;
    public Vector3 offset;
    
    private void Start()
    {
        _refImg = Instantiate<Image>(prefab, GameObject.Find("PlayerUI").transform);
        _cam = Camera.main;
        _lastPlay = speaker.IsPlaying;
        _refImg.gameObject.SetActive(speaker.IsPlaying);
    }
    private void Update()
    {
        Vector3 pos = transform.position + offset;
        _refImg.transform.position = _cam.WorldToScreenPoint(pos);
        if (_lastPlay == speaker.IsPlaying) return;
        _lastPlay = speaker.IsPlaying;
        _refImg.gameObject.SetActive(speaker.IsPlaying);
               
    }
    private void OnDestroy()
    {
        if (_refImg != null)
        {
            Destroy(_refImg.gameObject);
        }
    }   
}
    