using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Voice.Unity;

public class OptionMic : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public Recorder recorder;
    private void Start()
    {
        //Creo una lista con los microfones que detecta unity en microphones.devices y la agrego al dropdown
        List<string> list = new List<string>();
        string [] devices = Microphone.devices;
        if (devices.Length != 0)
        {
            for (int i = 0; i < devices.Length; i++)
            {
                list.Add(devices[i]);
            }
            dropdown.AddOptions(list);
            //seteo siempre el indice 0 
            SetMic(0);
        }        
       
    }
    public void SetMic(int i)
    {
        string[] devices = Microphone.devices;
        if (devices.Length > i)
        {
            //Tengo que setear el microfono que va a usar el recorder
            recorder.UnityMicrophoneDevice = devices[i];
        }
        
    }
}
