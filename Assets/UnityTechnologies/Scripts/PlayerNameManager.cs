using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class PlayerNameManager : MonoBehaviour
{
    [SerializeField] TMP_InputField username;

    private void Start()
    {
        
        {
            username.text = "Player" + Random.Range(0, 100000).ToString("0000");
            OnUsernameInputValueChanged();
        }
    }
    public void OnUsernameInputValueChanged()
    {
        if (!string.IsNullOrEmpty(username.text))
        {
            PhotonNetwork.NickName = username.text;
            PlayerPrefs.SetString("username", username.text);
        }
    }
}