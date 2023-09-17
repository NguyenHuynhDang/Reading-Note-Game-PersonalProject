using System;
using UnityEngine;
using UnityEngine.Serialization;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    
    public int Score { get; private set; }
    public int CurrentScoreMultiplier { get; private set; } = DefaultScoreMultiplier;
    public int ComboNoteHit { get; private set; }
    public int MaxCombo { get; private set; }
    public int NumberOfNoteHit { get; private set; }
    public int NumberOfNoteMissed { get; private set; }

    public event EventHandler OnValueChange;
    
    [SerializeField] private int[] multiplierThreshold;
    
    private const int ScorePerNoteHit = 100;
    private const int DefaultScoreMultiplier = 1;
    
    private int _multiplyTracker;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        MusicalNote.OnNoteHit += MusicalNote_OnNoteHit;
        MusicalNote.OnNoteMissed += MusicalNote_OnNoteMissed;
    }

    private void MusicalNote_OnNoteHit(object sender, OnNoteHitEventArgs e)
    {
        Score += ScorePerNoteHit * CurrentScoreMultiplier;
        
        NumberOfNoteHit++;
        ComboNoteHit++;
        
        // Update Multiplier
        if (CurrentScoreMultiplier - 1 < multiplierThreshold.Length)
        {
            _multiplyTracker++;
            if (multiplierThreshold[CurrentScoreMultiplier - 1] <= _multiplyTracker)
            {
                _multiplyTracker = 0;
                CurrentScoreMultiplier++;
            }
        }
        
        OnValueChange?.Invoke(this, EventArgs.Empty);
    }

    private void MusicalNote_OnNoteMissed(object sender, OnNoteMissedEventArgs e)
    {
        CurrentScoreMultiplier = DefaultScoreMultiplier;
        
        NumberOfNoteMissed++;
        _multiplyTracker = 0;
        
        MaxCombo = Math.Max(MaxCombo, ComboNoteHit);
        ComboNoteHit = 0;
        
        OnValueChange?.Invoke(this, EventArgs.Empty);
    }
}
