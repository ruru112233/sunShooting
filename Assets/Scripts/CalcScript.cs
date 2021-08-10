using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CalcScript
{

    // �X�R�A�̌v�Z
    public static int ScoreCalc()
    {
        TimeController timeController = GameObject.FindWithTag("TimeController").GetComponent<TimeController>();
        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();

        int endTime = (int)timeController.CurTime;

        int score = player.DropCount * endTime;

        return score;
    }

}
