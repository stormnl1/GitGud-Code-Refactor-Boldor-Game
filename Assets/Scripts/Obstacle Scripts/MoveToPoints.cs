using UnityEngine;

public class MoveToPoints : MonoBehaviour
{
    [SerializeField] private Transform[] points; // Array of points to move between
    private int currentPoint = 0; // Index refers to the current point in the array
    private int pointIndex = 1; // Point index to determine next point in array, 1 for forward, -1 for backward
    private float speed = 2f;
    
    private void Update()
    {
        MoveToPoint();
    }
    private void MoveToPoint()
    {
        if (points.Length == 0) return; // Check if there are points to move to otherwise return

        transform.position = Vector3.MoveTowards(transform.position, points[currentPoint].position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, points[currentPoint].position) < 0.01f)
        {
            currentPoint += pointIndex;
        }

        if (currentPoint >= points.Length)
        {
            pointIndex = -1;
            currentPoint += pointIndex;
        }

        if (currentPoint < 0)
        {
            pointIndex = 1;
            currentPoint += pointIndex;
        }
    }
}