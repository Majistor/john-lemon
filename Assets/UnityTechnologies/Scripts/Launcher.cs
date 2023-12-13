using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using UnityEditor;
using System.Linq;
using UnityEngine.Windows;
using ExitGames.Client.Photon.StructWrapping;
using static UnityEngine.Rendering.DebugUI;


public class Launcher : MonoBehaviourPunCallbacks

{
    public static Launcher Instance;
    public TMP_InputField RoomNameInput;
    public TMP_Text RoomName;
    public TMP_Text errorText;
    public Transform PlayerListContent;
    public GameObject PlayerListPrefab;
    public Transform roomListContent;
    public GameObject roomListItemPrefab;
    public TMP_InputField username;
    bool hasLeft = false;
    public TMP_InputField codeInput;
    public TMP_Text codeText;
    RoomOptions roomOptions = new RoomOptions();
    Hashtable customProperties = new Hashtable();
    public int codeLength = 4;
    public string roomcode;
    RoomInfo[] rooms;
    public GameObject startGameButton;
    
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        MenuManager.Instance.OpenMenu("Loading Menu");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("ConnectedToMaster");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        if (!hasLeft)
        {
            Debug.Log("LobbyJoined");
            MenuManager.Instance.OpenMenu("Username");
        }
    }

    public void EnterUsername()
    {
        if ((!string.IsNullOrEmpty(username.text)))
        { MenuManager.Instance.OpenMenu("TITLE"); }
    }

    public void Create_Room()
    {
        MenuManager.Instance.OpenMenu("Create Room");
    }

    public void Create()
    {
        roomcode = GenerateRandomCode(codeLength);
        customProperties.Add("roomcode", "roomcode");
        string[] props = { "roomcode" };
        roomOptions.CustomRoomPropertiesForLobby = props;

        if (!string.IsNullOrEmpty(RoomNameInput.text))
        {
            MenuManager.Instance.OpenMenu("loading");
            PhotonNetwork.CreateRoom(RoomNameInput.text, roomOptions);

        }
    }
    public string GenerateRandomCode(int length)
    {

        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        System.Text.StringBuilder code = new System.Text.StringBuilder();

        System.Random random = new System.Random();

        for (int i = 0; i < length; i++)
        {

            code.Append(chars[random.Next(chars.Length)]);
        }

        return code.ToString();
    }


    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        MenuManager.Instance.OpenMenu("Error");
        errorText.text = "Room Creation Failed" + message;
    }

    public void Back()
    {
        MenuManager.Instance.OpenMenu("TITLE");
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        MenuManager.Instance.OpenMenu("room");
        RoomName.text = PhotonNetwork.CurrentRoom.Name;

        codeText.text = "Room Code: " + roomcode;


        Player[] player = PhotonNetwork.PlayerList;

        foreach (Transform child in PlayerListContent)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < player.Count(); i++)
        {
            Instantiate(PlayerListPrefab, PlayerListContent).GetComponent<PlayerListItem>().SetUp(player[i]);
        }

        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }
    public override void OnPlayerEnteredRoom(Player player)
    {
        Instantiate(PlayerListPrefab, PlayerListContent).GetComponent<PlayerListItem>().SetUp(player);
    }
    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);


        MenuManager.Instance.OpenMenu("loading");
    }

    public override void OnLeftRoom()
    {
        hasLeft = true;
        MenuManager.Instance.OpenMenu("title");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        rooms = roomList.ToArray();
        foreach (Transform trans in roomListContent)
        {
            Destroy(trans.gameObject);
        }

        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList)
                continue;
            Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
        }
    }
    public void StartGame()
    {
        PhotonNetwork.LoadLevel(1);

    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        MenuManager.Instance.OpenMenu("loading");
    }

    public void joinroom()

    {

        if (!string.IsNullOrEmpty(codeInput.text))
        {

            foreach (RoomInfo room in rooms)
            {
                if ((string)room.CustomProperties["roomcode"] == "roomcode")
                {
                    MenuManager.Instance.OpenMenu("loading");
                    PhotonNetwork.JoinRoom(room.Name);
                    break;
                }
            }



        }
    }
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }
    
}