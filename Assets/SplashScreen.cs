using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour {

    bool ShouldFade;
	// Use this for initialization
	void Start ()
    {
        Invoke("FadeOut", 3f);
	}
	
	// Update is called once per frame
	void Update () {
		if(ShouldFade)
        {
            gameObject.GetComponent<CanvasGroup>().alpha -= 0.01f;
        }
        if (gameObject.GetComponent<CanvasGroup>().alpha <= 0) gameObject.SetActive(false);
	}

    void FadeOut()
    {
        ShouldFade = true;
    }

    
}
