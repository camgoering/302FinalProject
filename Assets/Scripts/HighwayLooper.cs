using UnityEngine;

public class HardcodedHighwayLooper : MonoBehaviour
{
    // Enum used to identify which highway the instance is using
    public enum HighwayType { Highway1, Highway2, Highway3, Highway4 }
    public HighwayType highwayType;

    // Starting positions for highway segments
    private Vector3 highway1Start = new Vector3(-0.5f, -9.11f, 0f);
    private Vector3 highway2Start = new Vector3(15.04f, -5.01f, 0f);
    private Vector3 highway3Start = new Vector3(30.65f, -0.89f, 0f);
    private Vector3 highway4Start = new Vector3(46.19f, 3.21f, 0f);

    // Direction and speed of highway
    private Vector2 scrollDirection = new Vector2(-0.92f, -0.24f);
    public float scrollSpeed = 10f;
    private float resetYThreshold = -13.4f;

    // Initializes starting position for different highways
    void Start()
    {
        switch (highwayType)
        {
            case HighwayType.Highway1:
                transform.position = highway1Start;
                break;
            case HighwayType.Highway2:
                transform.position = highway2Start;
                break;
            case HighwayType.Highway3:
                transform.position = highway3Start;
                break;
            case HighwayType.Highway4:
                transform.position = highway4Start;
                break;
        }
    }

    // Move the highway section and loop
    void Update()
    {
        transform.position += (Vector3)(scrollDirection.normalized * scrollSpeed * Time.deltaTime);

        if (transform.position.y < resetYThreshold)
        {
            transform.position = highway4Start;
        }
    }
}
