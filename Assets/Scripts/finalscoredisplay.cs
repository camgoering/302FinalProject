using UnityEngine;
using TMPro;

public class FinalScoreDisplay : MonoBehaviour
{
    // Reference to the score UI
    [SerializeField] private TextMeshProUGUI scoreText;

    // Changes score text
    void Start()
    {
        int finalScore = PlayerPrefs.GetInt("FinalScore", 9999);
        scoreText.text = finalScore + " ms";
    }
}
