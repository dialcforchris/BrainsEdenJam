using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour
{
    bool greyWorld = true;
    void Awake()
    {
        Physics2D.IgnoreLayerCollision(8, 10, true);
    }
	
	void Update ()
    {
        float _v = Input.GetAxis("Vertical") * Time.deltaTime * 5.0f;
        float _h = Input.GetAxis("Horizontal") * Time.deltaTime * 5.0f;
        transform.position += new Vector3(_h, _v, 0.0f);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            greyWorld = !greyWorld;
            Physics2D.IgnoreLayerCollision(8, 10, greyWorld);
            Physics2D.IgnoreLayerCollision(9, 10, !greyWorld);
        }
    }
}
