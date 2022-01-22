using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public GameObject ExitCanvas;
    private void Update()
    {
        if(ExitCanvas!= null)
        if (Input.GetKeyDown(KeyCode.Escape))
            ExitCanvas.SetActive(!ExitCanvas.active);
    }
    public void Exit()
    {
        Application.Quit();
        Debug.Log("Exiting game");
    }
}
