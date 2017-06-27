using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public static UIManager instance;

    public Text timerText, heightText;

    private void Awake()
    {
        instance = this;
    }

    public void SetTimer(int min, int sec) {
        timerText.text = string.Format("{0:D2}:{1:D2}", min, sec);
    }

    public void SetHeight(float height) {
        heightText.text = height+"M";
    }
    	
	// Update is called once per frame
	void Update () {
		
	}
}
