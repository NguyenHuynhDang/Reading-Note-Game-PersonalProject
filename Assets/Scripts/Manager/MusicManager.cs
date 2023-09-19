using System;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource _audioSource;
    private int _baseTempo;
    
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _baseTempo = NoteManager.Instance.GetTempo();
        
        NoteManager.Instance.OnTempoIncrease += NoteManager_OnTempoIncrease;
        GameManager.Instance.OnStateChange += GameManager_OnStateChange;
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;
    }

    private void GameManager_OnGameUnpaused(object sender, EventArgs e)
    {
        _audioSource.UnPause();
    }

    private void GameManager_OnGamePaused(object sender, EventArgs e)
    {
        _audioSource.Pause();
    }

    private void GameManager_OnStateChange(object sender, EventArgs e)
    {
        if (GameManager.Instance.CurrentGameState == GameState.GamePlaying)
        {
            _audioSource.Play();
        }
    }

    private void NoteManager_OnTempoIncrease(object sender, OnTempoIncreaseEventArgs e)
    {
        float baseBps = _baseTempo / 60f;
        float bps = NoteManager.Instance.GetTempo() / 60f;
            
        float pitchAmount = bps / baseBps;
        
        _audioSource.pitch = pitchAmount;
    }
}
