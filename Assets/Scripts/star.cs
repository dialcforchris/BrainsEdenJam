using UnityEngine;
using System.Collections;

public class star : MonoBehaviour {

    bool collected;

    public Animator anim;
    public ParticleSystem starPoof;

    void OnTriggerEnter2D()
    {
        if (!collected)
        {
            Invoke("thing", 0.5f);
            anim.SetBool("collected", true);
            collected = true;
            starManager.instance.starCollection();
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
