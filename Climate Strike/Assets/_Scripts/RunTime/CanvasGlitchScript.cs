using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasGlitchScript : MonoBehaviour
{
    private OmnisceneScript dontDestroy;
    private Canvas pauseCanvas;
    private GameObject glitch;
    private Canvas combatCanvas;

    // Start is called before the first frame update
    void Start()
    {
        dontDestroy = GameObject.FindObjectOfType<OmnisceneScript>();
        combatCanvas = GetComponent<Canvas>();
        glitch = GameObject.Find("TempCanvas");
        pauseCanvas = glitch.GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dontDestroy.pause)
        {
            combatCanvas.sortingOrder = 1;
            pauseCanvas.sortingOrder = 2;
        }
        else
        {
            combatCanvas.sortingOrder = 2;
            pauseCanvas.sortingOrder = 1;
        }
    }
}
