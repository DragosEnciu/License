using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitioZone : MonoBehaviour {
    
    public Object scene;
    public EventStartExploration.Position spawnPoint;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hero")
        {
            EventManager.Instance.TriggerEvent(new EventStartExploration(scene.name, spawnPoint));
        }
    }
}
