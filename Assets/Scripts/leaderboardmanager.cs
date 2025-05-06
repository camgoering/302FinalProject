//Shows leaderboard when UI is pressed

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LeaderboardManager : MonoBehaviour
{
    public Animator animator;

    public void ShowLeaderboard()
    {
        animator.SetTrigger("LeaderboardPressed");
    }
}
