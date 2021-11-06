using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    float horizontal;
    float vertical;

    public Rigidbody2D body;
    public GameObject ballPrefab;
    private Vector3 mousePosition;

    public float moveSpeed = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SoundManagerScript.PlaySound("pop");
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        body.AddForce(transform.up, ForceMode2D.Force);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePosition - transform.position);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerCharacter")
        {
            //Debug.Log("PlayerHit");
        }
        else
        {
            //Debug.Log("OtherHit");
            if (collision.tag != "WindRight" && collision.tag != "WindLeft")
            {
                SoundManagerScript.PlaySound("pop");
                Destroy(gameObject);
            }
        }
    }
}
