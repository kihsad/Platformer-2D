using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPause : MonoBehaviour
{
    [SerializeField]
    private PauseComponent _menu;
    public void OpenPauseMenue()
    {
        if (_menu.gameObject.activeSelf) return;
        _menu.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
}
