using UnityEngine;

public class Ident : MonoBehaviour
{
    void Start()
    {
        if (BGM.instance!=null)
        {
            Destroy(BGM.instance);
        }
    }
	public void loadLevel()
    {
        Application.LoadLevel(1);
    }
}
