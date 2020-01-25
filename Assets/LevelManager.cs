using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public PuzzleController Puzzle;
    public PuzzleGrid SelectedPuzzleGrid;
    public SwipeInputController swipeController;
    public Text TimeText;
    public GameObject WinPanel;
    private float Timer = 0;
    private bool StartCountTime = false;
    private bool HasWin = false;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        swipeController.OnSwipeAction += HandleSwipe;
        WinPanel.SetActive(false);
    }

    public void HandleSwipe(SwipeGesture gesture)
    {
        if (SelectedPuzzleGrid != null)
        {
            StartCountTime = true;
            Puzzle.OnReceiveMoveEvent(SelectedPuzzleGrid, gesture);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (StartCountTime&&!HasWin)
        {
            Timer += Time.deltaTime;
            TimeText.text = String.Format("{0:F}", Timer);
        }
    }

    public void Win()
    {
        HasWin = true;
        WinPanel.SetActive(true);
        WinPanel.GetComponent<WinPanel>().InitInfos(Timer);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
