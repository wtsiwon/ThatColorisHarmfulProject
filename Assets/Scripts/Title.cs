using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Title : MonoBehaviour
{
    public float maxMoveY;
    public float minMoveY;

    public bool isMoving;

    public ETitleState state = ETitleState.Down;
    public enum ETitleState
    {
        Down,
        Up,
    }

    public float time;

    void Start()
    {
        StartCoroutine(nameof(IVerticalMove));
    }

    private IEnumerator IVerticalMove()
    {
        while (true)
        {
            float current = 0;
            float percent = 0;
            Vector3 startPos = transform.localPosition;
            Vector3 endPos;

            endPos = state == ETitleState.Down ? 
                new Vector3(startPos.x, minMoveY, startPos.z) 
                : new Vector3(startPos.x, maxMoveY, startPos.z);

            while (percent < 1)
            {
                current += Time.deltaTime;
                percent = current / time;

                transform.localPosition = Vector3.Lerp(startPos, endPos, percent);

                yield return null;
            }

            state = state == ETitleState.Down ? ETitleState.Up : ETitleState.Down;
            
            yield return null;
        }
    }
}