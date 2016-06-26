using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class LevelButton : MonoBehaviour {

    public int Index;

    public Text text;
    public Image[] Stars;

	// Use this for initialization
	void Start ()
    {
        text.text = ""+Index;
	}

    public void LoadLevel()
    {
               Application.LoadLevel(Index);
    }
}
