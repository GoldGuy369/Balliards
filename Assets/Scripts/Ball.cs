using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))] 

public class Ball : MonoBehaviour
{
    public SpriteRenderer sprite;
    public int points = 1;
    public Rigidbody2D rb;
    public bool potted;
    const float minSpeed =0.1f;
    

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        rb.linearDamping = 1.0f; //table friction
        rb.angularDamping = 1.3f;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;        //stops faster balls going straight through
    }

    void FixedUpdate()
    {
        //if ball moving slower than threshold then stop
        if (rb.linearVelocity.sqrMagnitude < minSpeed * minSpeed)
            rb.linearVelocity = Vector2.zero;
    }
    //set ball to still when no longer moving
    public bool isMoving => rb.linearVelocity.sqrMagnitude > 0.001f;

    public void ballSetup(int pointValue, Sprite ballNumber)
    {
        points = pointValue;
        if (sprite != null && ballNumber !=null) 
            sprite.sprite = ballNumber;
    }

    //set potted ,remove speed and delete ball
    public void potBall()
    {
        potted = true;
        rb.linearVelocity = Vector2.zero;
        gameObject.SetActive(false);
    }
}
