using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultUI : MonoBehaviour
{
    [SerializeField]
    private Button resume;

    [SerializeField]
    private Button goTitle;

    private void Start()
    {
        AddListener();
    }

    private void OnEnable()
    {
        GameManager.Instance.CurrentFallingObj.timeScale = 0;
    }

    private void OnDisable()
    {
        GameManager.Instance.CurrentFallingObj.timeScale = 1;
    }

    private void AddListener()
    {
        resume.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });

        goTitle.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Title");
        });
    }
}