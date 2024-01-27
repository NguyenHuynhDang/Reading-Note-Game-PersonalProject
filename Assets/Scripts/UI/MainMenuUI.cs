using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button howtoplayButton;
    [SerializeField] private Button quitButton;

    public static event EventHandler OnHowtoplayButtonPressed;
    private void Awake()
    {
        ResetStaticData();
        
        playButton.onClick.AddListener(() => Loader.Load(Scene.GamePracticeScene));
        howtoplayButton.onClick.AddListener(() => OnHowtoplayButtonPressed?.Invoke(this, EventArgs.Empty));
        quitButton.onClick.AddListener(Application.Quit);

        Time.timeScale = 1f;
    }

    private void ResetStaticData()
    {
        OnHowtoplayButtonPressed = null;
    }
}
