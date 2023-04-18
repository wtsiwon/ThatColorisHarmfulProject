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

    public bool isObjectSpawn;

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

    }

    /// <summary>
    /// ObjSpawn
    /// </summary>
    public void ObjSpawn()
    {
        int rand = Random.Range(0, 2);
        GameObject gameObject = Instantiate(originObj, spawnPos.position, Quaternion.identity);

        Obj obj = gameObject.GetComponent<Obj>();

        obj.SetObj((EObjType)rand);

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
