using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Spawn : MonoBehaviourPunCallbacks
{
    
    public GameObject JohnLemon;
    void Start()
    {
        PhotonNetwork.Instantiate(JohnLemon.name, new Vector3(Random.Range(-11.55f, -7.82f), -0.073f, Random.Range(-6.24f, 2.47f)), Quaternion.identity);
    }

   
}
