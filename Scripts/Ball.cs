using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    private Sprite[] ball_sprites;

    private Vector3 vel;
    public float speed = 50f;

    public BallColour ballColor;

    [SerializeField]
    private GameObject ThreeX;

    [SerializeField]
    private GameObject FiveX;

    private VideoManager videoManager;
    private Manager manager;

    public enum BallColour
    {
        blue = 0,
        white,
        yellow
    }


    void Start()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = ball_sprites[ChooseColour()];

        rb = GetComponent<Rigidbody2D>();

        videoManager = GameObject.FindGameObjectWithTag("Video Manager").GetComponent<VideoManager>();
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
    }

    private int ChooseColour()
    {
        int i = 0;
        switch (ballColor)
        {
            case BallColour.blue:
                i = 0;
                break;
            case BallColour.white:
                i = 1;
                break;
            case BallColour.yellow:
                i = 2;
                break;
        }
        return i;
    }
    // Update is called once per frame
    void Update()
    {
        vel = rb.velocity;
        CheckSpeed();
    }

    private void CheckSpeed()
    {
        if (rb.velocity.magnitude != speed)
        {
            rb.velocity = rb.velocity.normalized * speed;
        }
    }

    private void CheckLose()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        int count = 1;
        foreach (GameObject ball in balls)
        {
            if (ball == null)
                count++;
        }
        if (count == balls.Length)
        {
            VideoManager vidManager = GameObject.FindGameObjectWithTag("Video Manager").GetComponent<VideoManager>();
            vidManager.PlayBad();
            manager.LoseAction();
        }
    }

    private void CheckPowerUp(GameObject brick)
    {
        int r = Random.Range(1, 15);
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");

        int active = FindActiveBalls(balls);

        if (r == 4 && active <= 12)
        {
            Instantiate(ThreeX, brick.transform.position, Quaternion.identity);
        }
        else if (r == 3 && active <= 9)
        {
            Instantiate(FiveX, brick.transform.position, Quaternion.identity);
        }
    }

    private int FindActiveBalls(GameObject[] balls)
    {
        int active = 0;
        foreach (GameObject ball in balls)
        {
            if(ball != null)
                active++;
        }
        return active;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Reflect(collision);
        if(collision.gameObject.tag == "Death")
        {
            CheckLose();
            Destroy(this.gameObject);
        }
        else if(collision.gameObject.tag == "Brick")
        {
            CheckPowerUp(collision.gameObject);
            videoManager.collided = true;
            StartCoroutine(videoManager.StartVideo());
            videoManager.currTime = 0;
            Destroy(collision.gameObject);
        }
        

    }

    private void Reflect(Collision2D collision)
    {
        var dir = Vector3.Reflect(vel.normalized, collision.contacts[0].normal);
        rb.velocity = dir.normalized * speed;
        if (rb.velocity == Vector2.zero)
            Destroy(this.gameObject);
    }


}
