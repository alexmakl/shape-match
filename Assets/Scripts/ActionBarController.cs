using System;
using System.Collections.Generic;
using UnityEngine;

public class ActionBarController: MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    public Transform[] slotTransforms;
    public Tile[] slots;
    
    private int _maxSlots = 7;

    private void Start()
    {
        slots = new Tile[_maxSlots];
    }

    public bool AddTile(Tile tile)
    {
        int lastIndex = -1;
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] != null && slots[i].IsSameType(tile))
                lastIndex = i;
        }
        
        int insertIndex;
        if (lastIndex >= 0)
            insertIndex = lastIndex + 1;
        else
        {
            insertIndex = 0;
            for (int i = 0; i < _maxSlots; i++)
            {
                if (slots[i] == null)
                {
                    insertIndex = i;
                    break;
                }
            }
        }

        if (insertIndex >= _maxSlots)
        {
            gameManager.OnLose();
            return false;
        }
        
        for (int i = _maxSlots - 1; i > insertIndex; i--)
        {
            if (slots[i - 1] != null)
            {
                slots[i] = slots[i - 1];
                slots[i].StartCoroutine(slots[i].MoveTo(false, slotTransforms[i].position));
            }
        }
        
        slots[insertIndex] = tile;
        
        tile.StartCoroutine(tile.MoveTo(false, slotTransforms[insertIndex].position, () =>
        {
            CheckTriplets();
        }));

        return true;
    }

    private void CheckTriplets()
    {
        bool actionBarIsFull = false;
        for (int i = 0; i <= _maxSlots - 3; i++)
        {
            if (slots[i] != null && slots[i + 1] != null && slots[i + 2] != null)
            {
                if (slots[i].IsSameType(slots[i + 1]) && slots[i].IsSameType(slots[i + 2]))
                {
                    RemoveTiles(i, 3);
                    actionBarIsFull = false;
                }
                else
                {
                    actionBarIsFull = CheckActionBarIsFull();
                }
            }
        }

        if (actionBarIsFull)
        {
            gameManager.OnLose();
        }
    }

    private bool CheckActionBarIsFull()
    {
        return slots[_maxSlots - 1] != null;
    }

    private void RemoveTiles(int start, int count)
    {
        for (int i = start; i < start + count; i++)
        {
            if (slots[i] != null)
            {
                slots[i].AnimateRemove();
                slots[i] = null;
            }
        }

        for (int i = start + count; i < _maxSlots; i++)
        {
            slots[i - count] = slots[i];
            if (slots[i - count] != null)
                slots[i - count].StartCoroutine(slots[i - count].MoveTo(true, slotTransforms[i - count].position));
            slots[i] = null;
        }
    }
}