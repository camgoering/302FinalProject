//This script manages the reaction time gameplay
//Controls prompts and measures players reaction times
//Increments road speed on shift aswell

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ReactionManager : MonoBehaviour
{
    // UI pop up
    public Image shiftPopup;

    // Sets max shifts, max/min delay before popup 
    public int totalShifts = 5;
    public float minDelay = 1f;
    public float maxDelay = 3f;

    // Reference in order to change car's size
    public Transform mr2Transform;
    public Transform e30Transform;

    // Car animators for drive up effect
    public Animator mr2Animator;
    public Animator e30Animator;

    public HardcodedHighwayLooper[] highwayLoopers;  // Reference to road scripts
    public float baseRoadSpeed = 10f;  // Base road speed 
    public float roadSpeedIncrement = 1.4f;  // How much to increase road speed with each shift

    //Stores reaction times
    private List<float> reactionTimes = new List<float>();
    private float reactionStartTime;

    // Variable to check if user can press space bar
    private bool waitingForInput = false;
   

    // Track current shift for increasing forward movement
    private int currentShiftCount = 0;

    // Initial game state
    void Start()
    {
        shiftPopup.gameObject.SetActive(false);

        // Find all highway loopers if not assigned
        if (highwayLoopers == null || highwayLoopers.Length == 0)
        {
            highwayLoopers = FindObjectsByType<HardcodedHighwayLooper>(FindObjectsSortMode.None);
        }

        // Reset the road speed to base speed
        ResetRoadSpeed();

        StartCoroutine(InitialDriveIn());
    }

    // Resets road speed
    void ResetRoadSpeed()
    {
        foreach (var looper in highwayLoopers)
        {
            if (looper != null)
            {
                looper.scrollSpeed = baseRoadSpeed;
            }
        }
    }

    // Check for player input
    void Update()
    {
        if (waitingForInput && Input.GetKeyDown(KeyCode.Space))
        {
            // Calculates reaction time in milliseconds
            float reactionTime = (Time.time - reactionStartTime) * 1000f;

            reactionTimes.Add(reactionTime);
            Debug.Log("Reaction Time: " + reactionTime + " ms");

            // Increment shift count for acceleration effect
            currentShiftCount++;


            // Increase road speed
            IncreaseRoadSpeed();

            // Scaling effect of the car
            StartCoroutine(Scaling(mr2Transform));
            StartCoroutine(Scaling(e30Transform));

            // Plays engine sound on shift
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.StartCarEngine();
            }

            // Gets rid of popup 
            shiftPopup.gameObject.SetActive(false);
            waitingForInput = false;
        }
    }

    // Increases road speed
    void IncreaseRoadSpeed()
    {
        // New road speed based on shift count
        float newRoadSpeed = baseRoadSpeed + (currentShiftCount * roadSpeedIncrement);

        // Apply to all highway loopers
        foreach (var looper in highwayLoopers)
        {
            if (looper != null)
            {
                looper.scrollSpeed = newRoadSpeed;
            }
        }

        Debug.Log($"Road speed increased to {newRoadSpeed}");
    }

    // Visual effect that makes car pop on shift
    private IEnumerator Scaling(Transform carTransform)
    {
        // Save original scale
        Vector3 originalScale = carTransform.localScale;

        // Scale up slightly
        carTransform.localScale = originalScale * 1.05f;

        yield return new WaitForSeconds(0.1f);

        // Scale back to normal
        carTransform.localScale = originalScale;
    }

    IEnumerator InitialDriveIn()
    {
        // Play drive-in animations
        mr2Animator.SetTrigger("DriveIn");
        e30Animator.SetTrigger("DriveIn");

        yield return new WaitForSeconds(1.2f); // match animation length
        StartCoroutine(ShiftSequence());
    }

    // Controls time between shifts and pop up
    IEnumerator ShiftSequence()
    {
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < totalShifts; i++)
        {
            // Random delay between the range
            float delay = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(delay);

            // Enables pop up and starts clock
            shiftPopup.gameObject.SetActive(true);
            reactionStartTime = Time.time;
            waitingForInput = true;

            // Waits to continue until user presses spacebar
            yield return new WaitUntil(() => waitingForInput == false);
        }

        //Adds up total reaction time
        float totalTime = 0f;
        foreach (float rt in reactionTimes)
            totalTime += rt;

        // Rounds milliseconds
        int roundedMilliseconds = Mathf.RoundToInt(totalTime);
        roundedMilliseconds = Mathf.Min(roundedMilliseconds, 99999);

        // Saves score
        PlayerPrefs.SetInt("FinalScore", roundedMilliseconds);
        PlayerPrefs.Save();
        SceneManager.LoadScene("GameOver");
    }

    // Cleans up the scene when it changes
    void OnDestroy()
    {
        // Reset road speeds before destroying
        ResetRoadSpeed();
    }
}