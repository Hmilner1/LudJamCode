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

    // Start is called before the first frame update
    void Start()
    {
        //startingPositionY = transform.position.y;
    }

    void Awake()
    {
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = 144;
    }

    // Update is called once per frame
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
