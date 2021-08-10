using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject emergencyPanel;

    public GameObject gameOverPanel = null;

    // ゲームオーバーフラグ
    public bool gameOverFlag = false;

    public static GameManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        emergencyPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        AudioManager.instance.PlayBGM(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
