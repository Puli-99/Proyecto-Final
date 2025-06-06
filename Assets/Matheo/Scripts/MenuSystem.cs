using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSystem : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Street_1");
    }

    public void ExitGame()
    {
        Debug.Log("Saliendo del Juego...");
        Application.Quit();
    }
}
