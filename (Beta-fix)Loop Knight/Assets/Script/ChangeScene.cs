using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    private AudioSource cameraSound;
    public AudioClip clickSound;
    public string URL = "https://github.com/MonPanitan/Loop-Knight_Unity";
    public void GoToScene1()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void loadUrl()
    {
        Application.OpenURL(URL);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void goToMain()
    {
        SceneManager.LoadScene("Play");
    }
}
