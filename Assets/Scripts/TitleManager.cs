using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField]
    private Button gameStartBtn;

    [SerializeField]
    private Button settingBtn;

    [SerializeField]
    private TextMeshProUGUI highScore;

    [SerializeField]
    private GameObject settingBoard;

    

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        gameStartBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("InGame");
        });

        settingBtn.onClick.AddListener(() =>
        {
            settingBoard.SetActive(true);
        });

        highScore.text = $"{PlayerPrefs.GetInt("Score", 0)}";
    }
}