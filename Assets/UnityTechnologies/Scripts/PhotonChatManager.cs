using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Chat;
using ExitGames.Client.Photon;
using Photon.Pun;
using TMPro;
using static UnityEngine.Rendering.DebugUI;


public class PhotonChatManager : MonoBehaviour, IChatClientListener
{
    #region Setup;
    //[SerializeField] GameObject joinChatButton;
    ChatClient chatClient;
    //[SerializeField] string UserID;
    bool isconnected;

    /*public void UsernameValueChanged(string valueIn)
    {
        UserID = valueIn;
    }
    public void ChatConnectedOnClick()
    {
        
        Debug.Log("Connecting");

    }*/
    #endregion Setup;
    #region General;
    [SerializeField] GameObject ChatPanel;
    string PrivateReciever = "";
    string CurrentChat;
    [SerializeField] TMP_InputField chatfield;
    [SerializeField] TMP_Text chatdisplay;
    void Start()
    {
        isconnected = true;
        chatClient = new ChatClient(this);
        chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, PhotonNetwork.AppVersion, new AuthenticationValues(PhotonNetwork.NickName));
    }

    void Update()
    {
        if (isconnected)
        {
            chatClient.Service();
        }
        if (chatfield.text != "" && Input.GetKeyDown(KeyCode.Return))
        {
            SubmitPublicChatOnClick();
            SubmitPrivateChatOnClick();
        }

    }
    public void OnConnected()
    {
        Debug.Log("Connected");
        isconnected = true;
        //joinChatButton.SetActive(false);
        chatClient.Subscribe(new string[] { "RegionChannel" });

    }
    #endregion General;
    #region PublicChat;
    public void SubmitPublicChatOnClick()
    {
        if (PrivateReciever == "")
        {
            chatClient.PublishMessage("RegionChannel", CurrentChat);
            chatfield.text = "";
            CurrentChat = "";
        }
    }
    public void TypeChatOnValueChange(string valueIn)
    {
        CurrentChat = valueIn;
    }
    #endregion PublicChat;
    #region PrivateChat;
    public void RecieverOnvalueChanged(string valueIn)
    {
        PrivateReciever = valueIn;
    }
    public void SubmitPrivateChatOnClick()
    {
        if (PrivateReciever != "")
        {
            chatClient.SendPrivateMessage(PrivateReciever, CurrentChat);
            chatfield.text = "";
            CurrentChat = "";
        }
    }
    #endregion PrivateChat;

    public GameObject cpanel;
    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        string msgs = "";
        for (int i = 0; i < senders.Length; i++)
        {
            msgs = string.Format("{0}:{1}", senders[i], messages[i]);
            chatdisplay.text += "\n" + msgs;
            Debug.Log(msgs);

        }
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        string msgs;
        msgs = string.Format("(Private){0}:{1}", sender, message);
        chatdisplay.text += "\n" + msgs;
        Debug.Log("private message" + msgs);
    }
    #region faltu_Callbacks;
    public void OnSubscribed(string[] channels, bool[] results)
    {
        ChatPanel.SetActive(true);
    }
    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        Debug.Log($"Status Update: User - {user}, Status - {status}, Got Message - {gotMessage}, Message - {message}");
    }
    public void OnUnsubscribed(string[] channels)
    {
        Debug.Log($"Unsubscribed from channels: {string.Join(", ", channels)}");
    }

    public void OnUserSubscribed(string channel, string user)
    {
        Debug.Log($"User {user} subscribed to channel {channel}");
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        Debug.Log($"User {user} unsubscribed from channel {channel}");
    }
    public void DebugReturn(DebugLevel level, string message)
    {
        Debug.Log($"DebugReturn: {level} - {message}");
    }

    public void OnChatStateChange(ChatState state)
    {
        Debug.Log($"Chat State Change: {state}");
    }

    public void OnDisconnected()
    {
        Debug.Log("Disconnected from chat");
    }
    #endregion faltu_Callbacks;
    public void PanelOpener()
    {
        if (cpanel != null)
        {
            bool isActive = cpanel.activeSelf;
            cpanel.SetActive(!isActive);
        }

    }
}