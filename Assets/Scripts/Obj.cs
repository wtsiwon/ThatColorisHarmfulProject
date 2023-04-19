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
    FallDown,//처음에 스폰되고 떨어짐
    Slow,//느려지면서 상호작용이 가능해짐
    Pass,//type이 other일 경우 옆으로 넘김
    Drop,//옳바르지 않은 버튼을 눌렀을 경우 떨어지고(-1hp)
    Break,//type이 green경우 부숨
}
public class Obj : MonoBehaviour
{
    public EObjType type;

    [Tooltip("현재 속도")]
    public float spd;

    [Tooltip("기본 속도")]
    public const float DSPD = 10f;

    [Tooltip("최고 속도")]
    public const float MAXSPD = 35f;

    [Tooltip("처음 스폰되고 떨어질 때와 틀리고 바닥으로 떨어질 때 속도")]
    public const float FALLSPD = 30f;

    

    [SerializeField]
    [Tooltip("오브젝트의 상태")]
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
    /// Obj 세팅함수
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

            case EObjState.Pass:

                break;
            case EObjState.Drop:

                break;
            case EObjState.Break:

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
