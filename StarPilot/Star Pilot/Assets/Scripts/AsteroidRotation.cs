using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed; 
    private Vector3 randomRotationDirection;

    void Start()
    {
        randomRotationDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    void Update()
    {
        transform.Rotate(randomRotationDirection * rotationSpeed * Time.deltaTime);
    }
}
