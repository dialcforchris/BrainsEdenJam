using UnityEngine;
using System.Collections;

public class TouchInput : MonoBehaviour 
{
    private static TouchInput touch = null;
    public static TouchInput instance
    {
        get { return touch; }
    }
    public bool swipe = false;
	// Use this for initialization
	void Awake () 
    {
	    if (touch == null)
        {
            touch = this;
        }
	}
	
	
    Vector2 GetTouch()
    {
        Vector2 touches = Vector2.zero;
        if (Input.touchCount>0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                swipe = true;
            }
            else
            {
                swipe = false;
            }
            touches = Input.GetTouch(0).position;

        }
        return touches;
    }
}
