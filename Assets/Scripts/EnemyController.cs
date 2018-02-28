using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController {
    private static List<Vector2Int> taken = new List<Vector2Int>();  // The already occupied spots

    // Return a new random vector not taken given a range
    public static Vector2Int GetRandomVector (int min, int max)
    {
        Vector2Int result = new Vector2Int(Random.Range(min, max), Random.Range(min, max));
        for (; taken.Contains(result); result = new Vector2Int(Random.Range(min, max), Random.Range(min, max)));
        taken.Add(result);
        return result;
    }
}
