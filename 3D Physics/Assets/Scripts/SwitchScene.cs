using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public void SwitchToUI()
    {
        SceneManager.LoadScene("UI");
    }

    public void SwitchToGame()
    {
        SceneManager.LoadScene("Game");
    }
}
