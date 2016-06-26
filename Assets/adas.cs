using UnityEngine;
using System.Collections;

public class adas : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    for(int i = 0; i < transform.childCount; ++i)
        {
            for (int j = 0; j < transform.GetChild(i).transform.childCount; ++j)
            {

                    Destroy(transform.GetChild(i).transform.GetChild(j).transform.GetChild(0).gameObject);
                
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
