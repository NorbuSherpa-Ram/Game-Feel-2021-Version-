using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    [SerializeField] private float collisionIgnoreTime = 1f;
    [SerializeField] private BoxCollider2D myCollider;
    private bool isOnPlatform;

    void Update()
    {
        if (!isOnPlatform) return;
        if (PlayerController.Instance.frameInput.Move.y < 0)
        {
            StartCoroutine(IgnorePlatformCollision());
        }
    }
    private IEnumerator IgnorePlatformCollision()
    {
        Collider2D playerColider = PlayerController.Instance.GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(playerColider, myCollider, true);
        yield return new WaitForSeconds(collisionIgnoreTime);
        Physics2D.IgnoreCollision(playerColider, myCollider,false);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null) {
            isOnPlatform = true;
        }
    }
    private void OnCollisionExit(Collision other)
    {
        if(other.gameObject.GetComponent<PlayerController>()!=null){
            isOnPlatform = false;
        }
    }

}
