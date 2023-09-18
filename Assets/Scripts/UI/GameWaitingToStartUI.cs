using System;
using UnityEngine;

public class GameWaitingToStartUI : MonoBehaviour
{
    private void Start()
    {
        Show();
        GameManager.Instance.OnStateChange += InstanceOnOnStateChange;
    }

    private void InstanceOnOnStateChange(object sender, EventArgs e)
    {
        if (GameManager.Instance.CurrentGameState != GameState.WaitingToStart)
        {
            Hide();
        }
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
