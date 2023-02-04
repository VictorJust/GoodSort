using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour
{
    public float speed = 1f;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    private void Update()
    {
        // Make objects move "down" the screen
        if (gameManager.isGameActive == true)
        {
            transform.Translate(-Vector3.forward * speed * Time.deltaTime);
        }
    }
}
