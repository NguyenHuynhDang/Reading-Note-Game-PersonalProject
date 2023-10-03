using System;
using UnityEngine;

public class NoteScroller : MonoBehaviour
{
    private const float DeadZoneX = -4.5f;

    public event EventHandler OnPlayPositionEnter;
    public event EventHandler OnPlayPositionExit;
    
    private void Update()
    {
        ScrollNoteByBeat();
    }
    
    private void ScrollNoteByBeat()
    {
        if (GameManager.Instance.CurrentGameState != GameState.GamePlaying)
        {
            return;
        }
        
        float offset = 1f;
        float distance = GameAssets.Instance.musicalNotePrefab.transform.position.x -
            GameAssets.Instance.playPositionTransform.position.x + offset;
        
        float speed = distance / NoteManager.Instance.TimeBetween8Beat;
        
        transform.position += Vector3.left * (speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        OnPlayPositionEnter?.Invoke(this, EventArgs.Empty);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (transform.position.x < DeadZoneX)
        {
            OnPlayPositionExit?.Invoke(this, EventArgs.Empty);
        }
    }
}
