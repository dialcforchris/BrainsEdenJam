using UnityEngine;
using System.Collections;

public class star : MonoBehaviour {

    bool collected;

    public Animator anim;
    public ParticleSystem starPoof;
    public AudioClip pickupSound;
    public AudioSource source;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            if (!collected)
            {
                Invoke("thing", 0.5f);
                anim.SetBool("collected", true);
                source.PlayOneShot(pickupSound);
                collected = true;
                starManager.instance.starCollection();
            }
        }
    }

    void thing()
    {
        starPoof.Play();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update () {
	
	}
}
