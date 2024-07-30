using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidMovement : MonoBehaviour
{
    [SerializeField] ListBoid listBoid;
    List<BoidMovement> neighbors;

    private Vector2 Velocity;
    private float radius = 2f;
    private float forwardSpeed = 5;
    private float turnSpeed = 10;
    private float cosHalfVisionAngle;

    private void Awake()
    {
       cosHalfVisionAngle = Mathf.Cos(135 * Mathf.Deg2Rad);
    }

    private void FixedUpdate()
    {
        FindNeighbors();
        Velocity = Vector2.Lerp(Velocity, CalculateVelocity(), turnSpeed / 2 * Time.fixedDeltaTime);
        transform.position += (Vector3)Velocity * Time.fixedDeltaTime;
        LookRotation();
    }

    private void FindNeighbors()
    {
        // Tìm hàng xóm để quyết định hành vi dựa trên bán kính và tầm nhìn
        neighbors = listBoid.boids.FindAll(boid => boid != this
            && (transform.position - boid.transform.position).magnitude <= radius
            && InVisionCone(boid.transform.position));
    }

    private bool InVisionCone(Vector2 position)
    {
        // Xét có trong tầm nhìn dựa vào tích vô hướng
        Vector2 directionToNeighbor = (position - (Vector2)transform.position).normalized;
        float dotProduct = Vector2.Dot(transform.forward, directionToNeighbor);
        return dotProduct >= cosHalfVisionAngle;
    }

    private Vector2 CalculateVelocity()
    {
        // Tính vector vận tốc mới dựa trên 3 tập luật
        Vector2 velocity = ((Vector2)transform.forward
            + 2f * Seperation()
            + 0.1f * Alignment()
            + Cohension()).normalized * forwardSpeed;
        return velocity;
    }

    private void LookRotation()
    {
        // Quay nội duy đến vector vận tốc
        transform.rotation = Quaternion.Slerp(transform.localRotation,
            Quaternion.LookRotation(Velocity), turnSpeed * Time.fixedDeltaTime);
    }

    private Vector2 Seperation()
    {
        // Tách các cá thể trong cùng 1 đàn
        Vector2 direction = Vector2.zero;

        foreach(BoidMovement boid in neighbors)
        {
            Vector2 directionToNeighbor = boid.transform.position - transform.position;
            float ratio = Mathf.Clamp01(directionToNeighbor.magnitude / radius);
            direction -= ratio * directionToNeighbor;
        }

        return direction.normalized;
    }

    private Vector2 Alignment()
    {
        // Tính toán vector vận tốc trung bình cả đàn
        Vector2 direction = Vector2.zero;
        foreach (BoidMovement boid in neighbors)
            direction += boid.Velocity;

        if (neighbors.Count > 0)
            direction /= neighbors.Count;
        else
            direction = Velocity;

        return direction.normalized;
    }

    private Vector2 Cohension()
    {
        //Tập trung vào điểm trung tâm của đàn
        Vector2 center = Vector2.zero;
        foreach (BoidMovement boid in neighbors)
            center += (Vector2)transform.position;

        if (neighbors.Count > 0)
            center /= neighbors.Count;
        else 
            center = transform.position;

        return (center - (Vector2)transform.position).normalized;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);

        Gizmos.color = Color.yellow;
        foreach (BoidMovement boid in neighbors)
            Gizmos.DrawLine(transform.position, boid.transform.position);
    }
}
