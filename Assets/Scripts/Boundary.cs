using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Boundary")]
public class Boundary : ScriptableObject
{
    private Vector2 limit;

    public Vector2 Limit
    {
        get
        {
            CalculateLimit();
            return limit;
        }
    }

    private void CalculateLimit()
    {
        limit.y = Camera.main.orthographicSize + 1;
        limit.x = limit.y * Screen.width / Screen.height + 1;
    }
}
