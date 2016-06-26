using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

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
        public Image colourBackground;

    public float multi,offZet,velocity;
    bool currentSide = true; //False can be left, true for right

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
            //if (level != 0)
            //{
            //    DontDestroyOnLoad(BGM.instance);
            //}
            SceneManager.LoadScene(level);
           // Application.LoadLevel(level);
        }
    }

    Vector3 shakeOffset;

    public IEnumerator Shake(float duration)
    {
        float timer = 0;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            shakeOffset= new Vector2(Mathf.Sin(Random.value), Mathf.Sin(Random.value));
            transform.position = shakeOffset;
            yield return new WaitForEndOfFrame();
        }
        shakeOffset = Vector3.zero;
        transform.position = Vector3.zero;
    }

    void Update()
    {
        bool touchTarget  =  TouchInput.instance.GetTouchScreen().x<0.5 ? true : false;
        //bool target = Input.mousePosition.x / Screen.width < 0.5f ? true : false;
       ////val = Input.mousePosition.x / Screen.width;
       // val += target ? Time.deltaTime  : -Time.deltaTime ;
        if (TouchInput.instance.IsTouched())
        {
          if (currentSide)
          {
            //velocity = previousTouchPos.x - pos.x;
            //val = pos.x;
              
          
                val += touchTarget ? Time.deltaTime : -Time.deltaTime;
              if (val>=1)
              {
                  currentSide=false;
                  val = 1;
              }
          }
          else
          {
              val += touchTarget ? +Time.deltaTime : -Time.deltaTime;
              if (val<=0)
              {
                  currentSide = true;
                  val = 0;
                  SoundManager.instance.playSound(0);
              }
          }
            //
               // previousTouchPos = TouchInput.instance.GetTouchScreen();
         
          
         }
        else 
        {
           if (currentSide)
           {
               val -= Time.deltaTime;

           }
           else if (!currentSide)
           {
               val += Time.deltaTime;

           }
           
        }

        //else if (velocity ==0)
        //{
        //    /*if (val > 0 && !LastDir)//Snap left
        //    {
        //        val = Mathf.Lerp(val, 0, Time.deltaTime * TouchInput.instance.GetSwipeSpeed());
        //        val = (val < 0) ? 0 : val;
        //    }
        //    else if (val < 1 && LastDir)//Snap right
        //    {
        //        val = Mathf.Lerp(val, 1, Time.deltaTime * TouchInput.instance.GetSwipeSpeed());
        //        val = (val > 1) ? 1 : val;
        //    }*/

        //    if (val > 0.85f)
        //        LastDir = true;
        //    else if (val < 0.15f)
        //        LastDir = false;
        //}
        //else
        //{
        //    val += velocity;
        //}
        val = Mathf.Clamp01(val);

        if (val != previousVal)
            moveScreenSlider();
    }

    void update()
    {
        moveScreenSlider();
 val = Input.mousePosition.x / Screen.width;
    }

    void moveScreenSlider()
    {
        camA.transform.position = new Vector3((camA.orthographicSize * val * multi )+ shakeOffset.x, shakeOffset.y, offZet);
        Rect a = camA.rect;
        a.xMin = val;
        camA.rect = a;

        camB.transform.position = new Vector3((camA.orthographicSize * (1 - val) * multi * -1)+shakeOffset.x, shakeOffset.y, offZet);
        Rect CamBRect = camB.rect;
        CamBRect.xMax = val;
        camB.rect = CamBRect;
        
        colourBackground.fillAmount = val;
        previousVal = val;
    }
    public float GetVal()
    {
        return val;
    }
}
