//This script allows game objects to stay between scene changes

using UnityEngine;

public class PersistentObject : MonoBehaviour
{ 
    private static GameObject highwayInstance;

    void Awake()
    {
        //Deletes duplicate highways that persist through scenes
        if (gameObject.name == "highway parent")
        {
            if (highwayInstance != null && highwayInstance != gameObject)
            {
                Destroy(gameObject);
                return;
            }

            else
            {

                highwayInstance = gameObject;
            }
        }

        // Game Objects persist through scenes
        DontDestroyOnLoad(gameObject);
    }
}
