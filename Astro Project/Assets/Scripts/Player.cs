using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject planet;
    public float movementSpeed = 100.0f;
    public float jumpSpeed = 20.0f;
    public float jumpHeight = 1f;
    public float gravity = 7f;
    private bool falling = true;
    private bool jumping = false;
    private float movement = 0.0f;
    private float timer = 0f;


    void Start() {
    }

    void Update() {
        if(Input.GetKey(KeyCode.LeftArrow)) {
            movement = movementSpeed;
        } else if (Input.GetKey(KeyCode.RightArrow)) {
            movement = -movementSpeed;
        } else {
            movement = 0.0f;
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            if (!jumping) {
                jumping = true;
                timer = jumpHeight / 10f;
                falling = true;
            }
        }

        if (jumping) {
            if (timer > 0f) {
                transform.position += transform.up * Time.deltaTime * jumpSpeed;
                timer = timer - (Time.deltaTime);
            }
        }   
        
        if (falling) {
            transform.position -= transform.up * Time.deltaTime * gravity;
        }

        transform.RotateAround(planet.transform.position, Vector3.forward, movement * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == planet.name) {
            falling = false;
            jumping = false;
        }
    }
}
