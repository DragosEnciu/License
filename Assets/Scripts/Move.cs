using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {


	public GameObject character;
	Vector3 newPosition;
	public float speed;


	void Start()
	{			
		newPosition = this.transform.position;
	}

	void Update () {

		if (Input.GetMouseButtonDown(0) == true)
        {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Grass tag was added to the plane 
			if (Physics.Raycast (ray, out hit) && hit.transform.tag == "Grass")
            {
				//sending the position given by the raycast 
				newPosition = hit.point;
                newPosition.y = transform.position.y;
				//triyng to move the box toward the point
				Quaternion transRot = Quaternion.LookRotation(newPosition - this.transform.position, Vector3.up );
			}
        }
        if ((transform.position - newPosition).magnitude >= speed * Time.deltaTime)
            transform.position = Vector3.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);

    }



}
