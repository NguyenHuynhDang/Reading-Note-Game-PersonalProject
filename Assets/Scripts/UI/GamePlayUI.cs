using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private TextMeshProUGUI currentMultiplierText;
    [SerializeField] private TextMeshProUGUI currentComboText;
    [SerializeField] private Button replayButton;

    private void Awake()
    {
        replayButton.onClick.AddListener(() => GameManager.Instance.Replay());
    }

    private void Start()
    {
        ScoreManager.Instance.OnValueChange += ScoreManager_OnValueChange;
    }

    private void ScoreManager_OnValueChange(object sender, EventArgs e)
    {
        currentScoreText.text = ScoreManager.Instance.Score.ToString();
        currentComboText.text = ScoreManager.Instance.ComboNoteHit.ToString();
        currentMultiplierText.text = $"x{ScoreManager.Instance.CurrentScoreMultiplier}";
    }
    
}
