using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public float speed = 0.1f;  // Adjust as needed
    private Renderer quadRenderer;
    private float textureOffsetX = 0f;

    void Start()
    {
        quadRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        // Move the texture offset
        textureOffsetX += speed * Time.deltaTime;

        // Reset offset when it reaches 1 (full cycle)
        if (textureOffsetX >= 1f)
        {
            textureOffsetX = 0f;
        }

        // Apply the offset to the material
        quadRenderer.material.mainTextureOffset = new Vector2(textureOffsetX, 0);
    }
}
