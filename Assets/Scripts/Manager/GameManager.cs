using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance { get; private set; }
   
   private GameState _currentGameState;

   private float _waitingToStartTimer = 1f;
   private float _countDownToStartTimer = 3f;

   private void Awake()
   {
      Instance = this;
      _currentGameState = GameState.WaitingToStart;
   }

   private void Update()
   {
      switch (_currentGameState)
      {
         case GameState.WaitingToStart:
            _waitingToStartTimer -= Time.deltaTime;

            if (_waitingToStartTimer < 0f)
            {
               _currentGameState = GameState.CountDownToStart;
            }
            
            break;
         case GameState.CountDownToStart:
            if (_waitingToStartTimer < 0f)
            {
               _currentGameState = GameState.GamePlaying;
            }
            
            break;
      }
   }

   public bool IsGamePlaying()
   {
      return _currentGameState == GameState.GamePlaying;
   }
}
