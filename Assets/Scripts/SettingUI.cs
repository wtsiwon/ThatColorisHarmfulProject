using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    [SerializeField]
    [Tooltip("효과음 껏다 키는 버튼 교체 SpriteList")]
    private List<Sprite> SFXBtnSpriteList = new List<Sprite>(2);

    [SerializeField]
    [Tooltip("음악 껏다 키는 버튼 교체 SpriteList")]
    private List<Sprite> musicBtnSpriteList = new List<Sprite>(2);

    [SerializeField]
    [Tooltip("스왑 Sprite를 넣을 Image")]
    private List<Image> swapImageList = new List<Image>(2);

    [SerializeField]
    [Tooltip("버튼 SpriteList")]
    private List<Sprite> swapSpriteList = new List<Sprite>();

    [SerializeField]
    [Tooltip("음악 껏다 켜는 버튼")]
    private Button musicMuteBtn;

    [SerializeField]
    [Tooltip("효과음 껏다 켜는 버튼")]
    private Button SFXMuteBtn;

    [SerializeField]
    [Tooltip("넘기기, 부수기 위치 바꾸는 버튼 == 스왑버튼")]
    private Button buttonSwapBtn;

    private bool onMusic;

    /// <summary>
    /// 음악 켜져 있는지
    /// </summary>
    public bool OnMusic
    {
        get
        {
            onMusic = PlayerPrefs.GetInt("MusicON", 0) == 1 ? true : false;
            return onMusic;
        }
        set
        {
            onMusic = value;

            int temp = onMusic == true ? 1 : 0;
            PlayerPrefs.SetInt("MusicON", temp);
            musicMuteBtn.GetComponent<Image>().sprite = musicBtnSpriteList[temp];
            SoundManager.Instance.Mute(OnMusic);
        }
    }

    private bool onSFX;

    /// <summary>
    /// 효과음이 켜져 있는지
    /// </summary>
    public bool OnSFX
    {
        get
        {
            onSFX = PlayerPrefs.GetInt("SFXON", 0) == 1 ? true : false;
            return onSFX;
        }
        set
        {
            onSFX = value;

            int temp = onSFX == true ? 1 : 0;
            PlayerPrefs.SetInt("SFXON", temp);
            SFXMuteBtn.GetComponent<Image>().sprite = SFXBtnSpriteList[temp];
            SoundManager.Instance.Mute(onSFX);
        }
    }

    private bool btnSwap;

    /// <summary>
    /// 버튼이 원래위치에서 바뀌어져 있는지
    /// </summary>
    public bool BtnSwap
    {
        get
        {
            btnSwap = PlayerPrefs.GetInt("btnSwap", 0) == 1 ? true : false;
            return btnSwap;
        }
        set
        {
            btnSwap = value;

            int temp = btnSwap == true ? 1 : 0;

            PlayerPrefs.SetInt("btnSwap", temp);

            swapImageList[0].sprite = swapSpriteList[temp];
            swapImageList[1].sprite = swapSpriteList[temp == 1 ? 0 : 1];//temp가 1이면 0을 대입 아니면 1을 대입
        }
    }


    void Start()
    {
        AddListener();
    }

    private void AddListener()
    {
        musicMuteBtn.onClick.AddListener(() =>
        {
            OnMusic = !OnMusic;
        });

        SFXMuteBtn.onClick.AddListener(() =>
        {
            OnSFX = !OnSFX;
        });

        buttonSwapBtn.onClick.AddListener(() =>
        {
            BtnSwap = !BtnSwap;
        });
    }
}