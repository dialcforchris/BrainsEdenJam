using UnityEngine;
using System.Collections;

public class endPoint : MonoBehaviour {

    SpriteRenderer sr;
    public int levelIndextoLoad;
    public AudioClip clip;
    public AudioSource source;
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            attractPlayer = true;

            thing = col.gameObject;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            attractPlayer = false;
            thing = null;
        }
    }

    bool attractPlayer,levelFinished;
    GameObject thing;
    // Update is called once per frame
    void Update()
    {
        if (attractPlayer)
        {
            thing.transform.position = Vector3.Lerp(thing.transform.position, transform.position, Time.deltaTime * 10);
            if (Vector3.Distance(thing.transform.position, transform.position) < 1)
            {
                sr.color = Color.red;
            }
            else
                sr.color = Color.white;
        }
        if (thing && !levelFinished)
        {
            if (Player.instance.playerState != Player.PlayerStates.DEAD)
            {
                if (Vector3.Distance(thing.transform.position, transform.position) < .2f)
                {
                    source.PlayOneShot(clip);
                    levelFinished = true;
                    StartCoroutine(screenTransition.instance.screenTransitioner(false, levelIndextoLoad));
                }
            }
            else
            {
                attractPlayer = false;
                thing = null;
            }
        }
    }
}
