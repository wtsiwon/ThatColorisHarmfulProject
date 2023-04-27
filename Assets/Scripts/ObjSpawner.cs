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

    public float objFallingSpd;

    public bool isSpawn;

    public float spawnInterval;

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
        //StartCoroutine(nameof(ISpawn));
    }

    private IEnumerator ISpawn()
    {
        while (true)
        {
            if (isSpawn)
            {
                yield return new WaitForSeconds(spawnInterval);
                ObjSpawn();
            }
            yield return null;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ObjSpawn();
        }
    }

    /// <summary>
    /// ObjSpawn
    /// </summary>
    public void ObjSpawn()
    {
        int rand = Random.Range(0, 2);
        GameObject gameObject = Instantiate(originObj, spawnPos.position, Quaternion.identity);


        Obj obj = gameObject.GetComponent<Obj>();

        obj.type = (EObjType)rand;

        int randSprite = 0;

        Sprite sprite;

        if (rand == 0)
        {
            randSprite = Random.Range(0, greenObjects.Length);
            sprite = greenObjects[randSprite];
        }
        else
        {
            randSprite = Random.Range(0, otherObjects.Length);
            sprite = otherObjects[randSprite];
        }

        obj.spd = GameManager.Instance.ObjSFallingSpd;
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        obj.transform.localScale = SpriteResizing(sprite);
    }

    private Vector3 SpriteResizing(Sprite sprite)
    {
        Vector3 scale;
        float x = sprite.texture.width;
        float y = sprite.texture.height;

        scale = new Vector3(150 / x, 150 / y, 1);
        return scale;
    }
}
