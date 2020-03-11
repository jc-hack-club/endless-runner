using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuReturnButton : MonoBehaviour
{
    public void returnMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}