using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
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
    public Obj currentObj;

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
}
