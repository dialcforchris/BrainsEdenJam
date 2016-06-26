using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class starManager : MonoBehaviour {

    int starsCollected;
    public Image[] UIStars;
    public Sprite FillledStar;
    public static starManager instance;
    public string s;

    void Start()
    {
        instance = this;
    }

    public void starCollection()
    {
        UIStars[starsCollected].sprite = FillledStar;
        starsCollected++;
        SaveToFile();
    }

    void SaveToFile()
    {
        if (!File.Exists(Application.dataPath + "/" + "sters.txt"))
        {
            StreamWriter newFile = new StreamWriter(Application.dataPath + "/" + "sters.txt", false);
            newFile.Close();
        }
        StreamReader sr = new StreamReader(Application.dataPath + "/" + "sters.txt");
        s = sr.ReadToEnd();
        if (s.IndexOf("Level " + Application.loadedLevel) != -1)
        {
            s = s.Remove(s.IndexOf("Level " + Application.loadedLevel), 10);
        }
        sr.Close();

        StreamWriter sw = new StreamWriter(Application.dataPath + "/" + "sters.txt", false);
        s += "Level " + Application.loadedLevel + " " + starsCollected + '\n';
        sw.Write(s);
        sw.Close();
    }
}
