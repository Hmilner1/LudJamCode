using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class Game : MonoBehaviour
{
    [SerializeField]
    private GameObject menu;
    [SerializeField]
    private GameObject playerCharacter;
    private bool isPaused = false;

    private void Awake()
    {
        
    }

    void Start()
    {
        Unpause();
    }

    public void Pause()
    {
        menu.SetActive(true);
        Time.timeScale = 0;
        playerCharacter.GetComponent<Movement>().enabled = false;
        isPaused = true;
    }

    public void Unpause()
    {
        menu.SetActive(false);
        Time.timeScale = 1;
        playerCharacter.GetComponent<Movement>().enabled = true;
        isPaused = false;
    }

    public bool IsGamePaused()
    {
        return isPaused;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (isPaused)
            {
                Unpause();
            }
            else
            {
                Pause();
            }
        }
    }
}
