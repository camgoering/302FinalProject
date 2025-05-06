//This script handles the scene changes and menu navigation

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Linq;

public class MainMenuManager : MonoBehaviour
{
    public Animator animator;
    public float delayBeforeSceneLoad = 1f;


    private void LogCurrentState(string label)
    {
        var state = animator.GetCurrentAnimatorStateInfo(0);
        Debug.Log($"🧩 [{label}] State: {state.fullPathHash} NameHash: {state.shortNameHash} NormalizedTime: {state.normalizedTime:F2} IsName: {state.IsName("GameOver")}");
    }

    // Transitions to game scene after play button is pressed
    public void PlayGame()
    {
        Debug.Log("▶️ Play Button Pressed");

        if (!ValidateAnimator("PlayPressed")) return;

        LogCurrentState("Before Play Trigger");

        animator.SetTrigger("PlayPressed");
        Debug.Log("✅ Trigger Set: PlayPressed");

        StartCoroutine(ConfirmTransition("Game"));
    }

    // Quits game
    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }

    // Transitions to leaderboard scene when pressed
    public void GoToLeaderboard()
    {
        Debug.Log("▶️ Leaderboard Button Pressed");

        if (!ValidateAnimator("LeaderboardPressed")) return;

        LogCurrentState("Before Leaderboard Trigger");

        animator.SetTrigger("LeaderboardPressed");
        Debug.Log("✅ Trigger Set: LeaderboardPressed");

        StartCoroutine(ConfirmTransition("Leaderboard"));
    }

    // Transitions to main menu when pressed
    public void MainMenu()
    {
        Debug.Log("▶️ Main Menu Button Pressed");

        if (!ValidateAnimator("MainMenuPressed")) return;

        LogCurrentState("Before MainMenu Trigger");

        animator.SetTrigger("MainMenuPressed");
        Debug.Log("✅ Trigger Set: MainMenuPressed");

        StartCoroutine(ConfirmTransition("MainMenu"));
    }

    // Waits for animation to change before loading next scene
    private IEnumerator ConfirmTransition(string sceneName)
    {

        AnimatorStateInfo initialState = animator.GetCurrentAnimatorStateInfo(0);
        int originalStateHash = initialState.fullPathHash;

        Debug.Log($"⏳ Waiting for state change from {originalStateHash}");

        // Timers
        float timer = 0f;
        float timeout = 2f;

        // Wait until animator moves to a different state
        while (animator.GetCurrentAnimatorStateInfo(0).fullPathHash == originalStateHash &&
               !animator.IsInTransition(0))
        {
            timer += Time.deltaTime;

            // Failure of animation to change
            if (timer > timeout)
            {
                Debug.LogWarning("⚠️ Animation state did not change within timeout.");
                break;
            }

            yield return null;
        }

        // Logs state for debugging
        if (animator.IsInTransition(0))
        {
            Debug.Log("🔄 Animator is transitioning...");
        }


        var newState = animator.GetCurrentAnimatorStateInfo(0);
        Debug.Log($"✅ Transition complete. New State Hash: {newState.fullPathHash}");

        //yield return new WaitForSeconds(delayBeforeSceneLoad);

        Debug.Log("🚀 Loading scene: " + sceneName);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    // Helper function to validate the animator set up
    private bool ValidateAnimator(string triggerName)
    {
        if (animator == null)
        {
            Debug.LogError("❌ Animator is NOT assigned.");
            return false;
        }

        if (!animator.gameObject.activeInHierarchy)
        {
            Debug.LogError("❌ Animator GameObject is NOT active in hierarchy.");
            return false;
        }

        // Check if the required triggers exist
        bool hasTrigger = animator.parameters.Any(p => p.name == triggerName && p.type == AnimatorControllerParameterType.Trigger);
        Debug.Log($"✅ Animator contains trigger \"{triggerName}\"? {hasTrigger}");

        if (!hasTrigger)
        {
            Debug.LogError($"❌ Animator is missing the trigger: {triggerName}");
            return false;
        }

        return true;
    }
}
