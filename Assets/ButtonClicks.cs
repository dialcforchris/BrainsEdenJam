using UnityEngine;
using System.Collections;

public class ButtonClicks : MonoBehaviour
{
    public AudioClip clip;
    public AudioClip hahahahahaha;
    public AudioSource source;
	public void PlaySound()
    {
        AudioClip ac = Random.value < 0.9f ? clip : hahahahahaha;
        source.PlayOneShot(ac);
    }
}
