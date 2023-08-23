using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform lookAt;
    public float boundX = 0.3f;
    public float boundY = 0.15f;
    private GameObject Player;
    private PlayerMove playerMove;

    bool hasBossTriggeredOnce = false;

    private void Start()
    {
        Player = GameObject.Find("Protagonist_0");
        lookAt = Player.transform;
        playerMove = Player.GetComponent<PlayerMove>();
    }

    private void LateUpdate()
    {
        Vector3 delta = Vector3.zero;

        float deltaX = lookAt.position.x - transform.position.x;
        float deltaY = lookAt.position.y - transform.position.y;

        //To check if player is within the bounds of the camera in x-axis
        if (deltaX > boundX || deltaX < -boundX)
        {
            if (transform.position.x < lookAt.position.x)
            {
                delta.x = deltaX - boundX;
            }
            else
            {
                delta.x = deltaX + boundX;
            }
        }

        //To check if player is within the bounds of the camera in y-axis
        if (deltaY > boundY || deltaY < -boundY)
        {
            if (transform.position.y < lookAt.position.y)
            {
                delta.y = deltaY - boundY;
            }
            else
            {
                delta.y = deltaY + boundY;
            }
        }

        transform.position += new Vector3(delta.x, delta.y, 0);

        if(GameManager.instance.hasBossBattleStarted && !hasBossTriggeredOnce)
        {
            StartCoroutine(BossCamera());
            hasBossTriggeredOnce = true;
        }
    }

    IEnumerator BossCamera()
    {
        lookAt = GameObject.Find("DeathAngelBoss").transform;
        playerMove.enabled = false;

        yield return new WaitForSeconds(2.0f);

        lookAt = GameObject.Find("Protagonist_0").transform;
        playerMove.enabled = true;
    }
}
