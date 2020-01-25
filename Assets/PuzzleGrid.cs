using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleGrid : MonoBehaviour
{

    public int GridId;
    public Vector2 PuzzlePoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitGrid(int gridId,Vector2 point,Sprite sprite)
    {
        GridId = gridId;
        PuzzlePoint = point;
        GetComponent<SpriteRenderer>().sprite = sprite;
    }

    private void OnMouseDown()
    {
        LevelManager.Instance.SelectedPuzzleGrid = this;
    }

    private void OnMouseUp()
    {
        LevelManager.Instance.SelectedPuzzleGrid = null;
    }

}
