using UnityEngine;
using System.Collections;

public class screenTransition : MonoBehaviour {

    public static screenTransition instance;

    [Range (0,1)]
    public float val;
    float previousVal;

    [SerializeField]
    private Camera camA, camB;

    [SerializeField]
    private Texture[] transitionTextures;
    public SpriteRenderer transitionSprite;

    public float multi,offZet,velocity;
    bool LastDir; //False can be left, true for right

    Vector2 previousTouchPos;
    void Awake()
    {
        StartCoroutine(screenTransitioner(true));
        moveScreenSlider();
        instance = this;
    }

    public IEnumerator screenTransitioner(bool inOut, int level = -99)//True = in, false = out
    {
        transitionSprite.material.SetTexture("_SliceGuide",transitionTextures[Random.Range(0, transitionTextures.Length)]);

        float lerpy = 1;
        
        if (!inOut)
        {
            while (lerpy > 0)
            {
                lerpy -= Time.deltaTime * 1;
                transitionSprite.material.SetFloat("_SliceAmount", lerpy);
                yield return new WaitForEndOfFrame();
            }
        }
        else
        {
            lerpy = 0;
            while (lerpy < 1)
            {
                lerpy += Time.deltaTime * 1;
                transitionSprite.material.SetFloat("_SliceAmount", lerpy);
                yield return new WaitForEndOfFrame();
            }
        }
        if (level != -99)
        {
            Application.LoadLevel(level);
        }
    }

    public IEnumerator Shake(float duration)
    {
        float timer = 0;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            transform.position = new Vector2(Mathf.Sin(Random.value), Mathf.Sin(Random.value));
            yield return new WaitForEndOfFrame();
        }
    }

    void Update()
    {
        val = Input.mousePosition.x / Screen.width;

        if (TouchInput.instance.IsTouched())
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
                val = Mathf.Lerp(val, 0, Time.deltaTime * TouchInput.instance.GetSwipeSpeed());
                val = (val < 0) ? 0 : val;
            }
            else if (val < 1 && LastDir)//Snap right
            {
                val = Mathf.Lerp(val, 1, Time.deltaTime * TouchInput.instance.GetSwipeSpeed());
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
        Rect a = camA.rect;
        a.xMin = val;
        camA.rect = a;

        camB.transform.position = new Vector3(camA.orthographicSize * (1 - val) * multi * -1, 0, offZet);
        Rect CamBRect = camB.rect;
        CamBRect.xMax = val;
        camB.rect = CamBRect;

        previousVal = val;
    }
}
