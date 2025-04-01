using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public float rotationSpeed = 120f; // Degrees per second

    void Update()
    {
        // Rotate around the Z-axis (the one perpendicular to the screen)
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
