using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class MusicalNote : MonoBehaviour
{
    
    private bool _canBePress;
    private NoteScroller _noteScroller;

    public event EventHandler<OnNoteInstantiateEventArgs> OnInstantiate;
    
    public static event EventHandler<OnNoteHitEventArgs> OnNoteHit;
    public static event EventHandler<OnNoteMissedEventArgs> OnNoteMissed;
    
    public Note Note { get; private set; }

    private void Awake()
    {
        SetMusicalNoteRandomly();
        _noteScroller = gameObject.GetComponent<NoteScroller>();
    }

    private void Start()
    {
        OnInstantiate?.Invoke(this, new OnNoteInstantiateEventArgs {Note = Note});
        GameInput.Instance.OnPlayPitch += GameInput_OnPlayPitch;
        
        _noteScroller.OnPlayPositionEnter += NoteScroller_OnPlayPositionEnter;
        _noteScroller.OnPlayPositionExit += NoteScroller_OnPlayPositionExit;
    }

    private void NoteScroller_OnPlayPositionEnter(object sender, EventArgs e)
    {
        _canBePress = true;
    }
    
    private void NoteScroller_OnPlayPositionExit(object sender, EventArgs e)
    {
        OnNoteMissed?.Invoke(this, new OnNoteMissedEventArgs { MusicalNote = this });
    }

    private void GameInput_OnPlayPitch(object sender, OnPitchToggleEventArgs e)
    {
        if (_canBePress && GetNotePitch() != e.PitchToggle && this)
        {
            OnNoteMissed?.Invoke(this, new OnNoteMissedEventArgs { MusicalNote = this });
            return;
        }
        
        if (_canBePress && GetNotePitch() == e.PitchToggle && this)
        {
            OnNoteHit?.Invoke(this, new OnNoteHitEventArgs { MusicalNote = this });
            return;
        }
    }
    
    private void SetMusicalNoteRandomly()
    {
        int numberOfNotes = Enum.GetNames(typeof(Note)).Length;
        int noteNumber = Random.Range(0, numberOfNotes);
        Note = (Note)noteNumber;
    }
    
    private Pitch GetNotePitch()
    {
        switch ((int)Note % 7)
        {
            case 0:
                return Pitch.G;
            case 1:
                return Pitch.A;
            case 2:
                return Pitch.B;
            case 3:
                return Pitch.C;
            case 4:
                return Pitch.D;
            case 5:
                return Pitch.E;
            case 6:
                return Pitch.F;
        }
        
        // the function never reaches here
        return Pitch.A;
    }

    public static void ResetStaticData()
    {
        OnNoteHit = null;
        OnNoteMissed = null;
    }
}
