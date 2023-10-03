using UnityEngine;

public class GameAssets : MonoBehaviour
{
    public static GameAssets Instance { get; private set; }
    
    public MusicalNote musicalNotePrefab;
    public Transform playPositionTransform;
    
    protected GameAssets() {} // protected constructor
    
    private void Awake()
    {
        Instance = this;
    }
}
