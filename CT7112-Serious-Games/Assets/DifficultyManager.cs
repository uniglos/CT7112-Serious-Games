using UnityEngine;

public enum DifficultyPreset
{
    Easy,
    Normal,
    Hard,
    Insane
}

public class DifficultyManager : MonoBehaviour
{
    public static float GlobalDifficultyMultiplier = 1f;

    public void SetPreset(DifficultyPreset preset)
    {
        switch (preset)
        {
            case DifficultyPreset.Easy:
                GlobalDifficultyMultiplier = 0.5f;
                break;

            case DifficultyPreset.Normal:
                GlobalDifficultyMultiplier = 0.75f;
                break;

            case DifficultyPreset.Hard:
                GlobalDifficultyMultiplier = 1f;
                break;

            case DifficultyPreset.Insane:
                GlobalDifficultyMultiplier = 1.5f;
                break;
        }
    }
}

