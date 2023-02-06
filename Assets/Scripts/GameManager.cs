using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public int lives = 3;
    public int score = 0;

    private float fixedDeltaTime;
    private float spawnRate = 1;

    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI gameOverText;

    public GameObject titleScreen;
    public GameObject pauseScreen;

    private SpawnManager spawnManager;
    
    public bool isPaused;
    public bool isGameActive;

    //Fields for display the player info
    public Text currentPlayerName;
    public Text bestPlayerNameAndScore;

    //Static variables for holding the best player data
    private static int _bestScore;
    private static string _bestPlayer;

    public void Awake()
    {
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();

        this.fixedDeltaTime = Time.fixedDeltaTime;

        UpdateLives(0);
        UpdateScore(0);

        LoadGameRankScript.LoadGameRank();
    }

    private void Start()
    {
        currentPlayerName.text = PlayerDataHandle.Instance.playerName;

        SetBestPlayer();
    }

    void Update()
    {
        //Check if the user has pressed the Space key
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangePaused();
        }

        Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
    }

    IEnumerator SpawnTarget() 
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            spawnManager.gameObject.SetActive(true);
        }
    }

    public void UpdateLives(int livesToDecrease)
    {
        lives += livesToDecrease;
        if (lives == 0)
        {
            GameOver();
        }
        livesText.SetText($"Lives: {lives}");
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.SetText($"Score: {score}");

        PlayerDataHandle.Instance.score = score;
    }

    public void GameOver()
    {
        isGameActive = false;
        CheckBestPlayer();
        gameOverText.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        isGameActive = true;

        score = 0;
        lives = 3;

        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        UpdateLives(0);

        titleScreen.gameObject.SetActive(false);
    }

    public void ChangePaused()
    {
        // Activate / deactivate pause panel
        if (!isPaused)
        {
            isPaused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else if (isPaused)
        {
            isPaused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void CheckBestPlayer()
    {
        int CurrentScore = PlayerDataHandle.Instance.score;

        if (CurrentScore > _bestScore)
        {
            _bestPlayer = PlayerDataHandle.Instance.playerName;
            _bestScore = CurrentScore;

            LoadGameRankScript.SaveGameRank(_bestPlayer, _bestScore);
        }

        bestPlayerNameAndScore.text = $"Best Score - {_bestPlayer}: {_bestScore}";
    }

    private void SetBestPlayer()
    {
        if (_bestPlayer == null && _bestScore == 0)
        {
            bestPlayerNameAndScore.text = "";
        }
        else
        {
            bestPlayerNameAndScore.text = $"Best Score - {_bestPlayer}: {_bestScore}";
        }
    }
}
