using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Player player;
    public float cameraSpeed = 1.5f;

    void Start() {
    }

    void Update() {
        var step =  cameraSpeed * Time.deltaTime;
        var posWithoutZ = new Vector3(player.planet.position.x, player.planet.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, posWithoutZ, step);
    }
}
