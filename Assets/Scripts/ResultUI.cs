using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ResultUI : MonoBehaviour
{
    [SerializeField]
    private Button goTitle;

    [SerializeField]
    private TextMeshProUGUI resultScoreText;

    [SerializeField]
    private Vector3 startPosition;

    [SerializeField]
    private Vector3 targetPosition;

    [SerializeField]
    private float moveTime;
    private void Start()
    {
        AddListener();
    }

    private void AddListener()
    {
        goTitle.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Title");
        });
    }

    public void SetResultBoard()
    {
        StartCoroutine(nameof(IResultMove));
        resultScoreText.text = $"{PlayerPrefs.GetInt("Score", 0)}";
    }

    private IEnumerator IResultMove()
    {
        float current = 0;
        float percent = 0;
        Vector3 startPos = startPosition;
        Vector3 endPos =  targetPosition;

        Vector3 tempPos;
        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / moveTime;

            tempPos = Vector3.Lerp(startPos, endPos, percent);

            transform.position = tempPos;

            yield return null;
        }

    }
}