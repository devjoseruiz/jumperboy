using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject game;
    private Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (game.GetComponent<GameController>().gameState == GameController.GameState.Playing &&
            (Input.GetKeyDown("space") || Input.GetMouseButtonDown(0)))
        {
            UpdateState("PlayerJump");
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
        if (collider.gameObject.tag == "Enemy")
        {
            game.GetComponent<GameController>().gameState = GameController.GameState.GameOver;
            UpdateState("PlayerDie");
        }
    }
}
