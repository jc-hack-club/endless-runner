using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenuScreen : MonoBehaviour
{
    public void playButtonClicked()
    {
        SceneManager.LoadScene("GameLevel");
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F4))
        {
            if (PlayerPrefs.HasKey("Highscore"))
            {
                PlayerPrefs.DeleteKey("Highscore");
            }
        }
    }
}