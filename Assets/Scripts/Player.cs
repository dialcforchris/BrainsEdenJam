using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour 
{
    private bool inGreyWorld = true;

    public Transform StartPoint;
    public Vector2 startPos;
    float speed = 6;
    ParticleSystem ps;

    private void Awake()
    {
        
        ps = GetComponent<ParticleSystem>();
        //transform.position = startPos;
        Physics2D.IgnoreLayerCollision(8, 10, true);
    }
	
	// Update is called once per frame
	void Update () 
    {
        Movement();
        if (TouchInput.instance.IsTouched())
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

        //If the world thing should be transitioning do this
        if (transform.position.x < Camera.main.ViewportToWorldPoint(new Vector3(screenTransition.instance.val, 0.5f, 10)).x)
        {
            SwitchWorld(false);
        }
        else
        {
            SwitchWorld(true);
        }
    }

    public float moveSpeed;

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

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position = new Vector3(transform.position.x - (moveSpeed * Time.deltaTime), transform.position.y, 0.0f);
            //GetComponent<Rigidbody2D>().AddForce(new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y));
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position = new Vector3(transform.position.x + (moveSpeed * Time.deltaTime), transform.position.y, 0.0f);
            //GetComponent<Rigidbody2D>().AddForce(new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y));
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + (moveSpeed * Time.deltaTime), 0.0f);
            //GetComponent<Rigidbody2D>().AddForce(new Vector2(transform.position.x , transform.position.y + moveSpeed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - (moveSpeed * Time.deltaTime), 0.0f);
            //GetComponent<Rigidbody2D>().AddForce(new Vector2(transform.position.x, transform.position.y - moveSpeed * Time.deltaTime));
        }
    }

    private void SwitchWorld(bool _world)
    {
        if (inGreyWorld != _world)
        {
            ps.startColor = _world ? Color.black : Color.white;
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
