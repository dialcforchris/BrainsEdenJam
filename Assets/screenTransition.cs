using UnityEngine;
using System.Collections;

public class screenTransition : MonoBehaviour {

    [Range (0,1)]
    public float val;

    [SerializeField]
    private Material transitionA, transitionB;
    [SerializeField]
    private Camera camA, camB;

    public float multi;
    public bool move;
    bool b;
	// Update is called once per frame
	void Update ()
    {
        if (move)
        {
            if (b)
                val = val + Time.deltaTime < 1 ? val + Time.deltaTime : 1;
            else
                val = val - Time.deltaTime > 0 ? val - Time.deltaTime : 0;

            if (val == 1)
                b = false;
            if (val == 0)
                b = true;
        }

        camA.transform.position = new Vector2(camA.orthographicSize * val * multi, 0);
        var a = camA.rect;
        a.xMin = val;
        camA.rect = a;

        camB.transform.position = new Vector2(camA.orthographicSize * (1 - val) * multi*-1, 0);
        var CamBRect = camB.rect;
        CamBRect.xMax = val;
        camB.rect = CamBRect;

        transitionA.SetFloat("_SliceAmount", val);
        transitionB.SetFloat("_SliceAmount", 1 - val);
    }
}
