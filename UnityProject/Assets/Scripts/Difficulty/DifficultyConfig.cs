using UnityEngine;

[CreateAssetMenu(fileName = "DifficultyConfig", menuName = "Scriptable Objects/DifficultyConfig")]
public class DifficultyConfig : ScriptableObject
{
    [field: SerializeField]
    public int MaxDifficulty { get; private set; } = 9;
}
