using System;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    [SerializeField] GameController GameController;
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;


    // Update is called once per frame
    void Update()
    {
        Debug.Log("TOUCHCOUNT "+Input.touchCount);
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouchPosition = Input.GetTouch(0).position;

            Debug.Log(startTouchPosition + " END " + endTouchPosition);

            if (Math.Abs(endTouchPosition.x - startTouchPosition.x) > Math.Abs(endTouchPosition.y - startTouchPosition.y))
            {
                //if (Math.Abs(endTouchPosition.x - startTouchPosition.x) > 0)
                {
                    if (endTouchPosition.x < startTouchPosition.x)
                    {
                        GameController.MoveOrAttack("E");
                    }
                    else
                    {
                        GameController.MoveOrAttack("W");
                    }
                }

            }
            else
            {
                //if (Math.Abs(endTouchPosition.y - startTouchPosition.y) > 0)
                {
                    if (endTouchPosition.y < startTouchPosition.y)
                    {
                        GameController.MoveOrAttack("S");
                    }
                    else
                    {
                        GameController.MoveOrAttack("N");
                    }
                }
            }
                
        }
    }
}
