using System;
using System.Collections;
using UnityEngine;

public class BarMovement : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private float left_most;

    [SerializeField]
    private float right_most;

    [SerializeField]
    private float top_most;

    [SerializeField]
    private float bottom_most;

    [SerializeField]
    private float y_offset = 0;

    [SerializeField]
    private Ball ball;

    private Manager manager;

    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
        speed = ball.speed;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            var mousePosition = Input.mousePosition;

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            var pos = cam.ScreenToWorldPoint(mousePosition);

            if (hit.collider != null && hit.collider.tag == "Bar" && CheckInsideEllipse(pos.x,pos.y - y_offset))
            {
                this.transform.position = new Vector3(pos.x,pos.y,0);
            }
        }
    }

    private bool CheckInsideEllipse(float x, float y)
    {
        float a = (right_most - left_most) / 2.0f;
        float b = (top_most - bottom_most) / 2.0f;

        return ((x*x)/(a*a) + (y*y)/(b*b) < 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.velocity = Vector3.zero;
            var dir = collision.transform.position - this.transform.position;
            rb.velocity = (dir.normalized * speed);
        }

        else if(collision.gameObject.tag == "3x")
        {
            ThreeXMultiplier();
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.tag == "5x")
        {
            FiveXMultiplier();
            Destroy(collision.gameObject);
        }
    }

    public void FiveXMultiplier()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");

        foreach (GameObject ball in balls)
        {
            var velocity = ball.GetComponent<Rigidbody2D>().velocity;
            GameObject left1Ball = Instantiate(ball, ball.transform.position, Quaternion.identity);
            GameObject left2Ball = Instantiate(ball, ball.transform.position, Quaternion.identity);

            GameObject right1Ball = Instantiate(ball, ball.transform.position, Quaternion.identity);
            GameObject right2Ball = Instantiate(ball, ball.transform.position, Quaternion.identity);

            var theta = Mathf.Atan2(velocity.y, velocity.x);

            var v = velocity.magnitude;
            left2Ball.GetComponent<Rigidbody2D>().velocity = new Vector2(v * (float)Math.Cos(theta + Math.PI / 6), v * (float)Math.Sin(theta + Math.PI / 6));
            right2Ball.GetComponent<Rigidbody2D>().velocity = new Vector2(v * (float)Math.Cos(theta - Math.PI / 6), v * (float)Math.Sin(theta - Math.PI / 6));

            left1Ball.GetComponent<Rigidbody2D>().velocity = new Vector2(v * (float)Math.Cos(theta + Math.PI / 12), v * (float)Math.Sin(theta + Math.PI / 12));
            right1Ball.GetComponent<Rigidbody2D>().velocity = new Vector2(v * (float)Math.Cos(theta - Math.PI / 12), v * (float)Math.Sin(theta - Math.PI / 12));

        }
    }

    public void ThreeXMultiplier()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");

        foreach (GameObject ball in balls)
        {
            var velocity = ball.GetComponent<Rigidbody2D>().velocity;
            GameObject leftBall = Instantiate(ball, ball.transform.position, Quaternion.identity);
            GameObject rightBall = Instantiate(ball, ball.transform.position, Quaternion.identity);

            var theta = Mathf.Atan2(velocity.y, velocity.x);

            var v = velocity.magnitude;
            leftBall.GetComponent<Rigidbody2D>().velocity = new Vector2(v * (float)Math.Cos(theta + Math.PI / 6), v * (float)Math.Sin(theta + Math.PI / 6));
            rightBall.GetComponent<Rigidbody2D>().velocity = new Vector2(v * (float)Math.Cos(theta - Math.PI / 6), v * (float)Math.Sin(theta - Math.PI / 6));

        }
    }

}
