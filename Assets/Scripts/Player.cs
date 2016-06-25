using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour 
{
    private static Player player = null;
    public static Player instance { get { return player; } }

    private bool inGreyWorld = true;

    public Transform StartPoint;
    float speed = 1;
    public float moveSpeed;
    public GameObject deathParticles_colour, deathParticles_grey, birthParticles_grey, birthParticles_colour;

    [SerializeField] private Rigidbody2D rigidBody = null;
    [SerializeField]
    private Transform frontPivot = null;

    [SerializeField]
    private ParticleSystem colourTrail, greyTrail;

    private PlayerStates state = PlayerStates.ACTIVE;
    public PlayerStates playerState { get { return state; } }

    [SerializeField] private SpriteRenderer greyRenderer = null;
    [SerializeField] private SpriteRenderer colourRenderer = null;

    private void Awake()
    {
        tempMoveSpeed = moveSpeed;
        tempSpeed = speed;
        player = this;
        transform.position = StartPoint.position;
        //transform.position = startPos;
        Physics2D.IgnoreLayerCollision(8, 10, true);
    }
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        if (Input.GetKeyDown(KeyCode.Space))
            StartCoroutine(Die());

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
        if (frontPivot.position.x < Camera.main.ViewportToWorldPoint(new Vector3(screenTransition.instance.val, 0.5f, 10)).x)
        {
            SwitchWorld(false);
        }
        else
        {
            SwitchWorld(true);
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
        rigidBody.AddForce(accInput * Time.deltaTime * speed);
        
        transform.rotation = Quaternion.LookRotation(Vector3.forward, accInput);

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rigidBody.AddForce(new Vector2(moveSpeed * -Time.deltaTime, 0.0f));
            transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.left);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rigidBody.AddForce(new Vector2(moveSpeed * Time.deltaTime, 0.0f));
            transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.right);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rigidBody.AddForce(new Vector2(0.0f, moveSpeed * Time.deltaTime));
            transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            rigidBody.AddForce(new Vector2(0.0f, moveSpeed * -Time.deltaTime));
            transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.down);
        }
    }

    private void SwitchWorld(bool _world)
    {
        if (inGreyWorld != _world)
        {
            inGreyWorld = _world;
            Physics2D.IgnoreLayerCollision(8, 10, inGreyWorld);
            Physics2D.IgnoreLayerCollision(9, 10, !inGreyWorld);
        }
    }

    float tempMoveSpeed;
    float tempSpeed;

    public IEnumerator Die()
    {
        if (playerState != PlayerStates.DEAD)
        {
            state = PlayerStates.DEAD;
            greyRenderer.enabled = false;
            colourRenderer.enabled = false;
            colourTrail.Stop();
            greyTrail.Stop();
            moveSpeed = 0;
            speed = 0;
            GameObject dp = Instantiate(deathParticles_colour, transform.position, transform.rotation) as GameObject;
            GameObject dp2 = Instantiate(deathParticles_grey, transform.position, transform.rotation) as GameObject;
            StartCoroutine(screenTransition.instance.Shake(.25f));
            yield return new WaitForSeconds(2);
            Destroy(dp);
            Destroy(dp2);

            transform.position = StartPoint.position;
            GameObject bp = Instantiate(birthParticles_colour, transform.position, transform.rotation) as GameObject;
            GameObject bp2 = Instantiate(birthParticles_grey, transform.position, transform.rotation) as GameObject;

            yield return new WaitForSeconds(.75f);
            moveSpeed = tempMoveSpeed;
            speed = tempSpeed;
            colourTrail.Play();
            greyTrail.Play();
            greyRenderer.enabled = true;
            colourRenderer.enabled = true;
            state = PlayerStates.ACTIVE;

            yield return new WaitForSeconds(.25f);
            Destroy(bp);
            Destroy(bp2);
        }
    }
    public enum PlayerStates
    {
        ACTIVE,
        DEAD,
    }

    void OnTriggerEnter2D(Collider2D _col)
    {
        if (_col.tag == "KillArea")
        {
            StartCoroutine("Die");
        }
    }

}
