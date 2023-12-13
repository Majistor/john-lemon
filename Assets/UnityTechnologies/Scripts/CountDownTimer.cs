using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.Impl;

public class CountDownTimer : MonoBehaviour
{
    // Start is called before the first frame update
    public float timer = 120f;
    public TMP_Text timer2;
    public GameObject Leaderboard;
    bool IsGameOver = false;
    public ScoreUi scoreui;



    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            timer2.text = "Timer: " + timer.ToString("00.0");
        }
        else if (!IsGameOver)
        {
            Debug.Log("gameover");
            IsGameOver = true;
            GameOver();
        }
    }

    private void GameOver()
    {
        scoreui.instantiate();
        Leaderboard.SetActive(true);
    }
}
