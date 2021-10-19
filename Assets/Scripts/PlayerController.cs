using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject game;
    public AudioClip jumpClip;
    public AudioClip dieClip;
    private Animator playerAnimator;
    private AudioSource audioPlayer;
    private float startY;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        audioPlayer = GetComponent<AudioSource>();
        startY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (game.GetComponent<GameController>().gameState == GameController.GameState.Playing &&
            transform.position.y == startY && (Input.GetKeyDown("space") || Input.GetMouseButtonDown(0)))
        {
            UpdateState("PlayerJump");
            audioPlayer.clip = jumpClip;
            audioPlayer.Play();
        }
        
    }

    public void UpdateState(string state = null)
    {
        if (state != null)
        {
            playerAnimator.Play(state);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log(collider.gameObject.tag);
        if (game.GetComponent<GameController>().gameState != GameController.GameState.GameOver &&
            collider.gameObject.tag == "Enemy")
        {
            game.GetComponent<GameController>().gameState = GameController.GameState.GameOver;
            UpdateState("PlayerDie");
            audioPlayer.clip = dieClip;
            audioPlayer.Play();
        }
    }

    private void GameRestartReady()
    {
        game.GetComponent<GameController>().gameState = GameController.GameState.RestartReady;
    }
}
