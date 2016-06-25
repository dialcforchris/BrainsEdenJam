using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour 
{
    private bool inGreyWorld = true;

    public Transform StartPoint;
    float speed = 6;
    public float moveSpeed;
    public GameObject deathParticles,birthParticles;
    ParticleSystem ps;

    [SerializeField]
    private Transform frontPivot = null;

    private void Awake()
    {
        transform.position = StartPoint.position;
        ps = GetComponent<ParticleSystem>();
        //transform.position = startPos;
        Physics2D.IgnoreLayerCollision(8, 10, true);
    }
	
	// Update is called once per frame
	void Update () 
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
        transform.position += accInput * Time.deltaTime * speed;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, accInput);
        if (Vector3.Distance(Vector3.zero, transform.position) > 25)
            transform.position = Vector3.zero;
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position = new Vector3(transform.position.x - (moveSpeed * Time.deltaTime), transform.position.y, 0.0f);
            transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.left);
            //GetComponent<Rigidbody2D>().AddForce(new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y));
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position = new Vector3(transform.position.x + (moveSpeed * Time.deltaTime), transform.position.y, 0.0f);
            transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.right);
            //GetComponent<Rigidbody2D>().AddForce(new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y));
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + (moveSpeed * Time.deltaTime), 0.0f);
            transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
            //GetComponent<Rigidbody2D>().AddForce(new Vector2(transform.position.x , transform.position.y + moveSpeed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - (moveSpeed * Time.deltaTime), 0.0f);
            transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.down);
            //GetComponent<Rigidbody2D>().AddForce(new Vector2(transform.position.x, transform.position.y - moveSpeed * Time.deltaTime));
        }
    }

    private void SwitchWorld(bool _world)
    {
        if (inGreyWorld != _world)
        {
            ps.startColor = !_world ? Color.black : Color.white;
            inGreyWorld = _world;
            Physics2D.IgnoreLayerCollision(8, 10, inGreyWorld);
            Physics2D.IgnoreLayerCollision(9, 10, !inGreyWorld);
        }
    }

    public IEnumerator Die()
    {
        float tempMoveSpeed = moveSpeed;
        float tempSpeed = speed;
        GetComponent<SpriteRenderer>().enabled = false;
        ps.Stop();
        moveSpeed = 0;
        speed = 0;
        GameObject dp = Instantiate(deathParticles, transform.position, transform.rotation) as GameObject;
        GameObject dp2 = Instantiate(dp) as GameObject;
        dp.layer = 8;
        dp2.layer = 9;
        dp2.GetComponent<ParticleSystem>().startColor = Color.white;
        StartCoroutine(screenTransition.instance.Shake(.25f));
        yield return new WaitForSeconds(2);
        Destroy(dp);

        transform.position = StartPoint.position;
        GameObject bp = Instantiate(birthParticles, transform.position, transform.rotation) as GameObject;
        bp.layer = 8;
        GameObject bp2 = Instantiate(birthParticles, transform.position, transform.rotation) as GameObject;
        bp2.layer = 9;
        bp2.GetComponent<ParticleSystem>().startColor = Color.white;

        yield return new WaitForSeconds(.75f);
        moveSpeed = tempMoveSpeed;
        speed = tempSpeed;
        ps.Play();
        GetComponent<SpriteRenderer>().enabled = true;

        yield return new WaitForSeconds(.25f);
        Destroy(bp);
        Destroy(bp2);
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
            Debug.Log("kill ");
            StartCoroutine("Die");
        }
    }

}
