using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Nails_Behaviour : MonoBehaviour
{

    SpriteRenderer spriteRenderer;
    private Collider2D collider2D;


    public GameObject clean_nail;
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
    private bool canBeCut = true;


    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        collider2D = renderer.GetComponent<Collider2D>();
                
        current_length = UnityEngine.Random.Range(1, sprites.Length);

        renderer.sprite = sprites[current_length];
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnMouseDown()
    {
        // Get mouse position in screen space
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        // Convert to world space
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        if (current_length > 0 && canBeCut)
        {
            current_length--;

            // Spawn the particle at the correct position in world space
            SpawnParticleAtPosition(worldPosition);
        }
        
        if (current_length == 0 && canBeCut) 
        {
            Instantiate(clean_nail, worldPosition, Quaternion.identity);

            IncreaseCompletedNailsCount();
                        
        }

        GetComponent<SpriteRenderer>().sprite = sprites[current_length];
                
    }


    void SpawnParticleAtPosition(Vector2 spawn_position)
    {
        GameObject particle = Instantiate(nail_particle, spawn_position, Quaternion.identity);
    }


    void IncreaseCompletedNailsCount()
    {
        canBeCut = false;
        Transform parentTransform = transform.parent;

        Hand parentHand = parentTransform.GetComponent<Hand>(); //on recupere le parent (ici, la main)

        if (parentHand != null)
        {
            parentHand.OneMoreCleanNail();
        }
    }
}
