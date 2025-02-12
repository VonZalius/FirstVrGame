using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public static ScoreScript Instance { get; private set; }
    public int Score { get; private set; } = 0;
    public Text scoreText; // Référence au texte UI

    private void Awake()
    {
        // Singleton pour s'assurer qu'il n'y a qu'une seule instance de ScoreScript
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateScoreUI(); // Met à jour le texte dès le début
    }

    public void AddScore(int amount)
    {
        Score += amount;
        UpdateScoreUI(); // Met à jour l'affichage du score
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + Score;
        }
    }
}


