using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    [SerializeField] TileCatalog tileCatalog;
    [SerializeField] GameObject tilePrefab;
    [SerializeField] int columns = 1;
    [SerializeField] GameManager gameManager;
    
    public float dropInterval = 0.05f;
    public float tileScale = 0.35f;

    private List<GameObject> _tilesPool = new();
    
    public void Spawn(int count)
    {
        if (_tilesPool.Count > 0)
        {
            foreach (GameObject o in _tilesPool)
            {
                Destroy(o);
            }
            _tilesPool.Clear();
        }
        
        List<TileConfig> tiles = GenerateTiles(count);
        StartCoroutine(SpawnTiles(tiles));
    }

    public void RespawnAll()
    {
        int count = _tilesPool.Count;
        
        Spawn(count);
    }

    public void RemoveTileFromPool(GameObject tile)
    {
        _tilesPool.Remove(tile);
        if (CheckTilesPoolIsEmpty())
        {
            gameManager.OnWin();
        }
    }

    public void EnableAllTiles(bool enable)
    {
        if (_tilesPool.Count > 0)
        {
            foreach (GameObject o in _tilesPool)
            {
                o.GetComponent<Tile>().canClick = enable;
            }
        }
    }

    private bool CheckTilesPoolIsEmpty()
    {
        return _tilesPool.Count == 0;
    }

    private List<TileConfig> GenerateTiles(int totalTiles)
    {
        var combos = new List<TileConfig>();
        var possibleCombos = new List<TileConfig>();

        for (int s = 0; s < tileCatalog.shapes.Length; s++)
            for (int c = 0; c < tileCatalog.colors.Length; c++)
                for (int a = 0; a < tileCatalog.animals.Length; a++)
                    possibleCombos.Add(new TileConfig { shapeIndex = s, colorIndex = c, animalIndex = a });

        possibleCombos = possibleCombos.OrderBy(x => Random.value).ToList();

        int neededCombos = totalTiles / 3;
        for (int i = 0; i < neededCombos; i++)
            for (int j = 0; j < 3; j++)
                combos.Add(possibleCombos[i]);

        combos = combos.OrderBy(x => Random.value).ToList();

        return combos;
    }

    private IEnumerator SpawnTiles(List<TileConfig> configs)
    {
        Camera camera = Camera.main;
        float cameraSize = camera.orthographicSize;
        float spawnWidth = cameraSize * camera.aspect;
        float spawnLeft = -spawnWidth + 0.2f;
        float spawnRight = spawnWidth - 0.2f;
        float tileWidth = (spawnRight - spawnLeft) / columns;
        float spawnYstart = camera.transform.position.y + cameraSize + 1.5f;

        for (int i = 0; i < configs.Count; i++)
        {
            int column = i % columns;
            int row = i / columns;
            float x = spawnLeft + column * tileWidth + tileWidth / 2f + Random.Range(-0.1f, 0.1f);
            float y = spawnYstart + row * (tileScale + 0.2f);

            GameObject tileObject = Instantiate(tilePrefab, new Vector3(x, y, 0), Quaternion.identity);
            tileObject.transform.localScale *= tileScale;
            tileObject.transform.parent = transform;
            
            Tile tile = tileObject.GetComponent<Tile>();
            tile.Setup(
                tileCatalog.shapes[configs[i].shapeIndex],
                tileCatalog.colors[configs[i].colorIndex],
                tileCatalog.animals[configs[i].animalIndex],
                this,
                gameManager.actionBarController
            );
            
            _tilesPool.Add(tileObject);

            yield return new WaitForSeconds(dropInterval);
        }

        yield return StartCoroutine(WaitAllTilesDropped());
    }

    private IEnumerator WaitAllTilesDropped()
    {
        float velocityThreshold = 0.1f;
        
        while (true)
        {
            bool allStopped = true;
            foreach (var tile in _tilesPool)
            {
                Rigidbody2D rb = tile.GetComponent<Rigidbody2D>();
                if (rb != null && Mathf.Abs(rb.velocity.y) > velocityThreshold)
                {
                    allStopped = false;
                    break;
                }
            }
            if (allStopped) break;
            yield return null;
        }

        gameManager.ChangeState(GameState.PlayerInput);
    }
}