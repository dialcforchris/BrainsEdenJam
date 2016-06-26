using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BGM : MonoBehaviour
{
    [SerializeField]
    private AudioClip light;
    [SerializeField]
    private AudioClip dark;
    [SerializeField]
   public AudioSource playLight;
    [SerializeField]
    public AudioSource playDark;
    public int count = 0;
    public Text text;

    public int nextLevel;

    private static BGM bgm = null;
    public static BGM instance
    {
        get { return bgm; }
    }
	// Use this for initialization
	void Awake () 
    {
        if (bgm = null)
        bgm = this;
        DontDestroyOnLoad(this);
       
        playLight.clip = light;
        playDark.clip = dark;
        if (!playLight.isPlaying)
        {
            playLight.Play();
            count++;
        }
        if (!playDark.isPlaying)
        {
            playDark.Play();
        }
       
	}
	
	// Update is called once per frame
	void Update () 
    {
        MixTracks();
      //  PlayStuff();
        text.text = count.ToString() ;
      //  DontDestoryMaybe();
	}
    void MixTracks()
    {
        playLight.volume = screenTransition.instance.GetVal();
        playDark.volume = 1 - playLight.volume;
    }

    void PlayStuff()
    {
      
       
         
        
    }
    void DontDestoryMaybe()
    {
        if (nextLevel!=0)
        {
            DontDestroyOnLoad(this);
        }
     }
}
