using Photon.Pun;
using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Photon.Realtime;

public class ScoreUi : MonoBehaviour
{
    public GameObject Row;

    public void instantiate()
    {
        Player[] scores = (from player in PhotonNetwork.PlayerList orderby player.GetScore() descending select player).ToArray();
        for (int i = 0; i < scores.Length; i++)
        {
            RowUi row = Instantiate(Row, transform).GetComponent<RowUi>();
            row.rank.text = (i + 1).ToString();
            row.name.text = scores[i].NickName;
            row.score.text = scores[i].GetScore().ToString();
        }
    }
}