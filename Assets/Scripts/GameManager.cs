﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum EButtonType
{
    Pass,
    Break,
}
public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private List<Image> hpUI = new List<Image>();

    [SerializeField]
    [Tooltip("옆으로 넘기는 버튼")]
    private Button passBtn;

    [SerializeField]
    [Tooltip("물건 부수는 버튼")]
    private Button breakBtn;

    private int hp;
    public int Hp
    {
        get => hp;
        set
        {
            hp = value;
            UpdateHpUI();
            if(hp <= 0)
            {
                OnDie();
            }

        }
    }

    private void UpdateHpUI()
    {
        for (int i = 0; i < hpUI.Count; i++)
        {
            hpUI[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < hp; i++)
        {
            hpUI[i].gameObject.SetActive(true);
        }
    }

    [SerializeField]
    [Tooltip("속도 증가량")]
    private float spdIncrement;

    [Tooltip("속도가 증가하는 점수")]
    public int speedIncreasePoint;

    private int spdLevel;
    public int SpdLevel
    {
        get => spdLevel;
        set
        {
            spdLevel = value;
        }
    }

    private float objFallingSpd;

    public float ObjSFallingSpd 
    {
        get
        {
            return objFallingSpd;

        }
    }

    private int score;
    public int Score
    {
        get => score;
        set
        {
            score = value;
        }
    }

    [Tooltip("현재 떨어지고 있는 오브젝트")]
    [SerializeField]
    private Obj currentFallingObj;
    public Obj CurrentFallingObj
    {
        get => currentFallingObj;
        set
        {
            currentFallingObj = value;
            if (currentFallingObj == null) 
            {
                ObjSpawner.Instance.ObjSpawn();
            }
        }
    }

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
        ObjSpawner.Instance.isObjectSpawn = true;

    }

    void Start()
    {
        AddListener();
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
        if (currentFallingObj == null) return;
        FallingObjControl(EButtonType.Break);
    }

    private void FallingObjControl(EButtonType type)
    {
        if(type == EButtonType.Pass)
        {
            if(CheckCurrentFallingObj() == EObjType.Other)
            {
                Pass();
            }
            else
            {
                Drop();
            }
        }
        else if(type == EButtonType.Break)
        {
            if(CheckCurrentFallingObj() == EObjType.Other)
            {
                Drop();
            }
            else
            {
                Break();
            }
        }
    }

    public void Pass()
    {
        currentFallingObj.State = EObjState.Pass;
        currentFallingObj = null;
    }

    public void Break()
    {
        currentFallingObj.State = EObjState.Break;
        currentFallingObj = null;
    }

    public void Drop()
    {
        currentFallingObj.State = EObjState.Drop;
        currentFallingObj = null;
    }

    private EObjType CheckCurrentFallingObj()
    {
        EObjType type = currentFallingObj.type;
        return type;
    }


    void Update()
    {

    }

    private void OnDie()
    {
        
    }
}
