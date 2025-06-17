using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Tile : MonoBehaviour
{
    public SpriteRenderer shapeRenderer;
    public SpriteRenderer colorRenderer;
    public SpriteRenderer animalRenderer;
    
    public void Setup(Sprite shape, Color color, Sprite animal)
    {
        shapeRenderer.sprite = shape;
        colorRenderer.sprite = shape;
        colorRenderer.color = color;
        animalRenderer.sprite = animal;
    }
}
