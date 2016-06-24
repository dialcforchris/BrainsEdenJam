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
    private int height = 0;
    private int width = 0;
    [SerializeField]
    private Camera cam;
	// Use this for initialization
	void Awake () 
    {
	    if (touch == null)
        {
            touch = this;
        }
        height = Screen.height;
        width = Screen.width;
       
	}
	
	
    public Vector2 GetTouchScreen()
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

            Vector3 vec = new Vector3(touches.x, touches.y, 10);
            //touches.x /= width;
            //touches.y /= height;
           touches = cam.ScreenToViewportPoint(vec);
        }
      
        return touches;
    }

    public Vector2 GetTouchWorldPos()
    {
        
        Vector2 worldPos = Vector2.zero;
      
            worldPos = GetTouchScreen();
          
           Vector3 vec = new Vector3(worldPos.x, worldPos.y, 10);
          worldPos = cam.ViewportToWorldPoint(vec);
     
        

        return worldPos;
            
    }
}
