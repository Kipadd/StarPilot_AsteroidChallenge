using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthRotation : MonoBehaviour
{
    [SerializeField] private float speedRotation;
    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * speedRotation);
    }
}
