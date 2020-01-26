using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PuzzleController : MonoBehaviour
{
    public PuzzleConfig Config;
    public float PuzzleDistance;
    public Transform StartTrans;
    public GameObject PuzzlePrefab;
    public int RowAmount=4;
    PuzzleGrid[,] grids;
    private int[] currentGridIds;

    private void Awake()
    {
        Vector3 spawnPoint = StartTrans.position;
        grids = new PuzzleGrid[Config.puzzleInfos.Length/RowAmount,RowAmount];
        currentGridIds = new int[Config.WinCheck.Length];
        for(int i = 0; i < Config.puzzleInfos.Length; i++)
        {
            int gridId = Config.puzzleInfos[i].Id;
            spawnPoint = StartTrans.position - PuzzleDistance * Vector3.up * (i / RowAmount) + PuzzleDistance * Vector3.right * (i % RowAmount);
            if (gridId != -1)
            {
                GameObject go = Instantiate(PuzzlePrefab, spawnPoint, Quaternion.identity, StartTrans);
                grids[i / RowAmount, i % RowAmount] = go.GetComponent<PuzzleGrid>();
                go.GetComponent<PuzzleGrid>().InitGrid(gridId, new Vector2(i / RowAmount, i % RowAmount),Config.puzzleInfos[i].PuzzleSprite);
            }
            else
            {
                grids[i / RowAmount, i % RowAmount] = null;
            }
        }
    }

    public void OnReceiveMoveEvent(PuzzleGrid moveGrid,SwipeGesture gesture)
    {
        Debug.Log("收到消息");
        Vector2 TranslateGesture = Vector2.zero;
        switch (gesture)
        {
            case SwipeGesture.Up:
                TranslateGesture = new Vector2(-1, 0);
                break;
            case SwipeGesture.Down:
                TranslateGesture = new Vector2(1, 0);
                break;
            case SwipeGesture.Right:
                TranslateGesture = new Vector2(0, 1);
                break;
            case SwipeGesture.Left:
                TranslateGesture = new Vector2(0, -1);
                break;
        }
        var TargetPuzzlePoint = moveGrid.PuzzlePoint + TranslateGesture;
        Debug.Log(moveGrid.PuzzlePoint);
        Debug.Log(TargetPuzzlePoint);
        
        if(TargetPuzzlePoint.x>= Config.puzzleInfos.Length / RowAmount 
            || TargetPuzzlePoint.y >= RowAmount
            ||TargetPuzzlePoint.x<0
            ||TargetPuzzlePoint.y<0)
        {
            return;
        }

        if (grids[(int)TargetPuzzlePoint.x, (int)TargetPuzzlePoint.y] == null)
        {
            StartMove(moveGrid, TargetPuzzlePoint, gesture);
        }

    }

    private void StartMove(PuzzleGrid moveGrid, Vector2 targetPoint, SwipeGesture gesture)
    {
        Debug.Log("开始移动");

        grids[(int)moveGrid.PuzzlePoint.x, (int)moveGrid.PuzzlePoint.y] = null;
        grids[(int)targetPoint.x, (int)targetPoint.y] = moveGrid;
        moveGrid.PuzzlePoint = targetPoint;

        Vector2 MoveDir = Vector2.zero;
        switch (gesture)
        {
            case SwipeGesture.Up:
                MoveDir = Vector2.up;
                break;
            case SwipeGesture.Down:
                MoveDir = Vector2.down;
                break;
            case SwipeGesture.Right:
                MoveDir = Vector2.right;
                break;
            case SwipeGesture.Left:
                MoveDir = Vector2.left;
                break;
        }

        moveGrid.transform.DOMove(StartTrans.position - Vector3.up * targetPoint.x*PuzzleDistance + Vector3.right * targetPoint.y*PuzzleDistance,0.2f);
        //moveGrid.transform.position += (Vector3)MoveDir * PuzzleDistance;

        GetComponent<AudioSource>().Play();

        CheckWin();
    }

    //判断是否胜利
    void CheckWin()
    {
        for(int i = 0; i < currentGridIds.Length; i++)
        {
            if(grids[i / RowAmount, i % RowAmount] != null)
            {
                currentGridIds[i] = grids[i / RowAmount, i % RowAmount].GridId;
            }
            else
            {
                currentGridIds[i] = -1;
            }
            if (currentGridIds[i] != Config.WinCheck[i])
            {
                return;
            }
        }

        LevelManager.Instance.Win();
        Debug.Log("win");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}



[System.Serializable]
public struct PuzzleGridInfo
{
    public bool IsFilled;
    public int Id;
    public Sprite PuzzleSprite;
}
