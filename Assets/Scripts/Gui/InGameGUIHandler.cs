using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InGameGUIHandler : MonoBehaviour 
{
    private GameObject pauseMenu; 
	// Use this for initialization
	void Start () 
    {
        pauseMenu = transform.FindChild("PauseMenu").gameObject;
		TogglePauseButton();
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void TogglePauseButton()
    {
        GameManager.TogglePause();
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }
}
