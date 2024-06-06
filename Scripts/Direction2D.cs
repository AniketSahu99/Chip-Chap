using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Direction2D
{
    public static List<Vector2> randDirections = new List<Vector2>()
    {
        new Vector2(1 + 0.5f,0),
        new Vector2(0, (1 + 0.5f)),
        new Vector2(- (1 + 0.5f), 0),
        new Vector2(0,- (1 + 0.5f)),
    };

    public static Vector2 GetRandomDirection()
    {
        return randDirections[Random.Range(0, randDirections.Count)];
    }
}