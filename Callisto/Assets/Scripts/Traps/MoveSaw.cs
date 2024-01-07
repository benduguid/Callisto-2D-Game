using UnityEngine;

public class MoveSaw : MonoBehaviour
{
    public GameObject[] points; // Array of points
    private int currentPoint = 0; // Store current point

    private float speed = 2f; // Speed of saws

    //====================================================
    // Update is called once per frame
    //====================================================
    private void Update()
    {
        // If the saw has reached the current point, move to the next point in the array
        if (Vector2.Distance(points[currentPoint].transform.position, transform.position) < .1f)
        {
            currentPoint++;

            // If the saw reaches the end of the array, go back to the beginning
            if (currentPoint >= points.Length)
            {
                currentPoint = 0;
            }
        }

        // Move the saw towards the current point
        transform.position = Vector2.MoveTowards(transform.position, points[currentPoint].transform.position, Time.deltaTime * speed);
    }
}
