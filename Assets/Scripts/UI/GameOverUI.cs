using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI numberOfNoteHitText;
    [SerializeField] private TextMeshProUGUI numberOfMissedText;
    [SerializeField] private TextMeshProUGUI maxComboText;
    [SerializeField] private TextMeshProUGUI totalScoreText;
    
    [SerializeField] private Button replayButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        replayButton.onClick.AddListener(() => GameManager.Instance.Replay());
        mainMenuButton.onClick.AddListener(() => Loader.Load(Scene.MainMenuScene));
        quitButton.onClick.AddListener(Application.Quit);
    }

    private void Start()
    {
        GameManager.Instance.OnStateChange += GameManager_OnStateChange;
        
        Hide();
    }

    private void GameManager_OnStateChange(object sender, EventArgs e)
    {
        if (GameManager.Instance.CurrentGameState != GameState.GameOver)
        {
            return;
        }
        
        numberOfNoteHitText.text = ScoreManager.Instance.NumberOfNoteHit.ToString();
        numberOfMissedText.text = ScoreManager.Instance.NumberOfNoteMissed.ToString();
        maxComboText.text = ScoreManager.Instance.MaxCombo.ToString();
        totalScoreText.text = ScoreManager.Instance.Score.ToString();
        
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
