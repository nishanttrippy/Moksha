using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastExplosion : MonoBehaviour
{
    // Start is called before the first frame update
    float spawnTime;
    float existTime = 0.5f;
    void Start()
    {
        spawnTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - spawnTime > existTime)
            Destroy(gameObject);
    }
}
