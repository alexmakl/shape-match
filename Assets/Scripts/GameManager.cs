using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] TileSpawner tileSpawner;
    
    // Start is called before the first frame update
    void Start()
    {
        tileSpawner.Spawn(54);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
