using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Tooltip("�ӵ��� �����ϴ� ����")]
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

    [Tooltip("���� �������� �ִ� ������Ʈ")]
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
    /// ���� ����
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
