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
        text.text = "hopefully 0-1 " + TouchInput.instance.GetTouchScreen().ToString();
        text2.text = "hopefully World " + TouchInput.instance.GetTouchWorldPos().ToString();
	}
}
