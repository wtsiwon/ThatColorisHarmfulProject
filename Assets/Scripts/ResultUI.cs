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
        Time.timeScale = 0.1f;
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
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