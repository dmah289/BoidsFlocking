using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] ListBoid listBoid;
    [SerializeField] Boundary boundary;
    [SerializeField] GameObject boidPrefab;
    [SerializeField] int boidCnt = 10;

    private void OnEnable()
    {
        int XLimit = (int)boundary.Limit.x;
        int YLimit = (int)boundary.Limit.y;

        if(listBoid.boids.Count > 0) listBoid.boids.Clear();

        for(int i = 0; i < boidCnt; i++)
        {
            float direction = Random.Range(0, 360);
            Vector2 position = new Vector2(Random.Range(-XLimit, XLimit),
                Random.Range(-YLimit, YLimit));
            GameObject boid = Instantiate(boidPrefab, position, 
                Quaternion.Euler(Vector3.forward * direction) * boidPrefab.transform.localRotation);
            boid.transform.parent = transform;
            listBoid.boids.Add(boid.GetComponent<BoidMovement>());
        }
    }
}
