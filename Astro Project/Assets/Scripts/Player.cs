using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject planets;
    public GameObject startPlanet;
    public float movementSpeed = 100.0f;
    public float jumpSpeed = 20.0f;
    public float jumpHeight = 1f;
    public float gravity = 7f;
    public Transform planet;

    private bool falling = true;
    private bool jumping = false;
    private bool faceLeft = true;
    private float movement = 0.0f;
    private float timer = 0f;

    void Start() {
        planet = startPlanet.transform;
    }

    void Update() {
        if(Input.GetKey(KeyCode.LeftArrow)) {
            movement = movementSpeed;
            if (!faceLeft) {
                flip();
                faceLeft = true;
            }
        } else if (Input.GetKey(KeyCode.RightArrow)) {
            movement = -movementSpeed;
            if (faceLeft) {
                flip();
                faceLeft = false;
            }
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
                float startDist = Vector3.Distance (transform.position, planet.transform.position);
                foreach (Transform child in planets.transform) { // Check for closer planets/objects while player is mid-jump and pull them towards closest one
                    float distance = Vector3.Distance (transform.position, child.transform.position);
                    if (distance < startDist && child != planet) {
                        planet = child;
                        jumping = false;
                        falling = true;
                        rotateTowards(planet);
                    }
                }
                
                transform.position += transform.up * Time.deltaTime * jumpSpeed;
                timer = timer - (Time.deltaTime);
            }
        }
        
        if (falling) {
            transform.position -= transform.up * Time.deltaTime * gravity;
        }

        transform.RotateAround(planet.transform.position, Vector3.forward, movement * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.name == planet.name) { // If the player collides back with ground, stop jumping and stop falling
            falling = false;
            jumping = false;
        } else if(jumping) { // If the player collides with another object while jumping, stop jumping and fall back
            jumping = false;
            falling = true;
        }
    }

    // Keeps the player's bottom facing towards an object
    void rotateTowards(Transform target) {
        var relativePos = target.position - transform.position;
        var angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
        var rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
        transform.rotation = rotation;
    }

    // Flips the sprite for use based on direction of travel
    void flip() {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
