using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance { get; private set; }

   public GameState CurrentGameState { get; private set; }
   public float CountDownToStartTimer { get; private set; } = 3f;
   public event EventHandler OnStateChange;
   public event EventHandler OnGamePaused;
   public event EventHandler OnGameUnpaused;

   private float _waitingToStartTimer = 1f;
   private bool _isGamePaused;

   private void Awake()
   {
      Instance = this;
      CurrentGameState = GameState.WaitingToStart;
   }

   private void Start()
   {
      NoteManager.Instance.OnGameOver += NoteManager_OnGameOver;
      GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
   }
   
   private void Update()
   {
      switch (CurrentGameState)
      {
         case GameState.WaitingToStart:
            _waitingToStartTimer -= Time.deltaTime;

            if (_waitingToStartTimer < 0f)
            {
               CurrentGameState = GameState.CountDownToStart;
               OnStateChange?.Invoke(this, EventArgs.Empty);
            }
            
            break;
         case GameState.CountDownToStart:
            CountDownToStartTimer -= Time.deltaTime;
            
            if (CountDownToStartTimer < 0f)
            {
               CurrentGameState = GameState.GamePlaying;
               OnStateChange?.Invoke(this, EventArgs.Empty);
            }
            
            break;
      }
   }
   
   private void GameInput_OnPauseAction(object sender, EventArgs e)
   {
      TogglePauseGame();
   }
   
   private void NoteManager_OnGameOver(object sender, EventArgs e)
   {
      CurrentGameState = GameState.GameOver;
      OnStateChange?.Invoke(this, EventArgs.Empty);
   }

   public void TogglePauseGame()
   {
      if (CurrentGameState is GameState.GameOver or GameState.WaitingToStart)
      {
         return;
      }
      
      _isGamePaused = !_isGamePaused;

      if (_isGamePaused)
      {
         Time.timeScale = 0f;
         OnGamePaused?.Invoke(this, EventArgs.Empty);
      }
      else
      {
         Time.timeScale = 1f;
         OnGameUnpaused?.Invoke(this, EventArgs.Empty);
      }
   }
}
