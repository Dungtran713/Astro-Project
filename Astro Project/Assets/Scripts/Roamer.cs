using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement; 
using UnityEngine;
using System;

public class Roamer : MonoBehaviour
{
    public GameObject planets;
    public GameObject startPlanet;
    public float movementSpeed = 100.0f;
    public float gravity = 7f;
    public int randomCap = 1000;
    public Transform planet;
    private bool falling = true;
    private bool faceLeft = true;
    private float movement = 0.0f;

    void Start() {
        planet = startPlanet.transform;
        movement = movementSpeed;
    }

    void Update() {
        System.Random rand = new System.Random();
        int randOutput = rand.Next(0, randomCap);
        if (randOutput == 0) {
            if(!faceLeft) {
                movement = movementSpeed;
                if (!faceLeft) {
                    flip();
                    faceLeft = true;
                }
            } else {
                movement = -movementSpeed;
                if (faceLeft) {
                    flip();
                    faceLeft = false;
                }
            }
        }
        
        if (falling) {
            transform.position -= transform.up * Time.deltaTime * gravity;
            rotateTowards(planet);
        }

        transform.RotateAround(planet.transform.position, Vector3.forward, movement * Time.deltaTime);

         if ((planet.gameObject.GetComponent("Asteroid")) != null) {
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
        if (collision.gameObject.name == planet.name) {
            falling = false;
        } else {
            rotateTowards(planet);
        }
    }

    void kill() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  
    }

    void rotateTowards(Transform target) {
        var relativePos = target.position - transform.position;
        var angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
        var rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
        transform.rotation = rotation;
    }

    void flip() {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
