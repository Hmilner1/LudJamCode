using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusicScript : MonoBehaviour
{
    AudioSource m_AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        m_AudioSource.loop = true;
        m_AudioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
