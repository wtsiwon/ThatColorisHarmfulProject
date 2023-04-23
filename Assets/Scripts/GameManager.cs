using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public enum EButtonType
{
    Pass,
    Break,
}
public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    [Tooltip("HpUIs")]
    private List<Image> hpUIList = new List<Image>();

    [SerializeField]
    [Header("버튼 들")]
    [Tooltip("옆으로 넘기는 버튼")]
    private Button passBtn;

    [SerializeField]
    [Tooltip("물건 부수는 버튼")]
    private Button breakBtn;

    private int hp;
    public int Hp
    {
        get => hp;
        set
        {
            hp = value;
            UpdateHpUI();
            if (hp <= 0)
            {
                OnDie();
            }

        }
    }

    private void UpdateHpUI()
    {
        for (int i = 0; i < hpUIList.Count; i++)
        {
            hpUIList[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < hp; i++)
        {
            hpUIList[i].gameObject.SetActive(true);
        }
    }

    [SerializeField]
    [Space(10f)]
    [Tooltip("속도 증가량")]
    private float spdIncrement;

    [Tooltip("속도가 증가하는 점수")]
    public int speedIncreasePoint;

    [Tooltip("상호작용 가능 상태일 때 속도")]
    [SerializeField]
    private float interactionSpd;
    public float InteractionSpd
    {
        get
        {
            interactionSpd = score / speedIncreasePoint;
            interactionSpd = Mathf.Clamp(interactionSpd, MINSPD, MAXSPD);

            return interactionSpd;
        }
    }

    public const float MAXSPD = 50f;

    public const float MINSPD = 5f;

    [SerializeField]
    [Tooltip("오브젝트 상호작용 상태가 아닐 때 속도")]
    private float objFallingSpd;

    public float ObjSFallingSpd
    {
        get
        {
            return objFallingSpd;
        }
    }

    [SerializeField]
    [Space(10f)]
    private int score;
    public int Score
    {
        get => score;
        set
        {
            score = value;
        }
    }

    [SerializeField]
    [Tooltip("Player공격하는 모션")]
    private GameObject playerAttackMotion;

    [SerializeField]
    [Tooltip("Player기본 모션")]
    private GameObject playerDefaultMotion;


    [SerializeField]
    [Space(10f)]
    [Tooltip("게임 시작 카운드 다운을 위한 Text")]
    private TextMeshProUGUI beginCountText;

    [SerializeField]
    [Tooltip("FadeOut을 위한 검정색 이미지")]
    private Image blackBoard;


    [SerializeField]
    private float fadeOutTime;

    private bool isGameStart;
    public bool IsGameStart
    {
        get => isGameStart;
        set
        {
            isGameStart = value;
            SetGame();
        }
    }

    /// <summary>
    /// 게임 세팅
    /// </summary>
    private void SetGame()
    {
        ObjSpawner.Instance.ObjSpawn();
    }

    private void Start()
    {
        AddListener();
        StartCoroutine(nameof(IFadeOut));
        StartCoroutine(nameof(IUpdate));
    }

    /// <summary>
    /// 여러가지 확인 하기 위해 만든 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator IUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator IFadeOut()
    {
        float current = 0;
        float percent = 0;

        Color tempColor = blackBoard.color;

        while(percent < 1)
        {
            current += Time.deltaTime;
            percent = current / fadeOutTime;

            tempColor.a = Mathf.Lerp(1, 0, percent);

            blackBoard.color = tempColor;

            yield return null;
        }
        StartCoroutine(nameof(IBeginCount));
        yield break;
    }

    private IEnumerator IBeginCount()
    {
        beginCountText.gameObject.SetActive(true);
        for (int i = 3; i > 0; i--)
        {
            beginCountText.text = $"{i}";
            yield return new WaitForSeconds(1f);
        }
        beginCountText.gameObject.SetActive(false);

        IsGameStart = true;
    }

    /// <summary>
    /// 버튼 이벤트 등록
    /// </summary>
    private void AddListener()
    {
        passBtn.onClick.AddListener(() =>
        {
            PressPassBtn();
        });

        breakBtn.onClick.AddListener(() =>
        {
            PressBreakBtn();
        });
    }

    /// <summary>
    /// Pass버튼을 눌렀을 때
    /// </summary>
    public void PressPassBtn()
    {
        if (ObjSpawner.Instance.CheckObjQueue() == false) return;
        FallingObjControl(EButtonType.Pass);
    }

    /// <summary>
    /// Break버튼을 눌렀을 때
    /// </summary>
    public void PressBreakBtn()
    {
        StartCoroutine(nameof(IAttackMotion));
        if (ObjSpawner.Instance.CheckObjQueue() == false) return;
        FallingObjControl(EButtonType.Break);
    }

    private void FallingObjControl(EButtonType type)
    {
        if (type == EButtonType.Pass)
        {
            if (GetCurrentFallingObj() == EObjType.Other)
            {
                Pass();
            }
            else
            {
                Drop();
            }
        }
        else if (type == EButtonType.Break)
        {
            if (GetCurrentFallingObj() == EObjType.Other)
            {
                Drop();
            }
            else
            {
                Break();
            }
        }
    }

    /// <summary>
    /// 옆으로 넘겨야 할 때 오브젝트 상태를 pass로 바꿔줌
    /// </summary>
    public void Pass()
    {
        Obj obj = ObjSpawner.Instance.objQueue.Dequeue();
        obj.State = EObjState.Pass;
    }

    /// <summary>
    /// 부숴야 할 때 오브젝트 상태를 break로 바꿔줌
    /// </summary>
    public void Break()
    {
        Obj obj = ObjSpawner.Instance.objQueue.Dequeue();
        obj.State = EObjState.Break;
    }

    /// <summary>
    /// 떨어뜨려야 할 때 오브젝트의 상태를 Drop으로 바꿔줌
    /// </summary>
    public void Drop()
    {
        Obj obj = ObjSpawner.Instance.objQueue.Dequeue();
        obj.State = EObjState.Drop;
    }

    /// <summary>
    /// 현재 떨어지고 있는 오브젝트의 Type을 받아옴
    /// </summary>
    /// <returns></returns>
    private EObjType GetCurrentFallingObj()
    {
        EObjType type = ObjSpawner.Instance.objQueue.Peek().type;
        return type;
    }


    void Update()
    {
        ComputerInputKey();
    }

    private void ComputerInputKey()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PressPassBtn();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            PressBreakBtn();
        }
    }

    /// <summary>
    /// 공격모션
    /// </summary>
    private void AttackMotion()
    {
        StartCoroutine(nameof(IAttackMotion));
    }

    private IEnumerator IAttackMotion()
    {
        playerAttackMotion.SetActive(true);
        playerDefaultMotion.SetActive(false);

        yield return new WaitForSeconds(0.1f);

        playerAttackMotion.SetActive(false);
        playerDefaultMotion.SetActive(true);
    }

    private void OnDie()
    {

    }
}