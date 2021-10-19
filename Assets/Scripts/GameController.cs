using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public float parallaxSpeed = 0.02f;
    public RawImage background;
    public RawImage platform;
    public GameObject uiIdle;

    public enum GameState {Idle, Playing, GameOver, RestartReady};
    public GameState gameState = GameState.Idle;

    public GameObject player;
    public GameObject enemyGenerator;

    public float scaleTime = 10.0f;
    public float scaleTimeIncrement = 0.10f;

    private AudioSource musicPlayer;

    void Start()
    {
        musicPlayer = GetComponent<AudioSource>();
        InvokeRepeating("GameDifficultyIncrement", scaleTime, scaleTime);
    }

    // Update is called once per frame
    void Update()
    {
        bool keyDown = Input.GetKeyDown("space") || Input.GetMouseButtonDown(0);
        // Game not started
        if (gameState == GameState.Idle)
        {
            if (keyDown)
            {
                // Change game state
                gameState = GameState.Playing;
                // Hide idle screen
                uiIdle.SetActive(false);
                // Change player animation
                player.SendMessage("UpdateState", "PlayerRun");
                enemyGenerator.SendMessage("StartGenerator");
                musicPlayer.Play();
            }
        }

        // Game started
        else if (gameState == GameState.Playing)
        {
            float finalSpeed = parallaxSpeed * Time.deltaTime;
            background.uvRect = new Rect(background.uvRect.x + finalSpeed, 0f, 1f, 1f);
            platform.uvRect = new Rect(platform.uvRect.x + finalSpeed * 4, 0f, 1f, 1f);
            // Increment game difficulty
            
        }

        // Game over
        else if (gameState == GameState.GameOver)
        {
            enemyGenerator.SendMessage("StopGenerator");
            musicPlayer.Stop();
        }

        // Ready for restart
        else
        {
            if (keyDown)
            {
                ResetGameDifficulty();
                SceneManager.LoadScene("MainScene");
            }
        }
    }

    void GameDifficultyIncrement()
    {
        Time.timeScale += scaleTimeIncrement;
    }

    void ResetGameDifficulty()
    {
        CancelInvoke("GameDifficultyIncrement");
        Time.timeScale = 1.0f;
    }
}
