<<<<<<< HEAD
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
        if((player.planet.gameObject.GetComponent("Asteroid")) != null) {step *= 2f;}
        var posWithoutZ = new Vector3(player.planet.position.x, player.planet.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, posWithoutZ, step);
    }
}
=======
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
        if((player.planet.gameObject.GetComponent("Asteroid")) != null) {step *= 2f;}
        var posWithoutZ = new Vector3(player.planet.position.x, player.planet.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, posWithoutZ, step);
    }
}
>>>>>>> 6d46c2557c40c94f3969ff1a471c9573658b3d45
