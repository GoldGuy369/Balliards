using UnityEngine;
using System.Collections.Generic;

public class BallSpawner : MonoBehaviour
{
    public Ball ballPrefab;
    public int rows = 4; //row of balls
    public float spacing = 0.2f;
    public float depth = 0.6f;
    public Sprite[] ballSprites;
    public List<Ball> Balls { get; } = new List<Ball>();        //list of balls with a getter

    public void Spawn()
    {
        Clear();
        int n = 0;
        for (int row = 0; row < rows; row++)
        {
            for (int i= 0; i <= row; i++)
            {
                //find place to spawn ball
                Vector2 offset = new Vector2(row * spacing * depth, (i - row * 0.5f) * spacing);
                Ball b = Instantiate(ballPrefab, (Vector2)transform.position + offset, Quaternion.identity);

                //ball constructor
                int points = 1 + (n % 5);
                Sprite sprite = ballSprites[n];
                b.ballSetup(points, sprite);

                Balls.Add(b);
                n++;
            }
        }
    }

    public void Start()
    {
        Spawn();
    }
    public void Clear()
    {
        foreach (Ball ball in Balls) { if (ball != null) Destroy(ball.gameObject); }
        Balls.Clear();
    }

    public bool checkMovement()
    {
        //if a ball ismoving return true
        foreach (Ball ball in Balls)
            if (ball != null && !ball.potted && ball.isMoving) return true;
        return false;
    }


    //return number of balls that haven't been potted
    public int ballsInPlay
    {
        get{
            int n = 0;
            foreach (Ball ball in Balls)
            {
                if (ball != null && !ball.potted) n++;
                
            }
            return n;
        }
    }
}
