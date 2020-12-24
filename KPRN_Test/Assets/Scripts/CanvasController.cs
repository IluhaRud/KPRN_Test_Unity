using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public Canvas mainMenu;
    public Canvas chooseTestMenu;

    public GameObject rockets;

    public void ClickStartButton()
    {
        chooseTestMenu.gameObject.SetActive(true);
        mainMenu.gameObject.SetActive(false);
    }
    public void ClickExitButton()
    {
        Application.Quit();
    }
    public void ClickBackButton()
    {
        chooseTestMenu.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
    }
}
