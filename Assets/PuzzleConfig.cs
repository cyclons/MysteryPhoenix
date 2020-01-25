using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PuzzleConfig", menuName = "Configs/PuzzleConfig")]
[System.Serializable]
public class PuzzleConfig : ScriptableObject
{
    [SerializeField]
    public PuzzleGridInfo[] puzzleInfos;

    public int[] WinCheck;
}
