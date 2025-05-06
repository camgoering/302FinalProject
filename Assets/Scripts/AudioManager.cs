//This script manages the games audio

using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    // Can only be set within this class
    public static AudioManager Instance { get; private set; }

    // References Audio Sources for AudioManager on the heirarchy for different sound types
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource carEngineSource; 

    // Sound clips references
    [SerializeField] private AudioClip buttonClickSound;
    [SerializeField] private AudioClip raceStartSound;
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioClip carEngineSound; 


    private void Awake()
    {
        // If another instance exists, duplicate gets destroyed
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Start background music
        StartBackgroundMusic();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check if this is the Game Over scene
        if (scene.name == "GameOver")
        {
            StopCarEngine();
        }
    }

    // Background music 
    private void StartBackgroundMusic()
    {
        if (musicSource != null && backgroundMusic != null && !musicSource.isPlaying)
        {
            musicSource.clip = backgroundMusic;
            musicSource.Play();
        }
    }

    // Car engine
    public void StartCarEngine()
    {
        if (carEngineSource != null && carEngineSound != null && !carEngineSource.isPlaying)
        {
            carEngineSource.clip = carEngineSound;
            carEngineSource.Play();
            Debug.Log("Car engine sound started");
        }
    }

    // Stops car engine sound
    public void StopCarEngine()
    {
        if (carEngineSource != null && carEngineSource.isPlaying)
        {
            carEngineSource.Stop();
            Debug.Log("Car engine sound stopped");
        }
    }

    // Regular sound methods
    public void PlayButtonClick()
    {
        PlaySound(buttonClickSound);
    }

    // Plays race start sound
    public void PlayRaceStart()
    {
        PlaySound(raceStartSound);
    }


    private void PlaySound(AudioClip clip)
    {
        if (sfxSource != null && clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
}