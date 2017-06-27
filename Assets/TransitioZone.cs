using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitioZone : MonoBehaviour {
    
    public string scene;
    public EventStartExploration.Position spawnPoint;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hero")
        {
            EventManager.Instance.TriggerEvent(new EventStartExploration(scene, spawnPoint));
        }
    }
}
