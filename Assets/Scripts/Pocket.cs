using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Pocket : MonoBehaviour
{
    void Reset()
    {
        GetComponent<CircleCollider2D>().isTrigger = true;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Ball ball = other.GetComponent<Ball>();
        if (ball == null || ball.potted) return;
        GameManager.instance.ballPotted(ball);
    }
}