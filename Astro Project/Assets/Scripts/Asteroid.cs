<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float movementSpeed = 2.0f;

    public float firstOffsetx = 0f;
    public float firstOffsety = 10.0f;

    public float lastOffsetx = 0.0f;
    public float lastOffsety = -10.0f;

    public bool orbit = false;
    public float orbitOriginx = 0f;
    public float orbitOriginy = 0f;
    
    public Vector3 toDest;

    private Vector3 firstOffset;
    private Vector3 lastOffset;
    private bool destStatus = false;
    
    void Start() {
        firstOffset = new Vector3(firstOffsetx, firstOffsety, 0);
        firstOffset = firstOffset + transform.position;
        lastOffset = new Vector3(lastOffsetx, lastOffsety, 0);
        lastOffset = lastOffset + transform.position;

    }

    void Update() {
        float step = movementSpeed * Time.deltaTime;
        if (orbit) {
            transform.RotateAround(new Vector2(orbitOriginx, orbitOriginy), Vector3.forward, movementSpeed * Time.deltaTime);
        } else {
            if (!destStatus) {
                toDest = firstOffset;
            } else {
                toDest = lastOffset;
            }
            transform.position = Vector2.MoveTowards(transform.position, toDest , step);
            if (transform.position == toDest) {
                destStatus = !destStatus;
            }
        }
    }
}
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float movementSpeed = 2.0f;

    public float firstOffsetx = 0f;
    public float firstOffsety = 10.0f;

    public float lastOffsetx = 0.0f;
    public float lastOffsety = -10.0f;

    public bool orbit = false;
    public float orbitOriginx = 0f;
    public float orbitOriginy = 0f;
    
    public Vector3 toDest;

    private Vector3 firstOffset;
    private Vector3 lastOffset;
    private bool destStatus = false;
    
    void Start() {
        firstOffset = new Vector3(firstOffsetx, firstOffsety, 0);
        firstOffset = firstOffset + transform.position;
        lastOffset = new Vector3(lastOffsetx, lastOffsety, 0);
        lastOffset = lastOffset + transform.position;

    }

    void Update() {
        float step = movementSpeed * Time.deltaTime;
        if (orbit) {
            transform.RotateAround(new Vector2(orbitOriginx, orbitOriginy), Vector3.forward, movementSpeed * Time.deltaTime);
        } else {
            if (!destStatus) {
                toDest = firstOffset;
            } else {
                toDest = lastOffset;
            }
            transform.position = Vector2.MoveTowards(transform.position, toDest , step);
            if (transform.position == toDest) {
                destStatus = !destStatus;
            }
        }
    }
}
>>>>>>> 6d46c2557c40c94f3969ff1a471c9573658b3d45
