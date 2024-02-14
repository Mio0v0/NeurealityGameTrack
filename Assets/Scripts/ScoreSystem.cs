using TMPro;
using UnityEngine;
using UnityEngine.UI; // Required for working with UI elements

public class ScoreSystem : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Assign this in the inspector with your UI Text element
    private int score = 0; // Initial score

    // Start is called before the first frame update
    void Start()
    {
        UpdateScoreText(); // Initial score update
    }

    // Method to add points and update the score display
    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();
    }

    // Method to update the score text UI
    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}
