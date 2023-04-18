using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitBtn : MonoBehaviour
{
    [SerializeField]
    private GameObject quitObject;

    private Button btn;
    void Start()
    {
        btn = GetComponent<Button>();

        btn.onClick.AddListener(() =>
        {
            quitObject.SetActive(false);
        });
    }

}
