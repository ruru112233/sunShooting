using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField]
    private Button gameStartButton = null
                 , playManualButton = null
                 , closeButton = null;

    [SerializeField]
    private GameObject manualPanel = null;

    // Start is called before the first frame update
    void Start()
    {
        gameStartButton.onClick.SetListener(GameStartButton);
        playManualButton.onClick.SetListener(GameManualButton);
        closeButton.onClick.SetListener(CloseButton);
        AudioManager.instance.PlayBGM(0);
        manualPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ゲームスタート
    void GameStartButton()
    {
        AudioManager.instance.PlaySE(4);
        SceneManager.LoadScene("GameScene");
    }

    // 遊び方
    void GameManualButton()
    {
        AudioManager.instance.PlaySE(4);
        manualPanel.SetActive(true);
    }

    void CloseButton()
    {
        AudioManager.instance.PlaySE(4);
        manualPanel.SetActive(false);
    }
}
