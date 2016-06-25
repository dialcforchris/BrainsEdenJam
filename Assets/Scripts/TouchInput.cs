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
    [SerializeField] private Camera cam;
    float speed;

    void Awake () 
    {
	    if (touch == null)
        {
            touch = this;
        }
        height = Screen.height;
        width = Screen.width;
       
	}
	
    void Update()
    {
        SpeedDrag();
    }
    public Vector2 GetTouchScreen()
    {
        Vector2 touches = Vector2.zero;
        if (Input.touchCount>0)
        {
            Touch _touch = Input.GetTouch(0);
            touches = _touch.position;
            Vector3 vec = new Vector3(touches.x, touches.y, -10);  
            touches = cam.ScreenToViewportPoint(vec);
            return touches;
        }
        return touches;
    }

    public Vector2 GetTouchWorldPos()
    {
        Vector2 worldPos = Vector2.zero;
        worldPos = GetTouchScreen();
        Vector3 vec = new Vector3(worldPos.x, worldPos.y, -10);
        worldPos = cam.ViewportToWorldPoint(vec);
        return worldPos;
    }

    public bool IsTouched()
    {
        return Input.touchCount > 0;
    }

    public float GetSwipeSpeed()
    {
        Vector2 touchVel = Vector2.zero;
        if (Input.touchCount>0&&Input.GetTouch(0).phase == TouchPhase.Moved)
        {
          touchVel = Input.GetTouch(0).deltaPosition;
          speed = Input.GetTouch(0).deltaTime;
            speed = (touchVel.magnitude / speed)/100;
        }
       
        return speed;
    }
    public float SpeedDrag()
    {
        if (Input.touches.Length==0)
        {
            if (speed > 0)
            {
                speed -= Time.deltaTime*100;
            }
            else
            {
                speed = 0;
            }
        }
        else if (Input.GetTouch(0).phase == TouchPhase.Stationary)
        {
            speed = 0;
        }
        return speed;
    }

    public float SetVelocity()
    {

        return  Mathf.Clamp01(Mathf.Clamp((TouchInput.instance.GetSwipeSpeed() / 100), 0, 100));
    }
}
