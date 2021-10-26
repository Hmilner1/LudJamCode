using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBall : MonoBehaviour
{
    public GameObject ballPrefab;
    public GameObject playerCharacter;
    private float heightOffset = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            if (GameObject.FindGameObjectsWithTag("Ball").Length == 0)
            {
                Instantiate(ballPrefab, transform.position+Vector3.up*heightOffset, Quaternion.identity);
            }
        }
    }
}
