using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI livesText;
    public Button restartButton;
    public GameObject titleScreen;
    public GameObject pause;
    private float spawnRate = 1.0f;
    public int score;
    public bool isGameActive;
    public int lives = 3;


    // Start is called before the first frame update
    void Start()
    {
        pause.SetActive(false);
        lives = 3;
        showlives();

    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);

        }
    }
    public void UpdateScore(int ScoretoAdd)
    {
        score += ScoretoAdd;
        scoreText.text = "Score:" + score;
    }
    public void UpdateLives()
    {

        lives = lives > 0 ? lives - 1 : 0;
        if (lives <= 0)
        {
            GameOver();
        }

        showlives();
    }

    private void showlives()
    {
        livesText.text = "Lives:" + lives;
    }

    public void GameOver()
    {
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
        gameOverText.gameObject.SetActive(true);
    }
    public void RestartGame()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        pause.SetActive(false);

    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void StartGame(int difficulty)
    {
        isGameActive = true;
        StartCoroutine(SpawnTarget());
        score = 0;
        UpdateScore(0);
        titleScreen.gameObject.SetActive(false);
        pause.SetActive(true);
        spawnRate /= difficulty;
    }
}
