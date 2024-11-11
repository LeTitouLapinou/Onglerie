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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
