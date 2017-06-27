using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Energy : MonoBehaviour {
    public Image Energybar;
	// Use this for initialization
	// Update is called once per frame
	void Update () {
        Energybar.fillAmount = this.GetComponent<Base>().get_ActionPoints / Base.MAXACTIONPOINTS;
	}
}
