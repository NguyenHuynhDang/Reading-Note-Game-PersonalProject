using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class NoteManager : MonoBehaviour
{
    // Handle spawning, destroying notes
    public static NoteManager Instance { get; private set; }
    
    public float TimeBetween8Beat { get; private set; }

    public event EventHandler OnTempoIncrease;
    public event EventHandler OnGameOver;

    [SerializeField] private int tempo;

    [SerializeField] private int tempoIncreaseAmount;
    [SerializeField] private int tempoIncreaseThreshold;
    [SerializeField] private int gameOverThreshold;

    private List<MusicalNote> _musicalNoteList;
    
    private int _numberOfNoteHitContinuously;
    private int _numberOfNoteMissedContinuously;
    private float _spawnNoteTimer;
    
    protected NoteManager() {} // protected constructor
    
    private void Awake()
    {
        Instance = this;
        _musicalNoteList = new List<MusicalNote>();

        SetTimeBetween8Beat();
    }

    private void Start()
    {
        MusicalNote.OnNoteHit += MusicalNote_OnNoteHit;
        MusicalNote.OnNoteMissed += MusicalNote_OnNoteMissed;
        
        // InvokeRepeating(nameof(CreateNote), 0, TimeBetween8Beat);
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentGameState != GameState.GamePlaying)
        {
            return;
        }

        SpawnNoteAfter8Beat();
    }

    private void MusicalNote_OnNoteHit(object sender, OnNoteHitEventArgs e)
    {
        _numberOfNoteHitContinuously++;
        _numberOfNoteMissedContinuously = 0;

        // Update Tempo
        if (_numberOfNoteHitContinuously == tempoIncreaseThreshold)
        {
            tempo += tempoIncreaseAmount;
            _numberOfNoteHitContinuously = 0;

            SetTimeBetween8Beat();
            OnTempoIncrease?.Invoke(this, EventArgs.Empty);
        }

        DestroyNote(e.MusicalNote);
    }
    
    private void MusicalNote_OnNoteMissed(object sender, OnNoteMissedEventArgs e)
    {
        _numberOfNoteMissedContinuously++;
        _numberOfNoteHitContinuously = 0;

        if (_numberOfNoteMissedContinuously >= gameOverThreshold)
        {
            // game over
            OnGameOver?.Invoke(this, EventArgs.Empty);
        }
        
        DestroyNote(e.MusicalNote);
    }
    
    private void CreateNote()
    {
        MusicalNote musicalNote = Instantiate(GameAssets.Instance.musicalNotePrefab);
        _musicalNoteList.Add(musicalNote);
    }

    private void SpawnNoteAfter8Beat()
    {
        _spawnNoteTimer -= Time.deltaTime;

        if (_spawnNoteTimer <= 0f)
        {
            _spawnNoteTimer = TimeBetween8Beat;
            CreateNote();
        }
    }
    
    private void DestroyNote(MusicalNote musicalNote)
    {
        for (int i = 0; i < _musicalNoteList.Count; i++)
        {
            if (_musicalNoteList[i] == musicalNote)
            {
                _musicalNoteList.Remove(musicalNote);
                Destroy(musicalNote.gameObject);
                // i--;
                
                break;
            }
        }
    }

    private void SetTimeBetween8Beat()
    {
        TimeBetween8Beat = 480f / tempo;
    }

    public int GetTempo()
    {
        return tempo;
    }
    
}
