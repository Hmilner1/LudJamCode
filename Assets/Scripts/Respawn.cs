using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    private Rigidbody2D m_RB;
    public GameObject respawnHeight;
    private Vector2 spawnPoint;

    private void OnEnable()
    {
        m_RB = GetComponent<Rigidbody2D>();
        spawnPoint = m_RB.transform.position;
}

    public void FixedUpdate()
    {
        if (m_RB.position.y < respawnHeight.transform.position.y)
        {
            m_RB.position = spawnPoint;
        }
    }
}
