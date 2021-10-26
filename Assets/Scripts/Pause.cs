using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    private bool pauseToggle = false;
    public GameObject playerCharacter;
    public GameObject pauseSprite;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Canvas>().enabled = false;
        pauseSprite.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (pauseToggle)
        {
            Time.timeScale = 0;
            playerCharacter.GetComponent<Movement>().enabled = false;
            pauseSprite.GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<Canvas>().enabled = true;
        }
        else
        {
            Time.timeScale = 1;
            playerCharacter.GetComponent<Movement>().enabled = true;
            pauseSprite.GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Canvas>().enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            pauseToggle = !pauseToggle;
        }
    }
}
