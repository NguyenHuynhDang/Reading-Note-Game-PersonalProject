using UnityEngine;

public class ResetStaticDataManager : MonoBehaviour
{
    public static ResetStaticDataManager Instance;
    private void Awake()
    {
        Instance = this;
        Reset();
    }

    public void Reset()
    {
        MusicalNote.ResetStaticData();
    }
}
