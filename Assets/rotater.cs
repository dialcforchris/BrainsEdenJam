using UnityEngine;
using System.Collections;

public class rotater : MonoBehaviour {

    public Transform rotationCenter;
    public float speed;
    float angle;    
	void FixedUpdate ()
    {
        transform.RotateAround(rotationCenter.position, Vector3.forward, Time.fixedDeltaTime*speed);
	}
}
