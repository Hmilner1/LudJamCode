using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip Fall, ballPop, wallPop, Ground, Jump;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        ballPop = Resources.Load<AudioClip>("pop");
        wallPop = Resources.Load<AudioClip>("wallpop");
        Ground = Resources.Load<AudioClip>("Ground");
        Jump = Resources.Load<AudioClip>("Jump");
        Fall = Resources.Load<AudioClip>("Fall");

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "pop":
                audioSrc.PlayOneShot(ballPop);
                audioSrc.pitch = Random.Range(0.8f, 1.3f);
                break;
            case "wallpop":
                audioSrc.PlayOneShot(wallPop);
                audioSrc.pitch = Random.Range(0.8f, 1.3f);
                break;
            case "Ground":
                audioSrc.PlayOneShot(Ground);
                audioSrc.pitch = Random.Range(0.8f, 1.3f);
                break;
            case "Jump":
                audioSrc.PlayOneShot(Jump);
                audioSrc.pitch = Random.Range(0.8f, 1.3f);
                break;
            case "Fall":
                audioSrc.PlayOneShot(Fall);
                audioSrc.pitch = Random.Range(0.8f, 1.3f);
                break;
        }
    }
}
