using UnityEngine;
using System.Collections;

public class endPoint : MonoBehaviour {

    SpriteRenderer sr;
    public int levelIndextoLoad;
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

    bool attractPlayer;
    GameObject thing;
    // Update is called once per frame
    void Update()
    {
        if (attractPlayer)
        {
            thing.transform.position = Vector3.Lerp(thing.transform.position, transform.position, Time.deltaTime * 2.5f);
            if (Vector3.Distance(thing.transform.position, transform.position) < 1)
            {
                sr.color = Color.red;
            }
            else
                sr.color = Color.white;
        }
        if (thing)
        {
            if (Vector3.Distance(thing.transform.position, transform.position) < .2f)
            {
                StartCoroutine(screenTransition.instance.screenTransitioner(false,levelIndextoLoad));
            }
        }
    }
}
