using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    [SerializeField]
    private Button musicMuteBtn;

    [SerializeField]
    private Button SFXMuteBtn;

    [SerializeField]
    private Button buttonSwapBtn;

    void Start()
    {
        AddListener();
    }

    private void AddListener()
    {
        musicMuteBtn.onClick.AddListener(() =>
        {
            
        });

        SFXMuteBtn.onClick.AddListener(() =>
        {

        });

        buttonSwapBtn.onClick.AddListener(() =>
        {

        });
    }

}
