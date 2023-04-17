using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                if (instance == null)
                {
                    GameObject go = new GameObject(typeof(T).Name);
                    go.AddComponent<T>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        instance = GetComponent<T>();
    }

    private void OnDestroy()
    {
        instance = null;
    }
}
