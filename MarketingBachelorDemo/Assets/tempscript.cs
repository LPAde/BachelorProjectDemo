using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempscript : MonoBehaviour
{
    void Update()
    {
        transform.RotateAround(transform.position, Vector3.up, 1f);
    }
}
