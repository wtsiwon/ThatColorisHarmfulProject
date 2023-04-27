using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    [SerializeField]
    [Tooltip("ȿ���� ���� Ű�� ��ư ��ü SpriteList")]
    private List<Sprite> SFXBtnSpriteList = new List<Sprite>(2);

    [SerializeField]
    [Tooltip("���� ���� Ű�� ��ư ��ü SpriteList")]
    private List<Sprite> musicBtnSpriteList = new List<Sprite>(2);

    [SerializeField]
    [Tooltip("���� Sprite�� ���� Image")]
    private List<Image> swapImageList = new List<Image>(2);

    [SerializeField]
    [Tooltip("��ư SpriteList")]
    private List<Sprite> swapSpriteList = new List<Sprite>();

    [SerializeField]
    [Tooltip("���� ���� �Ѵ� ��ư")]
    private Button musicMuteBtn;

    [SerializeField]
    [Tooltip("ȿ���� ���� �Ѵ� ��ư")]
    private Button SFXMuteBtn;

    [SerializeField]
    [Tooltip("�ѱ��, �μ��� ��ġ �ٲٴ� ��ư == ���ҹ�ư")]
    private Button buttonSwapBtn;

    private bool onMusic;

    /// <summary>
    /// ���� ���� �ִ���
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
    /// ȿ������ ���� �ִ���
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
    /// ��ư�� ������ġ���� �ٲ���� �ִ���
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
            swapImageList[1].sprite = swapSpriteList[temp == 1 ? 0 : 1];//temp�� 1�̸� 0�� ���� �ƴϸ� 1�� ����
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