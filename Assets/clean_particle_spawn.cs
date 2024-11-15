using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clean_particle_spawn : MonoBehaviour
{

    private AudioSource clean_sound;
    // Start is called before the first frame update
    void Start()
    {
        clean_sound = GetComponent<AudioSource>();
        clean_sound.pitch = Random.Range(0.9f, 1.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
