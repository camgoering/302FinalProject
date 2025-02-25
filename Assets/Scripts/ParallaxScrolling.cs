using UnityEngine;

public class ParallaxScrolling : MonoBehaviour
{
    public float parallaxSpeed = 0.5f;  
    private Transform cam;
    private Vector3 previousCamPos;
    private float textureWidth;

    void Start()
    {
        cam = Camera.main.transform;
        previousCamPos = cam.position;
        
        // Ensure the correct width of the sprite is used
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        textureWidth = spriteRenderer.bounds.size.x;
    }

    void Update()
    {
        Vector3 deltaMovement = cam.position - previousCamPos;
        transform.position += new Vector3(deltaMovement.x * parallaxSpeed, 0, 0);
        previousCamPos = cam.position;

        // If the camera moves far enough, reposition the background
        if (cam.position.x >= transform.position.x + textureWidth)
        {
            RepositionBackground();
        }
    }

    void RepositionBackground()
    {
        transform.position += new Vector3(textureWidth * 2f, 0, 0);
    }
}
