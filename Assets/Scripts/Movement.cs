using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody2D m_RB;
    public GameObject ballPrefab;
    public GameObject launchIndicator;
    public BoxCollider2D selfCollider;
    public GameObject winText;
    private float heightOffset = 0.2f;
    public Animator animator;
    public float m_MoveSpeed = 10.0f;
    public float jumpAmount = 35;
    public float gravityScale = 10;
    public float fallingGravityScale = 40;
    private float previousGravityScale;
    public float gravityIncreasePerSecond = 0.1f;
    public float ballHitSpeed = 2f;
    private bool ballHit = false;
    private bool isGrounded = true;
    public float animSpeed = 0.8f;
    public float ballSpawnTime = 0.25f;
    private float timer = 0f;
    private bool timerOn = false;
    private bool sliding = false;
    public float windSpeed = 10f;
    private bool notBounced = true;
    //public float test = 4f;
    private AudioSource footStep;
    public ParticleSystem Dust;

    // Start is called before the first frame update
    void Start()
    {
        m_RB = GetComponent<Rigidbody2D>();
        footStep = GetComponent<AudioSource>();
        previousGravityScale = fallingGravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        animator.speed = 1;
        if (GameObject.FindGameObjectsWithTag("Ball").Length == 0)
        {
            m_RB.gravityScale = 1;
            if (isGrounded)
            {
                fallingGravityScale = previousGravityScale;
                if ((m_RB.velocity.x > 0.02f && m_RB.velocity.y == 0f && Input.GetKey(KeyCode.D)) || (m_RB.velocity.x > 0.3f))
                {
                    animator.SetInteger("Move", 2);
                    animator.speed = (Mathf.Abs((m_RB.velocity.x * animSpeed)*m_MoveSpeed/4));
                    CreateDust();
                } else if ((m_RB.velocity.x < -0.02f && m_RB.velocity.y == 0f && Input.GetKey(KeyCode.A)) || (m_RB.velocity.x < -0.3f))
                {
                   animator.SetInteger("Move", 1);
                   animator.speed = (Mathf.Abs((m_RB.velocity.x * animSpeed)*m_MoveSpeed/4));
                    CreateDust();
                } else
                {
                    animator.SetInteger("Move", 0);
                    animator.speed = 1;
                }
                
                if (Input.GetKey(KeyCode.A))
                {
                    m_RB.AddForce(-transform.right * m_MoveSpeed, ForceMode2D.Force);
                }
                if (Input.GetKey(KeyCode.D))
                {
                    m_RB.AddForce(transform.right * m_MoveSpeed, ForceMode2D.Force);
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    SoundManagerScript.PlaySound("Jump");
                    m_RB.AddForce(Vector2.up * jumpAmount, ForceMode2D.Impulse);
                }
            } else if (!sliding && fallingGravityScale < 3)
            {
                fallingGravityScale += gravityIncreasePerSecond*(Time.deltaTime/2);
            }

            if (m_RB.velocity.y >= 0)
            {
                m_RB.gravityScale = gravityScale;
            }
            else if (m_RB.velocity.y < -1.2f)
            {
                m_RB.gravityScale = fallingGravityScale;
                Quaternion neutralRotation = Quaternion.identity;
                if (transform.rotation != neutralRotation)
                {
                    transform.rotation = neutralRotation;
                }
                animator.SetInteger("Move", 7);
                animator.speed = Mathf.Abs((m_RB.velocity.y * animSpeed) * m_MoveSpeed);
            }
        }
        else
        {
            m_RB.velocity = Vector3.zero;
            m_RB.gravityScale = 0.4f;
        }

        //Ball spawn timer code
        if (timerOn)
        {
            if (timer <= 0.25f)
            {
                animator.SetInteger("Move", 5);
            }
            if (timer <= 0)
            {
                animator.SetInteger("Move", 8);
                timerOn = false;
                Instantiate(ballPrefab, transform.position + Vector3.up * heightOffset, Quaternion.identity);
                launchIndicator.SetActive(true);
            } else
            {
                timer -= Time.deltaTime;
            }
        }

        //Activate ball spawn timer
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            if (GameObject.FindGameObjectsWithTag("Ball").Length == 0 && !ballHit)
            {
                timer = ballSpawnTime;
                timerOn = true;
                animator.SetInteger("Move", 5);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void FixedUpdate()
    {
        //Debug.Log(m_RB.velocity.y);
    }

    //Ball and reset ball code
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            GameObject ball = GameObject.FindGameObjectWithTag("Ball");
            SoundManagerScript.PlaySound("pop");
            transform.rotation = Quaternion.LookRotation(Vector3.forward, ball.transform.position - transform.position);
            m_RB.AddForce(transform.up * -ballHitSpeed, ForceMode2D.Impulse);
            if (m_RB.velocity.x > 0)
            {
                animator.SetInteger("Move", 9);
            } else
            {
                animator.SetInteger("Move", 6);
            }
            ballHit = true;
            launchIndicator.SetActive(false);
            Destroy(ball);
        } else if (collision.gameObject.tag == "ResetBall")
        {
            GameObject resetBall = GameObject.FindGameObjectWithTag("ResetBall");
            transform.rotation = Quaternion.LookRotation(Vector3.forward, resetBall.transform.position - transform.position);
            m_RB.AddForce(transform.up * -ballHitSpeed, ForceMode2D.Impulse);
            if (m_RB.velocity.x > 0)
            {
                animator.SetInteger("Move", 9);
            }
            else
            {
                animator.SetInteger("Move", 6);
            }
        } else if (collision.gameObject.tag == "End")
        {
            animator.speed = 1;
            Quaternion neutralRotation = Quaternion.identity;
            if (transform.rotation != neutralRotation)
            {
                transform.rotation = neutralRotation;
            }
            animator.SetInteger("Move", 0);
            gameObject.GetComponent<Movement>().enabled = false;
            winText.transform.localPosition = new Vector3(722.6f, 10521.2f, 0f);
        }
    }

    //Add wind force
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "WindRight")
        {
            m_RB.AddForce(new Vector3(1.0f, 0, 0) * windSpeed, ForceMode2D.Force);
        } else if (collision.gameObject.tag == "WindLeft")
        {
            m_RB.AddForce(new Vector3(-1.0f, 0, 0) * windSpeed, ForceMode2D.Force);
        }
    }

    //Set grounded and sliding to false
    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isGrounded = false;
            sliding = false;
        }
    }

    //Bounce off walls (very messy but it mostly works)
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor" && collision.collider.bounds.max.y > transform.localPosition.y && collision.collider.bounds.min.y < selfCollider.bounds.max.y && (animator.GetInteger("Move") == 6 || animator.GetInteger("Move") == 9) && notBounced)
        {
            transform.rotation = Quaternion.Inverse(transform.rotation);
            m_RB.AddForce(transform.up * -(ballHitSpeed / 5), ForceMode2D.Impulse);
            notBounced = false;
        }
        else if (collision.gameObject.tag == "Floor" && collision.collider.bounds.max.y > transform.localPosition.y && collision.collider.bounds.min.y < selfCollider.bounds.max.y && (animator.GetInteger("Move") == 6 || animator.GetInteger("Move") == 9) && !notBounced)
        {
            Quaternion neutralRotation = Quaternion.identity;
            if (transform.rotation != neutralRotation)
            {
                transform.rotation = neutralRotation;
            }
            animator.SetInteger("Move", 7);
        }

        if (collision.gameObject.tag == "Floor" && collision.collider.bounds.max.y < selfCollider.bounds.min.y && collision.transform.rotation.z == 0)
        {
            SoundManagerScript.PlaySound("Ground");
        }
    }

    //Check if on flat or tilted ground
    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor" && collision.collider.bounds.max.y < selfCollider.bounds.min.y && collision.transform.rotation.z == 0 && m_RB.velocity.y < 0.2f)
        {
            ballHit = false;
            notBounced = true;
            isGrounded = true;
            Quaternion neutralRotation = Quaternion.identity;
            if (transform.rotation != neutralRotation)
            {
                transform.rotation = neutralRotation;
            }
        } else if (collision.gameObject.tag == "Floor" && collision.collider.bounds.max.y < selfCollider.bounds.min.y && collision.transform.rotation.z != 0 && m_RB.velocity.y < 0.2f)
        {
            sliding = true;
            Quaternion neutralRotation = Quaternion.identity;
            if (transform.rotation != neutralRotation)
            {
                transform.rotation = neutralRotation;
            }
            if (m_RB.velocity.y < -0.7f)
            {
                animator.SetInteger("Move", 7);
            }
        } 
    }

    private void Footstep()
    {
        footStep.pitch = Random.Range(1f, 1.3f);
        footStep.Play();
    }

    private void CreateDust()
    {
        Dust.Play();
    }
}
