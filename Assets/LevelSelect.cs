using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class LevelSelect : MonoBehaviour {

    public LevelButton[] Levels;
    public Sprite filledStar;

	// Use this for initialization
	void Start ()
    {
        if (!File.Exists(Application.dataPath + "/" + "sters.txt"))
        {
            StreamWriter newFile = new StreamWriter(Application.dataPath + "/" + "sters.txt", false);
            newFile.Close();
        }
        StreamReader sr = new StreamReader(Application.dataPath + "/" + "sters.txt");
        string[] starString = sr.ReadToEnd().Split('\n');
        
        foreach (string s in starString)
        {
            if (s.Length > 2)
            {
                string Levelnum = "" + s[6];
                string stars = "" + s[s.Length - (s.EndsWith("\n") ? 2 : 1)];
                for (int i = 0; i < int.Parse(stars); i++)
                {
                    Levels[int.Parse(Levelnum) - 1].Stars[i].sprite = filledStar;
                }
            }
        }

        sr.Close();
    }
}
