using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoader : MonoBehaviour
{
    public Save save;
    public GameObject playerCharacter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //DELETE THIS
        if (Input.GetKeyUp(KeyCode.Q))
        {
            Save();
        }
        //DELETE THIS
        if (Input.GetKeyUp(KeyCode.E))
        {
            Load();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetGame();
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Save();
        }
    }

    private void Save()
    {
        save.playerPosX = playerCharacter.transform.position.x;
        save.playerPosY = playerCharacter.transform.position.y;
        SaveManager.SaveGame(save);
        Debug.Log("Saved data.");
    }

    private void Load()
    {
        save = SaveManager.LoadGame();
        float tempX = save.playerPosX;
        float tempY = save.playerPosY;
        playerCharacter.transform.position = new Vector2(tempX, tempY);
        Debug.Log("Loaded data.");
    }

    private void ResetGame()
    {
        save.resetGame();
        float tempX = save.playerPosX;
        float tempY = save.playerPosY;
        playerCharacter.transform.position = new Vector2(tempX, tempY);
        Save();
        Debug.Log("Reset game.");
    }
}
