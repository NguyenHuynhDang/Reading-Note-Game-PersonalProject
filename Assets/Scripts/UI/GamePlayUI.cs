using System;
using TMPro;
using UnityEngine;

public class GamePlayUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private TextMeshProUGUI currentMultiplierText;
    [SerializeField] private TextMeshProUGUI currentComboText;

    private void Start()
    {
        ScoreManager.Instance.OnValueChange += InstanceOnOnValueChange;
    }

    private void InstanceOnOnValueChange(object sender, EventArgs e)
    {
        currentScoreText.text = ScoreManager.Instance.Score.ToString();
        currentComboText.text = ScoreManager.Instance.ComboNoteHit.ToString();
        currentMultiplierText.text = $"x{ScoreManager.Instance.CurrentScoreMultiplier}";
    }
    
}
