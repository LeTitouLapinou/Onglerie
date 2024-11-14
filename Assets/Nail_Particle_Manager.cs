using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Nail_Particle_Manager : MonoBehaviour
{

    public Sprite[] nail_particle_sprites;

    public float gravity = -9.8f; // Gravity constant (adjustable)
    public float initialVelocityY = 5f; // Initial upward velocity (optional)
    public Vector2 velocity; // The particle's velocity

    // Start is called before the first frame update
    void Start()
    {

        SpriteRenderer renderer = GetComponent<SpriteRenderer>();

        renderer.sprite = nail_particle_sprites[Random.Range(0, nail_particle_sprites.Length - 1)];

        // Initialize the velocity (you can tweak the initial Y velocity here)

        initialVelocityY = 3;
        velocity = new Vector2(0, initialVelocityY);
        
    }

    // Update is called once per frame
    void Update()
    {
        // Apply gravity (change the velocity over time)
        velocity.y += gravity * Time.deltaTime;

        // Update the particle's position based on the velocity
        transform.position += (Vector3)velocity * Time.deltaTime;

        
        
        if (transform.position.y < -5f) // Adjust this threshold to your needs
        {
            Destroy(gameObject); // Destroy the particle when it falls off-screen (optional)
        }
    }

    
}
