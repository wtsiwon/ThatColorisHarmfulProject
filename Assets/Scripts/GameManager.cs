using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [Tooltip("최소 속도")]
    private float minSpd;

    [SerializeField]
    [Tooltip("최대 속도")]
    private float maxSpd;

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

    }

    void Update()
    {

    }

    private void OnDie()
    {

    }
}
