using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Nails_Behaviour : MonoBehaviour
{

    SpriteRenderer spriteRenderer;

    //public UnityEngine.Color nail_color;

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
        current_length--;
        GetComponent<SpriteRenderer>().sprite = sprites[current_length];
    }

}
