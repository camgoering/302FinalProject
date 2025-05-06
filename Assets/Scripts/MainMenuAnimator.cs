//Script for main menu animations and scene loading

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuAnimator : MonoBehaviour
{
    public Animator animator;
    public float transitionDelay = 1.0f; // Adjust based on animation length

    public void PlayGame()
    {
        animator.SetBool("PlayPressed", true);
        StartCoroutine(WaitAndLoadScene());
    }

    private System.Collections.IEnumerator WaitAndLoadScene()
    {
        yield return new WaitForSeconds(transitionDelay);
        SceneManager.LoadScene("Game"); // Use exact scene name
    }
}
