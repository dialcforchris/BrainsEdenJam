using UnityEngine;
using System.Collections;

public class sada : MonoBehaviour
{
    public GameObject p;
    public Sprite a;
    public Sprite b;
	// Use this for initialization
	void Start ()
    {
        int i = 0;
        while(i != transform.childCount)
        {
            //GameObject o = Instantiate(p, transform.GetChild(i).transform.position, Quaternion.identity) as GameObject;
            //if(transform.GetChild(i).eulerAngles.z > 45)
            //{
            //    o.GetComponent<SpriteRenderer>().sprite = a;
            //}
            //else
            //{
            //    o.GetComponent<SpriteRenderer>().sprite = b;
            //}
            //Destroy(transform.GetChild(i).gameObject);



            if (transform.GetChild(i).GetComponent<SpriteRenderer>().sprite.name == "Horizontal")
            {
                BoxCollider2D d = transform.GetChild(i).GetComponent<BoxCollider2D>();
                //d.size = new Vector2(d.size.y, d.size.x);
                d = transform.GetChild(i).transform.GetChild(0).GetComponent<BoxCollider2D>();
                d.size = new Vector2(d.size.x, 0.4f);
            }
            else
            {
                BoxCollider2D d = transform.GetChild(i).GetComponent<BoxCollider2D>();
                //d.size = new Vector2(d.size.y, d.size.x);
                d = transform.GetChild(i).transform.GetChild(0).GetComponent<BoxCollider2D>();
                d.size = new Vector2(0.4f, d.size.y);
            }
            ++i;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
