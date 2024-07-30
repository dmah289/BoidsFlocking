using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/List Boid")]
public class ListBoid : ScriptableObject
{
    public List<BoidMovement> boids;
}
