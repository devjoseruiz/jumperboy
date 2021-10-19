using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public float parallaxSpeed = 0.02f;
    public RawImage background;
    public RawImage platform;
    public GameObject uiIdle;

    public enum GameState {Idle, Playing, GameOver};
    public GameState gameState = GameState.Idle;

    public GameObject player;
    public GameObject enemyGenerator;

    // Update is called once per frame
    void Update()
    {
        // Game not started
        if (gameState == GameState.Idle)
        {
            if (Input.GetKeyDown("space") || Input.GetMouseButtonDown(0))
            {
                // Change game state
                gameState = GameState.Playing;
                // Hide idle screen
                uiIdle.SetActive(false);
                // Change player animation
                player.SendMessage("UpdateState", "PlayerRun");
                enemyGenerator.SendMessage("StartGenerator");
            }
        }

        // Game started
        else if (gameState == GameState.Playing)
        {
            float finalSpeed = parallaxSpeed * Time.deltaTime;
            background.uvRect = new Rect(background.uvRect.x + finalSpeed, 0f, 1f, 1f);
            platform.uvRect = new Rect(platform.uvRect.x + finalSpeed * 4, 0f, 1f, 1f);
        }

        // Game over
        else
        {
            enemyGenerator.SendMessage("StopGenerator");
        }
    }
}
