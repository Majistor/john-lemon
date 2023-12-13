using Cinemachine;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject[] Players;
    CinemachineVirtualCamera VirtualCamera;
    GameObject myPlayer;

    private void Start()
    {
        VirtualCamera = GetComponent<CinemachineVirtualCamera>();
        Players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in Players)
        {
            PhotonView view = player.GetComponent<PhotonView>();
            if (view.IsMine)
            {
                myPlayer = player;
            }
        }
        VirtualCamera.Follow = myPlayer.transform;
        VirtualCamera.LookAt = myPlayer.transform;
    }
}