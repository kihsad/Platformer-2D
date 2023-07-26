using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseComponent : MonoBehaviour
{
    [SerializeField]
    private PauseComponent _menu;

    public void CloseMenu()
    {
        _menu.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
    public void ToStartMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
}
