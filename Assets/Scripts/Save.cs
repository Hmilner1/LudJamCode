using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save
{
    public float playerPosX = -2.963f;
    public float playerPosY = -0.778f;

    public void updateX(float xPos)
    {
        playerPosX = xPos;
    }

    public void updateY(float yPos)
    {
        playerPosY = yPos;
    }
    
    public float returnX()
    {
        return playerPosX;
    }

    public float returnY()
    {
        return playerPosY;
    }

    public void resetGame()
    {
        playerPosX = -2.963f;
        playerPosY = -0.778f;
    }
}
