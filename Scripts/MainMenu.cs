using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnChippiChappa()
    {
        Globals.videoCount = 0;
        SceneManager.LoadScene("Level_Random");
    }

    public void OnLebronJames()
    {
        Globals.videoCount = 1;
        SceneManager.LoadScene("Level_Random");
    }

    public void OnBabyMaverick()
    {
        Globals.videoCount = 2;
        SceneManager.LoadScene("Level_Random");
    }

    public void OnExit()
    {
        Application.Quit();
    }

    public void OnBack(string s)
    {
        SceneManager.LoadScene(s);
    }
}
