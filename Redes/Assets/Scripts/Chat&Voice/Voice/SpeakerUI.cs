using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Voice.Unity;
using Photon.Voice.PUN;

public class SpeakerUI : MonoBehaviour
{
    public Image prefab;
    public Speaker speaker;
    public Transform micPos;
    Image _refImg;
    private void Start()
    {
        _refImg = Instantiate<Image>(prefab, GameObject.Find("PlayerUI").transform);
    }
    private void Update()
    {
        if (speaker.IsPlaying)
        {
            _refImg.gameObject.SetActive(true);
        }
        else
        {
            _refImg.gameObject.SetActive(false);
        }
        _refImg.transform.position = micPos.transform.position;
    }
    private void OnDestroy()
    {
        if (_refImg != null)
        {
            Destroy(_refImg.gameObject);
        }
    }
}
    