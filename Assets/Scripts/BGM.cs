using UnityEngine;
using System.Collections;

public class BGM : MonoBehaviour
{
    [SerializeField]
    private AudioClip light;
    [SerializeField]
    private AudioClip dark;
    [SerializeField]
    private AudioSource playLight;
    [SerializeField]
    private AudioSource playDark;

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

	}
    void MixTracks()
    {
        playLight.volume = screenTransition.instance.GetVal();
        playDark.volume = 1 - playLight.volume;
    }
}
