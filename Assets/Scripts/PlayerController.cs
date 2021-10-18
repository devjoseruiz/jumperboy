using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") || Input.GetMouseButtonDown(0))
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
}
