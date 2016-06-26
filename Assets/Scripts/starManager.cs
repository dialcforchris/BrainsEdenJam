using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class starManager : MonoBehaviour {

    int starsCollected;
    public Image[] UIStars;
    public Sprite FillledStar;
    public static starManager instance;

	void Start ()
    {
        instance = this;
	}

    public void starCollection()
    {
        UIStars[starsCollected].sprite = FillledStar;
        //Probably do some particle effects too
        starsCollected++;
    }

    void Update()
    {

    }
}
