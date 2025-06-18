using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class Tile : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigidbody2D;
    [SerializeField] float movementSpeed = 5f;
    
    public SpriteRenderer shapeRenderer;
    public SpriteRenderer colorRenderer;
    public SpriteRenderer animalRenderer;
    
    
    public Sprite shape;
    public Color color;
    public Sprite animal;
    
    private TileSpawner _spawner;
    
    public void Setup(Sprite shape, Color color, Sprite animal, TileSpawner spawner)
    {
        shapeRenderer.sprite = shape;
        colorRenderer.sprite = shape;
        colorRenderer.color = color;
        animalRenderer.sprite = animal;

        this._spawner = spawner;
        
        this.shape = shape;
        this.color = color;
        this.animal = animal;
    }

    public bool IsSameType(Tile tile)
    {
        return tile.shape == shape && tile.color == color && tile.animal == animal;
    }

    public void MoveTo(Vector3 position)
    {
        StartCoroutine(MoveAnimation(position));
    }

    private IEnumerator MoveAnimation(Vector3 target)
    {
        rigidbody2D.simulated = false;
        float t = 0;
        Vector3 start = transform.position;
        while (t < 1f)
        {
            t += Time.deltaTime * movementSpeed;
            transform.position = Vector3.Lerp(start, target, t);
            yield return null;
        }
        transform.position = target;
    }

    public void AnimateRemove()
    {
        _spawner.RemoveTileFromPool(gameObject);
        Destroy(gameObject, 0.2f);
    }

    private void OnMouseDown()
    {
        FindFirstObjectByType<ActionBarController>().AddTile(this);
    }
}
