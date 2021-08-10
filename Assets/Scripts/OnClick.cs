using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OnClick : MonoBehaviour
{
    [SerializeField]
    private Button reTryButton = null
                 , titleButton = null;

    // Start is called before the first frame update
    void Start()
    {
        reTryButton.onClick.SetListener(OnReTry);
        titleButton.onClick.SetListener(OnTitle);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // リトライする
    void OnReTry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        AudioManager.instance.PlaySE(4);
    }

    // タイトルに戻る
    void OnTitle()
    {
        SceneManager.LoadScene("TitleScene");
        AudioManager.instance.PlaySE(4);
    }
}
