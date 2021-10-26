using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchIndicator : MonoBehaviour
{
    public GameObject playerCharacter;
    private GameObject launchBall;
    public bool front = false;
    public SpriteRenderer sRenderer;
    private float clampMag;
    // Start is called before the first frame update
    void Start()
    {
        if (front)
        {
            clampMag = 0.1f;
        } else
        {
            clampMag = -0.1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        launchBall = GameObject.FindGameObjectWithTag("Ball");
        if (launchBall != null)
        {
            sRenderer.enabled = true;
            Vector3 ballPos = launchBall.transform.position;
            Vector3 playerPos = playerCharacter.transform.position;
            Vector3 allowedPos = playerPos - ballPos;
            allowedPos = Vector3.ClampMagnitude(allowedPos, clampMag);
            transform.position = playerPos + allowedPos;
        } else
        {
            sRenderer.enabled = false;
        }
    }
}
