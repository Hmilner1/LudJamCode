using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicsObject
{
    public float jumpTakeOffSpeed = 7f;
    public float maxSpeed = 7f;
    public float ballHitSpeed = 2f;
    private float ballHitAngle;
    //private Vector2 lastBallPos;
    public Rigidbody2D body;
    //public Animator animator;
    
    //private bool ballHit = false;
    private bool beingLaunched = false;
    public int interpolationFramesCount = 45;
    private int elapsedFrames = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;
        if (GameObject.FindGameObjectsWithTag("Ball").Length == 0 && !beingLaunched)
        {
            //animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
            move.x = Input.GetAxis("Horizontal");

            if (Input.GetButtonDown("Jump") && grounded)
            {
                velocity.y = jumpTakeOffSpeed;
            }
            else if (Input.GetButtonUp("Jump"))
            {
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * .5f;
                }
            }
            targetVelocity = move * maxSpeed;
        }
        else if (!beingLaunched)
        {
            move.x = 0;
            if (!grounded)
            {
                velocity.y = 0;
                gravityModifier = 0.1f;
            }
        } else if (GameObject.FindGameObjectsWithTag("Ball").Length == 0 && beingLaunched)
        {
            
            //elapsedFrames = (elapsedFrames + 1) % (interpolationFramesCount + 1);
            
            //Debug.Log(elapsedFrames);
            //Debug.Log(beingLaunched);
            if (elapsedFrames >= 200)
            {
                elapsedFrames = 0;
                gravityModifier = 1f;
                body.velocity = Vector2.zero;
                beingLaunched = false;
            } else
            {
                velocity.x = transform.position.x * ballHitAngle * ballHitSpeed;
                velocity.y = transform.position.y * ballHitAngle * ballHitSpeed;
                elapsedFrames += 1;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        beingLaunched = true;
        GameObject ball = GameObject.FindGameObjectWithTag("Ball");
        Vector3 dir = ball.transform.position - transform.position;
        dir = ball.transform.InverseTransformDirection(dir);
        ballHitAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Debug.Log(ballHitAngle);
        Destroy(ball);
        //lastBallPos = collision.transform.position;
        //body.velocity = new Vector2(Mathf.Pow(3, .5f) / 2, 1 / 2) * ballHitSpeed;
        //float velx = ballHitSpeed * Mathf.Cos(relativeAngle * Mathf.Deg2Rad);
        //float vely = ballHitSpeed * Mathf.Sin(relativeAngle * Mathf.Deg2Rad);
        //body.velocity = new Vector2(velx, vely);
    }
}
