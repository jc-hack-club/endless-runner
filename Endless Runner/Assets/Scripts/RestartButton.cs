using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
 
{
    public GameObject button;
    // Start is called before the first frame update
    public void restartGame()
    {
        SceneManager.LoadScene("GameLevel");
    }
}
