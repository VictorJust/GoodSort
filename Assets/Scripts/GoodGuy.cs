using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodGuy : MonoBehaviour
{
    public GameObject goodGuy;
    public bool isGoodGuySaved;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();

        isGoodGuySaved = false;
    }

    private void Update()
    {
        if (isGoodGuySaved)
        { 
            Destroy(goodGuy);
            gameManager.UpdateScore(1);
        }

        if (transform.position.z < -6)
        {
            Destroy(goodGuy);
            gameManager.UpdateLives(-1);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isGoodGuySaved = true;
        }
    }
}
