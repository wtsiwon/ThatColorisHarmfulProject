using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void SetObj()
    {

    }

    public Vector3 dir = Vector3.down;

    void Update()
    {
        Move();
    }


    /// <summary>
    /// ���¿� ���� �����Ӻ�ȭ
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
