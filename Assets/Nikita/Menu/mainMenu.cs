using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{

    public string firstLevel;

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene(firstLevel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {

    }

    public void OpenAbout()
    {

    }

    public void CloseAbout()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quiting");
    }
}
