using UnityEngine;
using System.Collections;

public class screenTransition : MonoBehaviour {

    [Range (0,1)]
    public float val;

    [SerializeField]
    private Camera camA, camB;

    public float multi,offZet;
    bool b;
	// Update is called once per frame
	void Update ()
    {
        if(TouchInput.instance.IsTouched())
        {
            val = TouchInput.instance.GetTouchScreen().x;
        }

        camA.transform.position = new Vector3(camA.orthographicSize * val * multi, 0,offZet);
        var a = camA.rect;
        a.xMin = val;
        camA.rect = a;

        camB.transform.position = new Vector3(camA.orthographicSize * (1 - val) * multi*-1, 0,offZet);
        var CamBRect = camB.rect;
        CamBRect.xMax = val;
        camB.rect = CamBRect;
    }
}
