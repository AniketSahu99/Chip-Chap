using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class LevelCreation : MonoBehaviour
{

    public bool buildBrick = false;

    public GameObject startPos;
    public float offset;
    public int row_size;
    public int col_size;

    public Shape shape;
    public Gradient gradient;
    public enum Shape
    {
        rectangle,
        triangle,
        invertedTriangle
    }

    public GameObject brick;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (buildBrick && shape == Shape.rectangle)
        {
            Vector3 spawnPos = startPos.transform.position;
            for(int i = 1; i <= row_size; i++)
            {
                for(int j = 1; j <= col_size; j++)
                {
                    GameObject newObj = Instantiate(brick, spawnPos, Quaternion.identity);

                    if (newObj.tag == "Brick")
                        newObj.GetComponent<SpriteRenderer>().color = gradient.Evaluate((float)i / row_size);

                    spawnPos += new Vector3(offset, 0, 0);
                }
                spawnPos = new Vector3(startPos.transform.position.x, spawnPos.y - offset, 0);
            }
            buildBrick = false;
        }

        else if(buildBrick && shape == Shape.triangle)
        {
            Vector3 spawnPos = startPos.transform.position;
            for (int i = 1; i <= row_size; i++)
            {
                for (int j = 1; j <= i; j++)
                {
                    GameObject newObj = Instantiate(brick, spawnPos, Quaternion.identity);

                    if (newObj.tag == "Brick")
                        newObj.GetComponent<SpriteRenderer>().color = gradient.Evaluate((float)i / row_size);

                    spawnPos += new Vector3(offset, 0, 0);
                }
                spawnPos = new Vector3(startPos.transform.position.x, spawnPos.y - offset, 0);
            }
            buildBrick = false;
        }

        else if (buildBrick && shape == Shape.invertedTriangle)
        {
            Vector3 spawnPos = startPos.transform.position;
            for (int i = 1; i <= row_size; i++)
            {
                for (int j = i; j <= col_size; j++)
                {
                    GameObject newObj = Instantiate(brick, spawnPos, Quaternion.identity);

                    if (newObj.tag == "Brick")
                        newObj.GetComponent<SpriteRenderer>().color = gradient.Evaluate((float)i / row_size);

                    spawnPos += new Vector3(offset, 0, 0);
                }
                spawnPos = new Vector3(startPos.transform.position.x, spawnPos.y - offset, 0);
            }
            buildBrick = false;
        }
    }
}
