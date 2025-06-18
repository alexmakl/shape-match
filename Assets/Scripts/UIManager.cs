using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject winScreen;
    public GameObject loseScreen;

    public void ShowWin()
    {
        winScreen.SetActive(true);
        loseScreen.SetActive(false);
    }

    public void ShowLose()
    {
        loseScreen.SetActive(true);
        winScreen.SetActive(false);
    }
    
    public void HideAll()
    {
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
    }
}
