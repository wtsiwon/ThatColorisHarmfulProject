using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public enum EObjType
{
    Green,
    Other,
}

public enum EObjState
{
    FallDown,
    Slow,
    Right,
    Drop,
    Correct,
}
public class Obj : MonoBehaviour
{
    public EObjType type;

    public float spd;

    [SerializeField]
    private EObjState state;
    public EObjState State
    {
        get => state;
        set
        {
            state = value;
            MovementOnTheState();
        }
    }

    /// <summary>
    /// Obj세팅함수
    /// </summary>
    /// <param name="type"></param>
    public void SetObj(EObjType type)
    {
        this.type = type;
    }

    public Vector3 dir = Vector3.down;

    void Update()
    {
        Move();
    }


    /// <summary>
    /// 상태에 따른 움직임변화
    /// </summary>
    private void MovementOnTheState()
    {
        switch (state)
        {
            case EObjState.FallDown:
                dir = Vector3.down;
                break;
            case EObjState.Slow:
                
                break;

            case EObjState.Right:

                break;
            case EObjState.Drop:

                break;
            case EObjState.Correct:

                break;
            default:
                Debug.Assert(false, "어..? 여기 오면 안돼는디");
                break;
        }
    }

    private void Move()
    {
        transform.position = dir * spd * Time.deltaTime;
    }

    public void Return()
    {
        ObjPool.Instance.Return(this);
    }
}
