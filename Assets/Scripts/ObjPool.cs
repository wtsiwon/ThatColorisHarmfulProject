using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPool : Singleton<ObjPool>
{
    [SerializeField]
    private Obj originObj;

    private Queue<Obj> queue = new Queue<Obj>();

    public Obj Getobj(EObjType type, Vector3 pos)
    {
        Obj obj = null;
        if(queue.Count > 0)
        {
            obj = queue.Dequeue();
        }
        else
        {
            obj = Instantiate(originObj);
        }

        obj.transform.position = pos;
        obj.type = type;

        return obj;
    }

    public void Return(Obj obj)
    {
        queue.Enqueue(obj);
        obj.gameObject.SetActive(false);
    }
}
