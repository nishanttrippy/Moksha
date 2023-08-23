using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Protagonist_0")
        {
            GameManager.instance.BeginBossBattle();
            Destroy(gameObject);
        }
    }
}
