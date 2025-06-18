using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] TileSpawner tileSpawner;
    [SerializeField] int tilesToSpawn = 54;
    [SerializeField] int tilesToUnfreeze = 9;
    
    private int _currentLevel = 1;

    public void StartCurrentLevel()
    {
        StartLevel(_currentLevel);
    }

    public void NextLevel()
    {
        StartLevel(_currentLevel + 1);
    }

    public void UnfreezeIfPossible(int count)
    {
        if (count >= tilesToUnfreeze)
        {
            tileSpawner.UnfreezeAll();
        }
    }

    private void StartLevel(int levelNum)
    {
        _currentLevel = levelNum;
        tileSpawner.Spawn(tilesToSpawn);
    }
}
