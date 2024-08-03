using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public TimeManager TimeManager;

    // Use this for initialization
    void Start()
    {
        TimeManager.Realtime();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Highscore()
    {
        Debug.Log("Highscore");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
