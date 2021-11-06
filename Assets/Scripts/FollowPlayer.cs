using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    //public GameObject playerCharacter;
    //private float startingPositionY;

    public Transform target;
    public Vector3 offset;
    [Range(1, 10)]
    public float smoothFactor;

    public Vector3 minValue, maxValue;

    void Start()
    {
        transform.position = new Vector2(target.position.x, Mathf.Clamp(target.position.y, -0.013f, 23.34f));
    }

    void Awake()
    {
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = 144;
    }

    void Update()
    {
        //Vector3 newPosition = new Vector3(Mathf.Clamp(playerCharacter.transform.position.x, -2.395f, -0.431f), Mathf.Clamp(playerCharacter.transform.position.y, startingPositionY, float.MaxValue));
        //transform.position = newPosition;
        Follow();
    }

    private void FixedUpdate()
    {

    }

    void Follow()
    {
        Vector3 targetPosition = target.position;
        Vector3 boundPosition = new Vector3(
            Mathf.Clamp(targetPosition.x, minValue.x, maxValue.x),
            Mathf.Clamp(targetPosition.y, minValue.y, maxValue.y),
            Mathf.Clamp(targetPosition.z, minValue.z, maxValue.z));
        Vector3 smoothPosition = Vector3.Lerp(transform.position, boundPosition, smoothFactor * Time.fixedDeltaTime);
        transform.position = smoothPosition;
    }
}
