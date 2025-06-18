using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] TileSpawner tileSpawner;
    [SerializeField] UIManager uiManager;
    [SerializeField] LevelManager levelManager;

    public GameState State { get; private set; }

    void Start()
    {
        ChangeState(GameState.GeneratingField);
        levelManager.StartCurrentLevel();
    }

    public void OnWin()
    {
        ChangeState(GameState.Win);
    }
    public void OnLose()
    {
        ChangeState(GameState.Lose);
    }
    
    public void OnNextLevel()
    {
        ChangeState(GameState.GeneratingField);
        levelManager.NextLevel();
    }
    
    public void OnRestartLevel()
    {
        ChangeState(GameState.GeneratingField);
        levelManager.StartCurrentLevel();
    }
    
    public void ChangeState(GameState newState)
    {
        Debug.Log($"State change: {State} → {newState}");
        State = newState;
        switch (State)
        {
            case GameState.GeneratingField:
                uiManager.HideAll();
                break;
            case GameState.PlayerInput:
                EnablePlayerInput(true);
                break;
            case GameState.Animating:
                EnablePlayerInput(false);
                break;
            case GameState.Win:
                EnablePlayerInput(false);
                uiManager.ShowWin();
                break;
            case GameState.Lose:
                EnablePlayerInput(false);
                uiManager.ShowLose();
                break;
        }
    }

    private void EnablePlayerInput(bool enable)
    {
        tileSpawner.EnableAllTiles(enable);
    }
}
