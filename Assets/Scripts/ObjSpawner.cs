using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjSpawner : Singleton<ObjSpawner>
{
    private Sprite[] greenObjects;
    private Sprite[] otherObjects;

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

    private IEnumerator ISpawn()
    {
        while (true)
        {
            if (GameManager.Instance.IsGameStart == true)
            {
                if(isObjectSpawn == true)
                {

                }
            }
            yield return null;
        }


    }
    private void Update()
    {

    }


}
