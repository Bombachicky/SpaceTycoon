using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    //singleton
    public static gameManager instance;

    //Menus
    [SerializeField] public GameObject menuCurrentlyOpen;
    [SerializeField] public GameObject pauseMenu;
    [SerializeField] public GameObject winMenu;
    [SerializeField] public GameObject loseMenu;
    [SerializeField] public GameObject buildMenu;
    [SerializeField] public Camera cam;


    public bool isPaused;
    float timeScaleOrig;


    // Start is called before the first frame update
    void Awake()
    {
        //singleton assignment
        instance = this;

        // confines cursor to screen but moveable
        Cursor.lockState = CursorLockMode.Confined;

        timeScaleOrig = Time.timeScale;
    }

    // Update is called once per frame
    void Update()
    {
        // if escape button pressed, pause game
        if (Input.GetButtonDown("Cancel"))
        {
            menuToggle(pauseMenu);
        }

        if (Input.GetButtonDown("Fire2"))
        {
            Cursor.visible = false;
        }

        if (Input.GetButtonUp("Fire2"))
        {
            Cursor.visible = true;
            
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            menuToggle(buildMenu);
        }
    }

    public void menuToggle(GameObject menu)
    {
        //this is a toggle
        isPaused = !isPaused;

        menuCurrentlyOpen = menu;

        menuCurrentlyOpen.SetActive(isPaused);

        if (isPaused)
        {
            cursorLockPause();
        }
        else
        {
            cursorUnlockUnpause();
        }
    }

    public void cursorLockPause()
    {
        // Makes the whole system pause outside of UI update
        Time.timeScale = 0;
    }

    /// <summary>
    /// Hide cursor and lock it to center of screen. Resume all game activity. 
    /// </summary>
    public void cursorUnlockUnpause()
    {
        // Makes the whole system resume
        Time.timeScale = timeScaleOrig;

        if (menuCurrentlyOpen != null)
        {
            // Turn off pause menu
            menuCurrentlyOpen.SetActive(false);
        }

        //clear current menu out to be reused
        menuCurrentlyOpen = null;
    }    
}
