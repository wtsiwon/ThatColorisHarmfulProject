using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ObjSpawner : Singleton<ObjSpawner>
{
    private Sprite[] greenObjects;
    private Sprite[] otherObjects;

    public GameObject originObj;

    public Transform spawnPos;

    [Tooltip("기본 속도")]
    public const float DSPD = 10f;

    [Tooltip("최고 속도")]
    public const float MAXSPD = 35f;

    [Tooltip("처음 스폰되고 떨어질 때와 틀리고 바닥으로 떨어질 때 속도")]
    public const float FALLSPD = 30f;

    private bool isObjectSpawn;
    public bool IsObjectSpawn
    {
        get => isObjectSpawn;
        set
        {
            isObjectSpawn = value;
        }
    }

    public float objFallingSpd;

    private void Awake()
    {
        AddResources();
    }
    private void AddResources()
    {
        greenObjects = Resources.LoadAll<Sprite>("GreenObj/");
        otherObjects = Resources.LoadAll<Sprite>("OtherObj/");
    }

    private void Start()
    {
        AddResources();
    }

    /// <summary>
    /// ObjSpawn
    /// </summary>
    public void ObjSpawn()
    {
        int rand = Random.Range(0, 2);
        GameObject gameObject = Instantiate(originObj, spawnPos.position, Quaternion.identity);

        //gameObject.transform.position = spawnPos.position;

        Obj obj = gameObject.GetComponent<Obj>();

        obj.SetObjType((EObjType)rand);

        int randSprite = 0;

        Sprite sprite;
        if (rand == 1)
        {
            randSprite = Random.Range(0, greenObjects.Length);
            sprite = greenObjects[randSprite];
        }
        else
        {
            randSprite = Random.Range(0, otherObjects.Length);
            sprite = otherObjects[randSprite];
        }

        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;

        GameManager.Instance.CurrentFallingObj = obj;
    }
}
