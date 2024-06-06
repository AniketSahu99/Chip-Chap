using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLevelAlgo
{
    public Vector2 startPos;

    public static HashSet<Vector2> SimpleRandomWalk(Vector2 startPos, int walkLength)
    {
        HashSet<Vector2> path = new HashSet<Vector2>();

        path.Add(startPos);
        var prevPos = startPos;

        for(int i = 0; i < walkLength; i++)
        {
            var newPos = prevPos + Direction2D.GetRandomDirection();
            path.Add(newPos);
            prevPos = newPos;
        }
        return path;
    }

}
