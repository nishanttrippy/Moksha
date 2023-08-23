using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angel : MonoBehaviour
{

    private float xSpeed;
    private float ySpeed;

    Angel()
    {
        xSpeed = 0.5f;
        ySpeed = 0.0f;
    }

    Angel(float x, float y)
    {
        xSpeed = x;
        ySpeed = y;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 Move()
    {
        return new Vector3(xSpeed, ySpeed, 0);
    }

    public void Despawn()
    {
        Destroy(gameObject);
    }
}
