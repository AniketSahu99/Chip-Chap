using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  PowerUps : MonoBehaviour
{
    [SerializeField]
    private float speed = 30f;

    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, -10 * speed);
    }

}
