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

        Debug.Log("���Đ��F " + player.GekituiCount + " �|�C���g�F " + player.Point + " �o�ߎ��ԁF " + endTime);

        int score = player.Point * endTime * player.GekituiCount;

        return score;
    }

}
