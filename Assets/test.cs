using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class test : MonoBehaviour {

    public Text text;
    public Text text2;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        text.text = "speed drag " + TouchInput.instance.SpeedDrag();
        text2.text = "swipe speed " + Mathf.Clamp01(Mathf.Clamp((TouchInput.instance.GetSwipeSpeed()/100),0,100)).ToString();
	}
}
