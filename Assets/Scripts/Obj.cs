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

    [Tooltip("이 오브젝트의 시간(1이 원래 속도)")]
    public float timeScale = 1;

    [Tooltip("상호작용이 가능해지는 포지션 Y값")]
    [SerializeField]
    private float interactionPossiblePositionY;

    [Tooltip("상호작용이 끝나는 포지션 Y값")]
    [SerializeField]
    private float interactionEndPositionY;

    [Tooltip("아래쪽 파괴 Y Position")]
    [SerializeField]
    private float downDestroyPositionY;

    [Tooltip("오른쪽 파괴 X Position")]
    [SerializeField]
    private float rightDestroyPositionX;

    [Tooltip("오브젝트의 상태")]
    [SerializeField]
    private EObjState state = EObjState.FallDown;
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
    /// <param name="type">obj의 색</param>
    /// <param name="spd">이동 속도</param>
    public void SetObj(EObjType type, float spd)
    {
        this.type = type;
        this.spd = spd;
    }

    [HideInInspector]
    public Vector3 dir = Vector3.down;

    private void Start()
    {
        SetObj(EObjType.Green, 10);
    }
    
    private void Update()
    {
        Move();
        ObjState();
        DestroyObj();
    }

    private void DestroyObj()
    {
        Vector3 currentpos = transform.position;
        if(currentpos.y < downDestroyPositionY)
        {
            GameManager.Instance.CurrentFallingObj = null;
            GameManager.Instance.Hp -= 1;

            Destroy(gameObject);
        }

        else if(currentpos.x < rightDestroyPositionX)
        {
            GameManager.Instance.CurrentFallingObj = null;
            GameManager.Instance.Hp -= 1;

            Destroy(gameObject);
        }
    }

    private void ObjState()
    {
        if (GameManager.Instance.CurrentFallingObj == null) return;

        float currentPosY = transform.position.y;
        if (currentPosY < interactionPossiblePositionY && currentPosY > interactionEndPositionY)
        {
            State = EObjState.Slow;
            GameManager.Instance.CurrentFallingObj = this;
        }
        else if(currentPosY < interactionEndPositionY)
        {
            State = EObjState.Drop;
            GameManager.Instance.CurrentFallingObj = null;
        }
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
                spd = GameManager.Instance.InteractionSpd;
                break;
            case EObjState.Pass:
                dir = Vector3.right;
                spd = GameManager.Instance.ObjSFallingSpd;
                break;
            case EObjState.Drop:
                spd = GameManager.Instance.ObjSFallingSpd;
                GameManager.Instance.CurrentFallingObj = null;
                break;
            case EObjState.Break:
                GameManager.Instance.CurrentFallingObj = null;
                Destroy(gameObject);
                break;
            default:
                Debug.Assert(false, "어..? 여기 오면 안돼는디");
                break;
        }
    }

    private void Move()
    {
        transform.position += dir * spd * Time.deltaTime * timeScale;
    }

    public void CameraShake(float time, float range) 
    {
        
    }
}
