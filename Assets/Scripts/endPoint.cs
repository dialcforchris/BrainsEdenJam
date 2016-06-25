using UnityEngine;
using System.Collections;

public class endPoint : MonoBehaviour {
    
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
	void Update ()
    {
        if (attractPlayer)
        {
            thing.transform.position = Vector3.Lerp(thing.transform.position, transform.position, Time.deltaTime*2.5f);
        }
    }
}
