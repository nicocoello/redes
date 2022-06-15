using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Voice.Unity;
using Photon.Voice.PUN;

public class UIMic : MonoBehaviour
{
    public Sprite micOn;
    public Sprite micOff;
    public Image micImg;
    Recorder _recorder;
    private void Start()
    {
        _recorder = PhotonVoiceNetwork.Instance.PrimaryRecorder;
    }
    private void Update()
    {
        OnMicChange();
    }
    public void OnMicChange()
    {
        //Si esta prendido pongo la imagen de encendido si no pongo la de apagado
        if (_recorder.TransmitEnabled)
        {
            micImg.sprite = micOn;
        }
        else
        {
            micImg.sprite = micOff;
        }
    }
}
