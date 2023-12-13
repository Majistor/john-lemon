using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;

public class collectibles : MonoBehaviourPunCallbacks
{
    public TMP_Text text1;
    float Count;
    PhotonView pv;
    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localRotation = Quaternion.Euler(90f, 100f * Time.deltaTime, 0f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.gameObject.GetPhotonView().IsMine)
            {
                other.gameObject.GetPhotonView().Owner.AddScore(1);
                Count++;
                Display();
            }
            Destroy(gameObject);
        }
    }
    void Display()
    {
        text1.text = Count.ToString("00");
    }
}
