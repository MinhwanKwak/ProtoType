using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureDetector : MonoBehaviour
{
    public enum State
    {
        None,
        First,
        Second,
    }

    private Vector2 fingerDownPosition;

    private float firstX = 400f;
    private float secondY = 400f;
    private float offsetX = 300f;
    private float offsetY = 100f;
    private State state = State.None;

	private void Awake()
	{
		firstX = Screen.width / 2f;
		secondY = Screen.height / 2f;
	}

#if TREEPLLA_LOG
	private void Update()
    {       
        #if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        #else
         if(Input.touchCount < 1)
            return;

        var touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
        #endif        
        {
            #if UNITY_EDITOR
            fingerDownPosition = Input.mousePosition;
            #else
            fingerDownPosition = touch.position;
            #endif
            state = State.First;
        }

        #if UNITY_EDITOR
        if (state != State.None)
        #else
        if (state != State.None && touch.phase == TouchPhase.Moved)
        #endif
        {
            switch(state)
            {
                case State.First:
                    #if UNITY_EDITOR
                    CheckFirstMove(Input.mousePosition);
                    #else
                    CheckFirstMove(touch.position);
                    #endif
                break;
                default:
                    #if UNITY_EDITOR
                    CheckSecondMove(Input.mousePosition);
                    #else
                    CheckSecondMove(touch.position);
                    #endif
                break;
            }
        }
        #if UNITY_EDITOR
        if (Input.GetMouseButtonUp(0))
        #else
        if (touch.phase == TouchPhase.Ended)
        #endif        
        {
            state = State.None;
        }
    }
#endif

    private void CheckFirstMove(Vector2 pos)
    {
        if(Mathf.Abs( pos.y - fingerDownPosition.y ) > offsetY)
        {
            state = State.None;
            return;
        }

		if (pos.x - fingerDownPosition.x > firstX)
        {
            state = State.Second;
            fingerDownPosition = pos;
        }
    }

    private void CheckSecondMove(Vector2 pos)
    {
        if(Mathf.Abs( pos.x - fingerDownPosition.x ) > offsetX)
        {
            state = State.None;
            return;
        }
        if(fingerDownPosition.y - pos.y > secondY)
        {
            state = State.None;
            //Complete();
        }
    }

    //private void Complete()
    //{
    //    GameRoot.Instance.SetCheatWindow(true);
    //}
}
