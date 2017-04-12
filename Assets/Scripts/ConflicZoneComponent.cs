using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConflicZoneComponent : MonoBehaviour {

    public GameObject EnemyCharacter;
    private Vector3 AttackPosition;
    public float speed = 10f;
	// Use this for initialization
	void Start () {
       // renderer.enable = false;
       //GetComponent<MeshRenderer>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        AttackPosition = new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y,other.gameObject.transform.position.z);
        EnemyCharacter.transform.Translate(AttackPosition * Time.deltaTime);
        if (other.gameObject.tag == "Hero")
        {
            
            EventManager.Instance.TriggerEvent(new EventCombat(new List<GameObject>(), this.name));
            

        }
       // EnemyCharacter.transform.position = Vector3.MoveTowards(EnemyCharacter.transform.position,AttackPosition, 10 * Time.deltaTime);
       //other.gameObject.transform.position = new Vector3(0,0,0);
    }
}
