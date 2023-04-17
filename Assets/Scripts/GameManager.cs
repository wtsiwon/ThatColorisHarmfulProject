using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
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
