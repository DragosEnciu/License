using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScript : MonoBehaviour {
    public Button startButton;
	// Use this for initialization
	void Start () {
        Button btn = startButton.GetComponent<Button>();
        btn.onClick.AddListener(LoadGame);
	}
	

    void LoadGame()
    {
        Application.LoadLevel("Exploration1");
    }
}
