using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.PlasticSCM.Editor.WebApi;

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

    [SerializeField]
    private Image blackBoard;

    private float sceneConvertSpd = 1.5f;
    

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        gameStartBtn.onClick.AddListener(() =>
        {
            StartCoroutine(nameof(IGoInGame));
        });

        settingBtn.onClick.AddListener(() =>
        {
            settingBoard.SetActive(true);
        });

        highScore.text = string.Format("{0:#,##0}", PlayerPrefs.GetInt("Score", 0));
    }

    private IEnumerator IGoInGame()
    {
        SoundManager.Instance.Play(ESoundType.SFX, "SFX_Start_game", 0.5f);
        float current = 0;
        float percent = 0;

        while(percent < 1)
        {
            current += Time.deltaTime;
            percent = current / sceneConvertSpd;
            Color tempColor = blackBoard.color;

            tempColor.a = Mathf.Lerp(0, 1, percent);

            blackBoard.color = tempColor;

            yield return null;
        }
        SceneManager.LoadScene("InGame");
        yield break;
    }
}