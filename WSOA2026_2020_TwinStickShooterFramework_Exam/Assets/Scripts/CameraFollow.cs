using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    ////////// PUBLIC FIELDS //////////
    public GameObject player;
    public float yOffset = 10;
    public float zOffset = -7.5f;
    public float smoothTime = 0.3f;

    ////////// PRIVATE FIELDS //////////
    Vector3 offset;
    Vector3 velocity = Vector3.zero;


    private void Start()
    {
        offset = new Vector3(0, yOffset, zOffset);
    }

    void Update()
    {
        transform.position = Vector3.SmoothDamp( transform.position, player.transform.position + offset, ref velocity, smoothTime);
    }
}
