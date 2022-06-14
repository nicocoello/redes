using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Photon.Chat;
using ExitGames.Client.Photon;
using Photon.Pun;
using TMPro;

public class ChatManager : MonoBehaviour , IChatClientListener
{
    public TextMeshProUGUI content;
    public TMP_InputField inputField;
    public ScrollRect scrollRect;
    public int MaxLines;
    ChatClient _chatClient;
    string[] _channels;
    string[] _chats;
    int _currentChat;
    float _scrollLimit = 0.2f;
    Dictionary<string, int> _chatDic = new Dictionary<string, int>();
    public Action OnSelect = delegate { };
    public Action OnDeselect = delegate { };
    void Start()
    {
        if (!PhotonNetwork.IsConnected) return;
        _channels = new string[] { "World", PhotonNetwork.CurrentRoom.Name };
        _chats = new string[2];
        //Seteo los valores del diccionario
        _chatDic["World"] = 0;
        _chatDic[PhotonNetwork.CurrentRoom.Name] = 1;
        //Creo el chat client y toma como referencia esta clase para reproducir los callbacks
        _chatClient = new ChatClient(this);
        // Me conecto
        _chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, PhotonNetwork.AppVersion, new AuthenticationValues(PhotonNetwork.LocalPlayer.NickName));
    }
    void Update()
    {
        //updatea el chat
        _chatClient.Service();
    }
    void UpdateChatUI()
    {       
        content.text = _chats[_currentChat];
        //Limitar la cantidad de lineas y que se vayan borrando
        if(content.textInfo.lineCount > MaxLines)
        {
            StartCoroutine(WaitToDeleteLines());           
        }
        //Para ir actualizando el scroll
        if (scrollRect.verticalNormalizedPosition <_scrollLimit)
        {
            StartCoroutine(WaitToScroll());
        }
    }
    IEnumerator WaitToScroll()
    {
        //Cuando termine el frame hago el scroll
        yield return new WaitForEndOfFrame();
        scrollRect.verticalNormalizedPosition = 0;
    }
    IEnumerator WaitToDeleteLines()
    {
        //Cuando termine el frame hago el scroll
        yield return new WaitForEndOfFrame();
        for (int i = 0; i < content.textInfo.lineCount - MaxLines; i++)
        {
            var index = _chats[_currentChat].IndexOf("\n");
            //Corto el texto a partir del indice + 1
            _chats[_currentChat] = _chats[_currentChat].Substring(index + 1);
        }
        content.text = _chats[_currentChat];
    }
    public void SendChat()
    {
        if (string.IsNullOrEmpty(inputField.text) || string.IsNullOrWhiteSpace(inputField.text)) return;
        
        //Mensaje privado
        string[] words = inputField.text.Split(' ');
        if (words[0] == "/w" && words.Length > 2)
        {
            // -2 porque saco el comando y target
            _chatClient.SendPrivateMessage(words[1], string.Join(" ", words, 2, words.Length - 2));
        }
        else
        {
            //Estoy mandando el mensaje por el canal pero no dibujandolo
            _chatClient.PublishMessage(_channels[_currentChat], inputField.text);
        }
        //limpio el texto del inputfield
        inputField.text = "";
        //Para seleccionar y deseleccionar
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(inputField.gameObject);
    }
    void IChatClientListener.DebugReturn(DebugLevel level, string message)
    {
      
    }

    void IChatClientListener.OnChatStateChange(ChatState state)
    {
        
    }

    void IChatClientListener.OnConnected()
    {
        print("Connected to Chat");
        //Nos suscribimos a los canales que le pasamos
        _chatClient.Subscribe(_channels);
    }

    void IChatClientListener.OnDisconnected()
    {
        print("Disconnected from Chat");
    }

    void IChatClientListener.OnGetMessages(string channelName, string[] senders, object[] messages)
    {
       for (int i =0; i < senders.Length; i++)
        {
            string color;
            //Cambio el color si yo mando el mensaje
            if(senders[i] == PhotonNetwork.NickName)
            {
                color = "<color=blue>";
            }
            else
            {
                color = "<color=yellow>";
            }
            //diferencio el indice
            int indexChat = _chatDic[channelName];
            //agrego el mensaje
            _chats[indexChat] += color + senders[i] + ": " + "</color>"+ messages[i] + "\n";
        }
        UpdateChatUI();
    }

    void IChatClientListener.OnPrivateMessage(string sender, object message, string channelName)
    {
      for (int i =0; i< _chats.Length; i++)
        {
            _chats[i] += "<color=purple>" + sender + ": " + "</color>" + message + "\n";
        }
        UpdateChatUI();
    }

    void IChatClientListener.OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
       
    }

    void IChatClientListener.OnSubscribed(string[] channels, bool[] results)
    {
        //nos llega la info de que nos conectamos a la sala y la pegamos en el chat
        for (int i = 0; i < channels.Length; i++)
        {
            _chats[0] += "<color=green>" + "Connected to channel: "+channels[i]+ "</color>" +"\n";
        }
        UpdateChatUI();
    }

    void IChatClientListener.OnUnsubscribed(string[] channels)
    {
        
    }

    void IChatClientListener.OnUserSubscribed(string channel, string user)
    {
       
    }

    void IChatClientListener.OnUserUnsubscribed(string channel, string user)
    {
        
    }

   public void SelectedChat()
    {
        OnSelect();
    }

    public void DeselectedChat()
    {
        OnDeselect();
    }
}
