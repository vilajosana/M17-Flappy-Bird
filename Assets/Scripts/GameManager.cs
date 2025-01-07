using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour 
{
    public bool isGameOver;
    public bool isGameStarted;

    private int score;
    private int bestScore;

    [SerializeField] private GameObject gameOverText;
    [SerializeField] private GameObject restartText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text currentScoreText; // Diálogo de Current Score
    [SerializeField] private TMP_Text bestScoreText;    // Diálogo de Best Score
    [SerializeField] private GameObject scoreDialog;    // Panel para mostrar scores
    [SerializeField] private GameObject startText;      // Texto inicial para iniciar partida
    
    // Audio sources for different game sounds
    [SerializeField] private AudioSource scoreSoundEffect;
    [SerializeField] private AudioSource collisionSoundEffect;

    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        isGameOver = false;
        isGameStarted = false;
        Time.timeScale = 0; // Pausa el juego al inicio
        startText.SetActive(true);
        scoreDialog.SetActive(false);

        // Cargar Best Score
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
    }

    void Update()
    {
        // Iniciar el juego al presionar pantalla o tecla X
        if (!isGameStarted && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.X)))
        {
            StartGame();
        }

        // Reiniciar el juego si está en Game Over y se hace tap
        if (isGameOver && Input.GetMouseButtonDown(0))
        {
            RestartGame();
        }
    }

    private void StartGame()
    {
        isGameStarted = true;
        Time.timeScale = 1;

        startText.SetActive(false);
    }

    public void GameOver()
    {
        if (isGameOver) return; // Evitar múltiples ejecuciones de GameOver
        isGameOver = true;

        // Play collision sound when game over
        if (collisionSoundEffect != null)
        {
            collisionSoundEffect.Play();
        }

        gameOverText.SetActive(true);
        restartText.SetActive(true);

        UpdateBestScore();
        ShowScoreDialog();

        Time.timeScale = 0; // Pausa el juego al finalizar
    }

    private void UpdateBestScore()
    {
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", bestScore);
            PlayerPrefs.Save();
        }
    }

    private void ShowScoreDialog()
    {
        scoreDialog.SetActive(true);
        currentScoreText.text = "Score: " + score;
        bestScoreText.text = "Best Score: " + bestScore;
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
        
        // Play score sound effect if assigned
        if (scoreSoundEffect != null)
        {
            scoreSoundEffect.Play();
        }
    }

    

    // Method to play collision sound (can be called from PlayerController)
    public void PlayCollisionSound()
    {
        if (collisionSoundEffect != null)
        {
            collisionSoundEffect.Play();
        }
    }
}