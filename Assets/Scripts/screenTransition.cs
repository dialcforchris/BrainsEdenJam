using UnityEngine;
using System.Collections;

public class screenTransition : MonoBehaviour {

    public static screenTransition instance;

    [Range (0,1)]
    public float val;
    float previousVal;

    [SerializeField]
    private Camera camA, camB;

    public float multi,offZet,velocity;
    bool LastDir; //False can be left, true for right

    Vector2 previousTouchPos;
    void Awake()
    {
        moveScreenSlider();
        instance = this;
    }

	void Update ()
    {
        if(TouchInput.instance.IsTouched())
        {
            Vector2 pos = TouchInput.instance.GetTouchScreen();
            velocity = previousTouchPos.x - pos.x;
            val = pos.x;
            previousTouchPos = TouchInput.instance.GetTouchScreen();
        }
        else if (velocity ==0)
        {
            /*if (val > 0 && !LastDir)//Snap left
            {
                val = Mathf.Lerp(val, 0, Time.deltaTime * 3);
                val = (val < 0) ? 0 : val;
            }
            else if (val < 1 && LastDir)//Snap right
            {
                val = Mathf.Lerp(val, 1, Time.deltaTime * 3);
                val = (val > 1) ? 1 : val;
            }*/

            if (val > 0.85f)
                LastDir = true;
            else if (val < 0.15f)
                LastDir = false;
        }
        else
        {
            val += velocity;
        }

        if (val != previousVal)
            moveScreenSlider();
    }

    void moveScreenSlider()
    {
        camA.transform.position = new Vector3(camA.orthographicSize * val * multi, 0, offZet);
        var a = camA.rect;
        a.xMin = val;
        camA.rect = a;

        camB.transform.position = new Vector3(camA.orthographicSize * (1 - val) * multi * -1, 0, offZet);
        var CamBRect = camB.rect;
        CamBRect.xMax = val;
        camB.rect = CamBRect;

        previousVal = val;
    }
}
