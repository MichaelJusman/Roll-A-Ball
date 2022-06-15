using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float speed = 10f;
    void Update()
    {
        //Rotate our object around an axis over time
        transform.Rotate(new Vector3(12, 12, 12) * Time.deltaTime * speed);
    }
}
