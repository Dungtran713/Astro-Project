using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement; 
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator animator;

    public GameObject planets;
    public GameObject satellites;
    public GameObject startPlanet;
    public float movementSpeed = 100.0f;
    public float jumpSpeed = 20.0f;
    public float jumpHeight = 1f;
    public float gravity = 7f;
    public Transform planet;
    public float planetAttractiveness = 10f;

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
                float startDist = Vector3.Distance (transform.position, planet.transform.position) + planetAttractiveness;
                float leastDistance = 100000f;
                Transform leastChild = null;
                foreach (Transform child in planets.transform) { // Check for closer planets/objects while player is mid-jump and pull them towards closest one
                    if (child != planet) {
                        float distance = Vector3.Distance (transform.position, child.transform.position);
                        if (distance < leastDistance) {
                            leastDistance = distance;
                            leastChild = child;
                        }
                    }
                }
      
                if (leastDistance < startDist && leastChild != planet) {
                    planet = leastChild;
                    jumping = false;
                    falling = true;
                    rotateTowards(planet);
                }

                transform.position += transform.up * Time.deltaTime * jumpSpeed;
                timer = timer - (Time.deltaTime);
            }
        }
        
        if (falling) {
            transform.position -= transform.up * Time.deltaTime * gravity;
            rotateTowards(planet);
        }

        transform.RotateAround(planet.transform.position, Vector3.forward, movement * Time.deltaTime);

<<<<<<< HEAD
        animator.SetFloat("PlayerMovementSpeed", Mathf.Abs(movement));
        animator.SetBool("PlayerJumping", jumping);

        if ((planet.gameObject.GetComponent("Blackhole")) != null) {
            transform.position = Vector2.MoveTowards(transform.position, planet.transform.position, (movementSpeed * Time.deltaTime) / 10);
            if (transform.position == planet.transform.position) {
                kill();
            }
        } else if ((planet.gameObject.GetComponent("Asteroid")) != null) { // Move player with moving object
=======
        if((planet.gameObject.GetComponent("Asteroid")) != null) { // Move player with moving object
>>>>>>> 6d46c2557c40c94f3969ff1a471c9573658b3d45
            Asteroid asteroid = planet.gameObject.GetComponent<Asteroid>();
            if (asteroid.orbit) {
                transform.RotateAround(new Vector2(asteroid.orbitOriginx, asteroid.orbitOriginy), Vector3.forward, asteroid.movementSpeed * Time.deltaTime);
                transform.RotateAround(planet.transform.position, Vector3.forward, movement * Time.deltaTime);
            } else {
                float step = asteroid.movementSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, asteroid.toDest, step);
                transform.RotateAround(planet.transform.position, Vector3.forward, movement * Time.deltaTime);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.name == planet.name) { // If the player collides back with ground, stop jumping and stop falling
            falling = false;
            jumping = false;
        } else if(jumping) { // If the player collides with another object while jumping, stop jumping and fall back
            jumping = false;
            falling = true;
        } else {
            rotateTowards(planet);
        }
        foreach (Transform child in satellites.transform) {
            if (collision.gameObject.name == child.name) {
                kill();                
            }
        }
        if (collision.gameObject.GetComponent("Roamer") != null) {
            kill();
        }
    }

    void kill() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  
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
