using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour 
{
    public Vector2 startPos;
	// Use this for initialization
	void Start () 
    {
	    
	}
	
	// Update is called once per frame
	void Update () 
    {
        Movement();
	}
    void Movement()
    {
        Vector2 accInput;
        accInput = Input.acceleration;

        if (accInput.sqrMagnitude>1)
        {
            accInput.Normalize();
        }
        
    }
   
    public void Die()
    {
        //do a death and....
        transform.position = startPos;
    }
    public enum PlayerStates
    {
        ACTIVE,
        DEAD,
    }
}
