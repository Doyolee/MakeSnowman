using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class MainMenu : MonoBehaviour
{
    public GameObject playUI;
    //public GameObject heroUI;
    //public GameObject shopUI;
    //public GameObject archiveUI;
    [HideInInspector]
    public bool isReady = false;

    GameObject currPanel;
    GameObject changePanel;

    private void Start()
    {
        currPanel = playUI;
    }

    public void ChangePanel(GameObject gameObject)
    {
        changePanel = gameObject;

        if (currPanel != changePanel)
        {
            currPanel.SetActive(false);
            changePanel.SetActive(true);

            currPanel=changePanel;
        }
    }


    

    public void PlayGame()
    {
        if(isReady)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        print("Quit");
        Application.Quit();
    }
}
