using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed = 2f; // Adjust the speed as needed

    void Update()
    {
        // Moves the camera to the right (positive X direction)
        transform.position += Vector3.right * speed * Time.deltaTime;
    }
}