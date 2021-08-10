using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    [SerializeField]
    private Text timeText = null;

    float curTime = 0;

    public float CurTime
    {
        get { return curTime; }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.gameOverFlag) curTime += Time.deltaTime;
        
        DateTimeInGame dateTimeInGame = RealTimeToGameTime(curTime);

        timeText.text = TimeTextView(dateTimeInGame);

    }

    public static DateTimeInGame RealTimeToGameTime(float time_realSec)
    {

        int second = (int)(time_realSec);

        int minute = second / 60;
        second %= 60;

        return new DateTimeInGame
        {
            minute = minute,
            second = second,
        };
    }

    public string TimeTextView(DateTimeInGame dateTimeInGame)
    {
        var date = dateTimeInGame;

        return date.minute.ToString() + "•ª" + date.second.ToString() + "•b";
    }

    public struct DateTimeInGame
    {
        public int minute;
        public int second;
    }
}
