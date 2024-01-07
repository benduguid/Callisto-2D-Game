using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSaw : MonoBehaviour
{
    private float rotationSpeed = 2f;

    //====================================================
    // Update is called once per frame
    //====================================================
    void Update()
    {
        // Turn saw around with rotation speed
        transform.Rotate(0, 0, 360 * rotationSpeed * Time.deltaTime);
    }
}
