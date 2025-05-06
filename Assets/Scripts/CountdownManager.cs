using UnityEngine;
using System.Collections;

public class CountdownManager : MonoBehaviour
{
    public Animator animator;

   

    void Start()
    {
        animator = GetComponent<Animator>();

        // Plays the countdown animation
        if (animator != null)
        {
            animator.Play("321clip");
        }
        else
        {
            Debug.LogWarning("CountdownManager: Animator not found on this GameObject.");
        }

        // Plays the race start audio
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayRaceStart();
        }

    }

}
