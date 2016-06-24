using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour 
{
    private bool inGreyWorld = true;

    public Vector2 startPos;
    float speed = 6;
    private void Awake()
    {
        Physics2D.IgnoreLayerCollision(8, 10, true);
    }
	
	// Update is called once per frame
	void Update () 
    {
        Movement();
        if(TouchInput.instance.IsTouched())
        {
            Vector2 _v = TouchInput.instance.GetTouchWorldPos();
            if (transform.position.x < _v.x)
            {
                SwitchWorld(false);
            }
            else
            {
                SwitchWorld(true);
            }
        }
	}

    void Movement()
    {
        Vector3 accInput;
        accInput = Input.acceleration;

        if (accInput.sqrMagnitude>1)
        {
            accInput.Normalize();
        }
        transform.position += accInput * Time.deltaTime * speed;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);
        if (Vector3.Distance(Vector3.zero, transform.position) > 25)
            transform.position = Vector3.zero;
        
    }

    private void SwitchWorld(bool _world)
    {
        if(inGreyWorld != _world)
        {
            inGreyWorld = _world;
            Physics2D.IgnoreLayerCollision(8, 10, inGreyWorld);
            Physics2D.IgnoreLayerCollision(9, 10, !inGreyWorld);
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
