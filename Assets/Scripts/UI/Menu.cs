using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public void OpenPanel(int value)
    {
        gameObject.SetActive(true);
        Time.timeScale = value;
    }

    public void ClosePanel(int value)
    {
        gameObject.SetActive(false);
        Time.timeScale = value;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
