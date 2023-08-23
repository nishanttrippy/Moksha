using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastSpirits : MonoBehaviour
{
    public Vector3 moveDelta;
    private CircleCollider2D spiritCollider;
    public bool isProjectileReady = false;
    private RaycastHit2D hit;
    public GameObject spiritExplosion;


    private void Start()
    {
        spiritCollider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isProjectileReady)
        {
            transform.Translate(moveDelta.x * Time.deltaTime, moveDelta.y * Time.deltaTime, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Protagonist_0")
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(30);
            Instantiate(spiritExplosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void SetSpiritParams(Vector3 direction)
    {
        moveDelta = direction;
        isProjectileReady = true;
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(3.0f);

        Instantiate(spiritExplosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
