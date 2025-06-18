using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public enum SpecialType
{
    None,
    Heavy,
    Frozen
}

public class Tile : MonoBehaviour
{
    public bool canClick = false;
    
    [SerializeField] new Rigidbody2D rigidbody2D;
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] GameObject explosion;
    [SerializeField] GameObject specialTypeGO;
    
    public SpriteRenderer shapeRenderer;
    public SpriteRenderer colorRenderer;
    public SpriteRenderer animalRenderer;
    
    public Sprite shape;
    public Color color;
    public Sprite animal;
    
    public SpecialType specialType;
    public bool isFrozen = false;
    
    private TileSpawner _spawner;
    private ActionBarController _actionBarController;
    
    public void Setup(
        Sprite shape, 
        Color color, 
        Sprite animal, 
        SpecialType specialType, 
        TileSpawner spawner, 
        ActionBarController actionBarController)
    {
        shapeRenderer.sprite = shape;
        colorRenderer.sprite = shape;
        colorRenderer.color = color;
        animalRenderer.sprite = animal;

        _spawner = spawner;
        _actionBarController = actionBarController;
        
        this.shape = shape;
        this.color = color;
        this.animal = animal;
        this.specialType = specialType;

        switch (specialType)
        {
            case SpecialType.Heavy:
                rigidbody2D.gravityScale = 3f;
                gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.gray;
                break;
            case SpecialType.Frozen:
                specialTypeGO.SetActive(true);
                isFrozen = true;
                break;
        } 
    }

    public bool IsSameType(Tile tile)
    {
        return tile.shape == shape && tile.color == color && tile.animal == animal;
    }

    public IEnumerator MoveTo(bool startDelay, Vector3 target, System.Action onComplete = null)
    {
        if (startDelay)
        {
            yield return new WaitForSeconds(0.2f);
        }
        rigidbody2D.simulated = false;
        float t = 0;
        Vector3 start = transform.position;
        while (t < 1f)
        {
            t += Time.deltaTime * movementSpeed;
            transform.position = Vector3.Lerp(start, target, t);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, t);
            yield return null;
        }
        transform.position = target;
        yield return new WaitForSeconds(0.1f);
        onComplete?.Invoke();
    }

    public void AnimateRemove()
    {
        _spawner.RemoveTileFromPool(gameObject);
        StartCoroutine(ExplosionThenDestroy());
    }

    public void Unfreeze()
    {
        isFrozen = false;
        specialTypeGO.SetActive(false);
    }

    private IEnumerator ExplosionThenDestroy()
    {
        explosion.SetActive(true);
        
        shapeRenderer.enabled = false;
        colorRenderer.enabled = false;
        animalRenderer.enabled = false;
        
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        if (!canClick || isFrozen) return;
        _actionBarController.AddTile(this);
    }
}
