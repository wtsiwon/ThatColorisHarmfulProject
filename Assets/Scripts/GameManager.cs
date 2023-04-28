﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public enum EButtonType
{
    Pass,
    Break,
}
public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    [Tooltip("HpUIs")]
    private List<Image> hpUIList = new List<Image>();

    [Header("버튼 들")]
    [SerializeField]
    [Tooltip("옆으로 넘기는 버튼")]
    private Button passBtn;

    [SerializeField]
    [Tooltip("물건 부수는 버튼")]
    private Button breakBtn;

    [SerializeField]
    [Tooltip("결과 창")]
    private ResultUI resultboard;

    [SerializeField]
    [Tooltip("현재 떨어진고 있는 오브젝트")]
    private Obj currentFallingObj;
    public Obj CurrentFallingObj
    {
        get => currentFallingObj;
        set
        {
            currentFallingObj = value;
            if (currentFallingObj == null && isGameStart == true)
            {
                ObjSpawner.Instance.ObjSpawn();
            }
        }
    }

    private int hp = 3;
    public int Hp
    {
        get => hp;
        set
        {
            hp = value;
            if (hp <= 0)
            {
                OnDie();
            }
            CameraShake(cameraShakeTime, cameraShakeRange);
            SoundManager.Instance.Play(ESoundType.SFX, "SFX_Narack");
            UpdateHpUI();
        }
    }

    private void UpdateHpUI()
    {
        for (int i = 0; i < hpUIList.Count; i++)
        {
            hpUIList[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < hp; i++)
        {
            hpUIList[i].gameObject.SetActive(true);
        }
    }

    [SerializeField]
    [Tooltip("얼마나 오래 흔들릴것인가")]
    private float cameraShakeTime;

    [SerializeField]
    [Tooltip("얼마나 넓게 흔들릴것인가")]
    private float cameraShakeRange;


    [Header("속도")]
    [SerializeField]
    [Space(10f)]
    [Tooltip("속도 증가량")]
    private float spdIncrement;

    [Tooltip("속도가 증가하는 점수")]
    public int speedIncreasePoint;

    [Tooltip("상호작용 가능 상태일 때 속도")]
    [SerializeField]
    private float interactionSpd;
    public float InteractionSpd
    {
        get
        {
            interactionSpd = score / speedIncreasePoint;
            interactionSpd = Mathf.Clamp(interactionSpd, MINSPD, MAXSPD);

            return interactionSpd;
        }
    }

    public const float MAXSPD = 7f;

    public const float MINSPD = 2f;

    [SerializeField]
    [Tooltip("오브젝트 상호작용 상태가 아닐 때 속도")]
    private float objFallingSpd;

    public float ObjSFallingSpd
    {
        get
        {
            return objFallingSpd;
        }
    }

    [SerializeField]
    private TextMeshProUGUI scoreText;

    public int scoreIncrement;

    [SerializeField]
    [Space(10f)]
    private int score;
    public int Score
    {
        get => score;
        set
        {
            score = value;
            scoreText.text = string.Format("{0:#,##0}", score);
            PlayerPrefs.SetInt("Score", score);
        }
    }

    [SerializeField]
    [Tooltip("Player공격하는 모션")]
    private GameObject playerAttackMotion;

    [SerializeField]
    [Tooltip("Player기본 모션")]
    private GameObject playerDefaultMotion;


    [SerializeField]
    [Space(10f)]
    [Tooltip("게임 시작 카운드 다운을 위한 Text")]
    private TextMeshProUGUI beginCountText;

    [SerializeField]
    [Tooltip("FadeOut을 위한 검정색 이미지")]
    private Image blackBoard;


    [SerializeField]
    private float fadeOutTime;

    private bool isGameStart;
    public bool IsGameStart
    {
        get => isGameStart;
        set
        {
            isGameStart = value;
            SetGame();
        }
    }

    /// <summary>
    /// 게임 세팅
    /// </summary>
    private void SetGame()
    {
        ObjSpawner.Instance.ObjSpawn();
    }

    private void Start()
    {
        AddListener();
        StartCoroutine(nameof(IFadeOut));
        StartCoroutine(nameof(IUpdate));
        SoundManager.Instance.Play(ESoundType.BGM, "BGM_Ingame");
    }

    /// <summary>
    /// 여러가지 확인 하기 위해 만든 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator IUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator IFadeOut()
    {
        float current = 0;
        float percent = 0;

        Color tempColor = blackBoard.color;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / fadeOutTime;

            tempColor.a = Mathf.Lerp(1, 0, percent);

            blackBoard.color = tempColor;

            yield return null;
        }
        StartCoroutine(nameof(IBeginCount));
        yield break;
    }

    private IEnumerator IBeginCount()
    {
        beginCountText.gameObject.SetActive(true);
        for (int i = 3; i > 0; i--)
        {
            beginCountText.text = $"{i}";
            yield return new WaitForSeconds(1f);
        }
        beginCountText.gameObject.SetActive(false);

        IsGameStart = true;
    }

    /// <summary>
    /// 버튼 이벤트 등록
    /// </summary>
    private void AddListener()
    {
        passBtn.onClick.AddListener(() =>
        {
            PressPassBtn();
        });

        breakBtn.onClick.AddListener(() =>
        {
            PressBreakBtn();
        });
    }

    /// <summary>
    /// Pass버튼을 눌렀을 때
    /// </summary>
    public void PressPassBtn()
    {
        if (currentFallingObj == null) return;
        FallingObjControl(EButtonType.Pass);
    }

    /// <summary>
    /// Break버튼을 눌렀을 때
    /// </summary>
    public void PressBreakBtn()
    {
        StartCoroutine(nameof(IAttackMotion));
        if (currentFallingObj == null) return;
        FallingObjControl(EButtonType.Break);
    }

    private void FallingObjControl(EButtonType type)
    {
        if (type == EButtonType.Pass)
        {
            if (GetCurrentFallingObj() == EObjType.Other)
            {
                Pass();
                Score += scoreIncrement;
            }
            else
            {
                Drop();
                Hp -= 1;
            }
        }
        else if (type == EButtonType.Break)
        {
            if (GetCurrentFallingObj() == EObjType.Other)
            {
                Drop();
                Hp -= 1;
            }
            else
            {
                Break();
                Score += scoreIncrement;
            }
        }
    }

    /// <summary>
    /// 옆으로 넘겨야 할 때 오브젝트 상태를 pass로 바꿔줌
    /// </summary>
    public void Pass()
    {
        Obj obj = currentFallingObj;
        obj.State = EObjState.Pass;
        SoundManager.Instance.Play(ESoundType.SFX, "SFX_Numgigi", 0.8f);
    }

    /// <summary>
    /// 부숴야 할 때 오브젝트 상태를 break로 바꿔줌
    /// </summary>
    public void Break()
    {
        Obj obj = currentFallingObj;
        obj.State = EObjState.Break;
        CameraShake(cameraShakeTime, cameraShakeRange);
        SoundManager.Instance.Play(ESoundType.SFX, "SFX_Attack");
    }

    /// <summary>
    /// 떨어뜨려야 할 때 오브젝트의 상태를 Drop으로 바꿔줌
    /// </summary>
    public void Drop()
    {
        Obj obj = currentFallingObj;
        obj.State = EObjState.Drop;
    }

    /// <summary>
    /// 현재 떨어지고 있는 오브젝트의 Type을 받아옴
    /// </summary>
    /// <returns></returns>
    private EObjType GetCurrentFallingObj()
    {
        EObjType type = currentFallingObj.type;
        return type;
    }


    void Update()
    {
        ComputerInputKey();
    }
    private void ComputerInputKey()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PressPassBtn();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            PressBreakBtn();
        }
    }

    /// <summary>
    /// 공격모션
    /// </summary>
    private void AttackMotion()
    {
        StartCoroutine(nameof(IAttackMotion));
    }

    private IEnumerator IAttackMotion()
    {
        playerAttackMotion.SetActive(true);
        playerDefaultMotion.SetActive(false);

        yield return new WaitForSeconds(0.1f);

        playerAttackMotion.SetActive(false);
        playerDefaultMotion.SetActive(true);
    }

    /// <summary>
    /// 죽었을 때
    /// </summary>
    private void OnDie()
    {
        if (PlayerPrefs.GetInt("HighScore", 0) <= PlayerPrefs.GetInt("Score", 0))
        {
            PlayerPrefs.SetInt("HighScore", PlayerPrefs.GetInt("Score", 0));
        }

        PlayerPrefs.SetInt("Score", score);

        resultboard.SetResultBoard();
        SoundManager.Instance.Play(ESoundType.SFX, "SFX_Game_Over");
        isGameStart = false;
    }

    public void CameraShake(float time, float range)
    {
        StartCoroutine(ICameraShake(time, range));
    }

    private IEnumerator ICameraShake(float time, float range)
    {
        float current = 0;
        float percent = 0;

        Vector3 defaultCameraPos = new Vector3(0, 0, -10);

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / time;

            Vector3 shakePos = Random.insideUnitCircle * range;
            shakePos.z = -10;

            cam.transform.position = shakePos;


            yield return null;
            cam.transform.position = defaultCameraPos;
        }
        cam.transform.position = defaultCameraPos;
        yield break;
    }
}