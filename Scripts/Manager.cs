using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] bricks;

    private int initialLength;

    private bool firstTime = true;
    
    public bool ballLaunced = false;

    private GameObject ball;

    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform ballSpawn;

    [SerializeField]
    private RandomLevelGenerator randomLevelGenerator;
    // Start is called before the first frame update
    void Awake()
    {
        bricks = GameObject.FindGameObjectsWithTag("Brick");
        initialLength = bricks.Length;
    }

    public void LoseAction()
    {
        StartCoroutine(SpawnBall());
    }

    IEnumerator SpawnBall()
    {
        Debug.Log("Game Over");
        yield return new WaitForSeconds(2f);
        GameObject ballNew = Instantiate(ballPrefab, ballSpawn.position, Quaternion.identity);
        yield return new WaitForSeconds(2f);
        firstTime = true;
    }
    // Update is called once per frame
    void Update()
    {
        /*if(ballLaunced)
        {
            GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
            foreach(GameObject ball in balls)
            {
                if (ball.GetComponent<Rigidbody2D>().velocity.magnitude == 0)
                    ball.GetComponent<Rigidbody2D>().velocity = new Vector2(0,-10);
            }
        }*/

        if(randomLevelGenerator != null && firstTime)
        {
             if(randomLevelGenerator.spawnComplete)
            {
                ball = GameObject.FindGameObjectWithTag("Ball");
                ball.GetComponent<Rigidbody2D>().velocity = new Vector2 (0, -10 * 50);
                bricks = GameObject.FindGameObjectsWithTag("Brick");
                initialLength = bricks.Length;
                firstTime = false;
            }
             else if(firstTime && randomLevelGenerator == null)
            {
                ball = GameObject.FindGameObjectWithTag("Ball");
                ball.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -10 * 50);
            }
        }
        else if(firstTime)
        {
            firstTime = false;
            StartCoroutine(LaunchBall());
        }
        CheckWin();
    }

    IEnumerator LaunchBall()
    {
        ballLaunced = true;
        yield return new WaitForSeconds(2f);
        ball = GameObject.FindGameObjectWithTag("Ball");
        ball.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -10 * 50);
    }

    IEnumerator WinAction()
    {
        yield return new WaitForSeconds(8f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void CheckWin()
    {
        int currLen = 0;
        foreach (var item in bricks)
        {
            if(item == null) 
                currLen++;
        }

        if(randomLevelGenerator == null && currLen == initialLength)
        {
            VideoManager vidManager = GameObject.FindGameObjectWithTag("Video Manager").GetComponent<VideoManager>();
            vidManager.collided = true;
            vidManager.PlayGood();
            StartCoroutine(WinAction());
            
        }

        else if (randomLevelGenerator != null && randomLevelGenerator.spawnComplete && currLen == initialLength && firstTime == false)
        {
            VideoManager vidManager = GameObject.FindGameObjectWithTag("Video Manager").GetComponent<VideoManager>();
            vidManager.collided = true;
            vidManager.PlayGood();
            StartCoroutine(WinAction());
        }
    }
}
