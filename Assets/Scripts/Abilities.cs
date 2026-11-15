using UnityEngine;
using System.Collections;
public class Abilities : MonoBehaviour
{
    public BallSpawner rack;
    public Camera cam;
    public Transform tableCentre;

    void Awake()
    {
        if (cam == null) cam = Camera.main; //set camera at start

    }

    public void useShake()
    {
        if (GameManager.instance.startMove()) StartCoroutine(shake());
    }
    public void useFlip()
    {
        if (GameManager.instance.startMove()) StartCoroutine(flip());
    }
    public void useTilt(int direction)
    {
        if (GameManager.instance.startMove()) StartCoroutine(tilt(direction));
    }
    public void useWind()
    {
        if (GameManager.instance.startMove()) StartCoroutine(wind());
    }


    IEnumerator shake()
    {
        float duration = 1f;
        float strength = 0.7f * GameManager.instance.stats.shakePower;
        Vector3 camStartPos = cam.transform.position;

        for (float time = 0f; time < duration; time += Time.fixedDeltaTime)
        {
            foreach (Ball ball in rack.Balls)
            {
                if (ball != null && !ball.potted)
                {
                    ball.rb.AddForce(Random.insideUnitCircle * strength, ForceMode2D.Impulse);  //send bvall ina a random direction
                }
            }
            cam.transform.position = camStartPos + (Vector3)(Random.insideUnitCircle * 0.1f);   //randomly shake camera
            yield return new WaitForFixedUpdate();
        }
        cam.transform.position = camStartPos; //send back to start position
        GameManager.instance.endMove();
    }

    //'flip table' to send balls
    IEnumerator flip()
    {
        float power = GameManager.instance.stats.flipPower;
        Vector2 centre = tableCentre != null ? (Vector2)tableCentre.position : Vector2.zero;

        foreach (Ball ball in rack.Balls)
        {
            //ignore potted balls
            if (ball == null || ball.potted) continue;
            //set direction of travel and if ball is in centre then randomly move
            Vector2 direction = ((Vector2)ball.transform.position - centre.normalized);
            if (direction == Vector2.zero)
                direction = Random.insideUnitCircle.normalized;
            ball.rb.AddForce(direction * 6f * power, ForceMode2D.Impulse);
        }
        
        
        //add camera spin here


        yield return new WaitForFixedUpdate();
        GameManager.instance.endMove();
    }


    IEnumerator tilt(int direction)
    {
        Vector2[] directions = { Vector2.up, Vector2.right, Vector2.left, Vector2.down };
        Vector2 d = directions[Mathf.Clamp(direction, 0, 3)];
        float force = 7f* GameManager.instance.stats.tiltPower;
        float duration = 1f;

        for (float t = 0f; t < duration; t += Time.fixedDeltaTime)
        {
            foreach(Ball ball in rack.Balls)
            {
                if (ball != null && !ball.potted)
                    ball.rb.AddForce(d * force, ForceMode2D.Force);

            }
            yield return new WaitForFixedUpdate();
        }
        GameManager.instance.endMove();
    }

    IEnumerator wind()
    {
        //not yet implemented
        yield return new WaitForFixedUpdate();
    }

}
