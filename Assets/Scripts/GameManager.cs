using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    [Tooltip("������ �ѱ�� ��ư")]
    private Button passBtn;

    [SerializeField]
    [Tooltip("���� �μ��� ��ư")]
    private Button breakBtn;

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
