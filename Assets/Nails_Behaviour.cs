using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Nails_Behaviour : MonoBehaviour
{

    SpriteRenderer spriteRenderer;

    //public UnityEngine.Color nail_color;

    public GameObject nail_particle;

    [SerializeField] private int nail_length;
    
    public enum Orientation
    {
        Top,
        Left, 
        Right
    };

    public Orientation nailOrientation;

    public Sprite[] sprites;

    private int current_length;



    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();

        current_length = UnityEngine.Random.Range(1, sprites.Length);

        renderer.sprite = sprites[current_length];
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnMouseDown()
    {
        if (current_length > 0)
        {
            current_length--;
        }
        
        GetComponent<SpriteRenderer>().sprite = sprites[current_length];

        // Get mouse position in screen space
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        // Convert to world space
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Spawn the particle at the correct position in world space
        SpawnPartcileAtPosition(worldPosition);
    }


    void SpawnPartcileAtPosition(Vector2 spawn_position)
    {
        GameObject particle = Instantiate(nail_particle, spawn_position, Quaternion.identity);
    }

}
