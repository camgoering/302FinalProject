// This script handles the game over screen

using UnityEngine;
using TMPro;
using Dan.Main; // LeaderboardCreator
using System.Linq;

public class GameOverManager : MonoBehaviour
{
    // UI panel and input field for player name
    public GameObject namePromptPanel;
    public TMP_InputField inputField;

    // Leaderboard API key
    public string leaderboardKey = "YOUR_REAL_KEY_HERE";

    private int finalScore;
    private bool awaitingInput = false;

    // Initialize screen
    void Start()
    {
        finalScore = PlayerPrefs.GetInt("FinalScore", 9999);
        namePromptPanel.SetActive(false);

        CheckIfTopScore();
    }

    // Submits name and score
    void Update()
    {
        if (awaitingInput && Input.GetKeyDown(KeyCode.Return))
        {
            SubmitName();
        }
    }

   // Checks if score is worthy for leaderboard and sorts leaderboard
   void CheckIfTopScore()
{
    finalScore = PlayerPrefs.GetInt("FinalScore", 9999);
    
    LeaderboardCreator.GetLeaderboard(leaderboardKey, (entries) =>
    {
        var sortedEntries = entries.OrderBy(e => e.Score).ToList();
        
        // Check if we have less than 10 entries or if the new score is better than the worst in top 10
        if (sortedEntries.Count < 10 || finalScore < (sortedEntries.Count > 0 ? sortedEntries.Last().Score : int.MaxValue))
        {
            // This is a top score
            namePromptPanel.SetActive(true);
            inputField.text = "";
            inputField.ActivateInputField();
            awaitingInput = true;
        }
    });
}

    // Submits name and score to leaderboard
    void SubmitName()
    {
        string initials = inputField.text.ToUpper().Substring(0, Mathf.Min(3, inputField.text.Length));

        if (!string.IsNullOrWhiteSpace(initials))
        {
            // Reset the player first
            LeaderboardCreator.ResetPlayer(() =>
            {

                    // Then upload the new entry
                    LeaderboardCreator.UploadNewEntry(leaderboardKey, initials, finalScore, (_) =>
                    {
                        Debug.Log("Score submitted!");
                        namePromptPanel.SetActive(false);
                        awaitingInput = false;
                    });
            });
        }
    }

}
