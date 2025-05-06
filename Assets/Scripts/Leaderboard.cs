using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;
using System.Linq;
using Dan.Models;

public class Leaderboard : MonoBehaviour
{
    // References to UI text
    [SerializeField] private List<TextMeshProUGUI> names;
    [SerializeField] private List<TextMeshProUGUI> scores;

    private string publicLeaderboardKey = "709a4f17469a5792d7b5d7010d6989127283e37fb5b8e4f17a02817cd8ec6506";

    // Gets leaderboard data
    private void Start()
    {
        GetLeaderboard();
    }

  public void GetLeaderboard()
{
    LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, (msg) =>
    {
        // Sort by Score (ascending for reaction times - lower is better)
        var sortedEntries = msg.OrderBy(entry => entry.Score).ToList();
        
        // Take top 10 only
        sortedEntries = sortedEntries.Take(10).ToList();
        int loopLength = Mathf.Min(sortedEntries.Count, names.Count);
        
        // Clear all entries first
        for (int i = 0; i < names.Count; i++)
        {
            names[i].text = "Name";
            scores[i].text = "Score";
        }
        
        // Fill in entries from the leaderboard
        for (int i = 0; i < loopLength; i++)
        {
            names[i].text = sortedEntries[i].Username;
            scores[i].text = sortedEntries[i].Score + " ms";
        }
    });
}

// Submits a new score to the leaderboard
public void SetLeaderboardEntry(string username, int newScore)
{
    string initials = username.Substring(0, Mathf.Min(3, username.Length)).ToUpper();

    // Reset the player's identifier before uploading a new entry
    LeaderboardCreator.ResetPlayer(() =>
    {
            Debug.Log("Player reset successfully");

            // Now upload the new entry
            LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, initials, newScore, (_) =>
            {
                Debug.Log($"Uploaded: {initials} - {newScore} ms");
                GetLeaderboard();
            });
    });
}

}
