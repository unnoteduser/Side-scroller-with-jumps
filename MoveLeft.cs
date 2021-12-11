using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed = 25;
    private PlayerController playerControllerScript;
    private float leftBound = -25;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();// set the connection between refernce and real script
    }

    // Update is called once per frame
    void Update()
    {
        if(playerControllerScript.gameOver == false)// gameObject move left until gameOver boolean from PlayerController.cs equals false
        {
             transform.Translate(Vector3.left * Time.deltaTime * speed);
        }

        if(transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))//destroy gameobject tagged as Obstacle when they passed left bound
        {
            Destroy(gameObject);
        }
    }
}
