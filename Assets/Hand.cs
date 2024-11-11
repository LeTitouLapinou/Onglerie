using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{

    public GameObject nail;

    public int nail_number = 5;
    public Transform[] locators;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < locators.Length; i++)
        {
            GameObject nailInstance = Instantiate(nail, locators[i].position, locators[i].rotation);
            nailInstance.layer = gameObject.layer + 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
