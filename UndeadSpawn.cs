using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndeadSpawn : MonoBehaviour
{
    public GameObject[] objectsToSpawn;
    private Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, playerTransform.position);

        if (distance <= 2.0f)
        {
            foreach (GameObject spawn in objectsToSpawn)
            {
                if (spawn != null)
                {
                    spawn.GetComponent<Animator>().SetTrigger("Rise");
                    spawn.GetComponent<Enemy>().Rebirth();
                }
            }
        }
    }
}
