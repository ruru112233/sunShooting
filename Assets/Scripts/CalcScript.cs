using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CalcScript
{

    // スコアの計算
    public static int ScoreCalc()
    {
        TimeController timeController = GameObject.FindWithTag("TimeController").GetComponent<TimeController>();
        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();

        int endTime = (int)timeController.CurTime;

        Debug.Log("撃墜数： " + player.GekituiCount + " ポイント： " + player.Point + " 経過時間： " + endTime);

        int score = player.Point * endTime * player.GekituiCount;

        return score;
    }

}
