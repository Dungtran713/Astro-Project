using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
	[SerializeField] private float speed = .5f;
	private Vector3 mv;
	
    void Update()
    {        
	mv = transform.position;
	mv.x = (mv.x - speed*5); 
	transform.position = mv;
	
	transform.Rotate(0, 0, 360 * speed * Time.deltaTime);
    }
}
