using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ESoundType
{
    BGM,
    SFX,
}
public class SoundManager : Singleton<SoundManager>
{
    [Tooltip("BGM담는 List")]
    private List<AudioClip> BGMList = new List<AudioClip>();

    [Tooltip("효과음담는 List")]
    private List<AudioClip> SFXList = new List<AudioClip>();

    [Tooltip("사운드 담는 Dictionary")]
    private Dictionary<string, AudioClip> soundDic = new Dictionary<string, AudioClip>();

    [Tooltip("현재 실행중인 오디오")]
    private List<AudioSource> audios = new List<AudioSource>();

    private void Start()
    {
        LoadSound();
        Play(ESoundType.BGM, "BGM_Title", 0.5f);
    }

    /// <summary>
    /// 사운드 가져오기
    /// </summary>
    private void LoadSound()
    {
        AudioClip[] bgmClips = Resources.LoadAll<AudioClip>("Sound/BGM");
        AudioClip[] sfxClips = Resources.LoadAll<AudioClip>("Sound/SFX");

        for (int i = 0; i < bgmClips.Length; i++)
        {
            BGMList.Add(bgmClips[i]);
        }
        for (int i = 0; i < sfxClips.Length; i++)
        {
            SFXList.Add(sfxClips[i]);
        }

        for (int i = 0; i < BGMList.Count; i++)
        {
            soundDic.Add(BGMList[i].name, BGMList[i]);
        }

        for (int i = 0; i < SFXList.Count; i++)
        {
            soundDic.Add(SFXList[i].name, SFXList[i]);
        }
    }

    public void Play(ESoundType type, string name, float volume = 0.5f, float pitch = 1)
    {
        GameObject audioSourceObj = CreateSoundObject(type, name);

        AudioSource source = audioSourceObj.GetComponent<AudioSource>();

        source.clip = soundDic[name];
        source.PlayOneShot(source.clip);
    }

    private GameObject CreateSoundObject(ESoundType type, string name)
    {
        GameObject obj = new GameObject();
        obj.AddComponent<AudioSource>();

        obj.name = name;

        if (type == ESoundType.BGM)
        {
            obj.GetComponent<AudioSource>().loop = true;
        }
        else
        {
            obj.GetComponent<AudioSource>().loop = false;
        }

        audios.Add(obj.GetComponent<AudioSource>());

        return obj;
    }

    /// <summary>
    /// 현재 실행중인 오디오 음소거
    /// </summary>
    /// <param name="mute"></param>
    public void Mute(bool mute)
    {
        if (audios.Count == 0) return;

        if (mute == true)
        {
            audios.ForEach(item => item.volume = 0);
        }
        else
        {
            audios.ForEach(item => item.volume = 0.5f);
        }
    }

    public void DestroyMusic()
    {
        
    }

}
