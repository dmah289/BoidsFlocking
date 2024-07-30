using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportBoundary : MonoBehaviour
{
    [SerializeField] private Boundary boundary;
    [SerializeField] private float XLimit;
    [SerializeField] private float YLimit;

    private void OnEnable()
    {
        XLimit = boundary.Limit.x;
        YLimit = boundary.Limit.y;
    }

    private void FixedUpdate()
    {
        Vector3 position = transform.position;

        if(Mathf.Abs(transform.position.x) > XLimit)
            position.x = -Mathf.Sign(transform.position.x) * XLimit;
        
        if(Mathf.Abs(transform.position.y) > YLimit)
            position.y = -Mathf.Sign(transform.position.y) * YLimit;

        transform.position = position;
    }
}
