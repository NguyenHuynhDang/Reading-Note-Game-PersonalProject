using System;
using UnityEngine;

public class HowtoplayUI : MonoBehaviour
{
    private void Start()
    {
        MainMenuUI.OnHowtoplayButtonPressed += MainMenuUI_OnHowtoplayButtonPressed;
        Hide();
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            Hide();
        }
    }

    private void MainMenuUI_OnHowtoplayButtonPressed(object sender, EventArgs e)
    {
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
