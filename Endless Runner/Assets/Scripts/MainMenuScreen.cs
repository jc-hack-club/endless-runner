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
}
