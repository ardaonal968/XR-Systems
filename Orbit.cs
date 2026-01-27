using UnityEngine;

public class OrbitMoon : MonoBehaviour
{
    public float rotationSpeed = 30.0f;

    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}