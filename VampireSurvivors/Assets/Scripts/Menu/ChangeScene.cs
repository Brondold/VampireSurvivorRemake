using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public GameObject Menu;
    public GameObject MainMenu;
    public GameObject MenuOptions;
    public GameObject MenuCredits;
    public GameObject MenuCommands;
    
    public void MoveToScene(int sceneID)
    {
        SceneManager.LoadScene("essential", LoadSceneMode.Single);
        SceneManager.LoadScene(sceneID, LoadSceneMode.Additive);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void ButtonClicked(string _String)
    {

        if (_String == "Credits Button")
        {
            MainMenu.SetActive(false);
            MenuCredits.SetActive(true);
        }
        if (_String == "Return From Credits Button")
        {
            MainMenu.SetActive(true);
            MenuCredits.SetActive(false);
        }
        if (_String == "Options Button")
        {
            MainMenu.SetActive(false);
            MenuOptions.SetActive(true);
        }
        if (_String == "Return From Options Button")
        {
            MainMenu.SetActive(true);
            MenuOptions.SetActive(false);
        }
        if (_String == "Commands Button")
        {
            MenuOptions.SetActive(false);
            MenuCommands.SetActive(true);
        }
        if (_String == "Return From Commands Button")
        {
            MenuOptions.SetActive(true);
            MenuCommands.SetActive(false);
        }

    }
}
