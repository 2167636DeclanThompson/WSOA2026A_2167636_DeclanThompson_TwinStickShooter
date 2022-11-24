using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticleSystem : MonoBehaviour
{
    public float particleDelay = 0.5f;

    void Start()
    {
        Destroy(gameObject, particleDelay);
    }
}
