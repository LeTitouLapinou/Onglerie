using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Hand : MonoBehaviour
{

    public GameObject nail_collection;

    public int nail_number = 5;

    public Color nail_color;

    public Transform[] locators;

    // Start is called before the first frame update
    void Start()
    {
        nail_color = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);

        for(int i = 0; i < nail_collection.transform.hierarchyCount; i++)
        {
            //ESSAYER d'ajouter tous les child du prefab dans une liste, pour ensuite prendre des child random dans la boucle for suivante
        }



        for (int i = 0; i < locators.Length; i++)
        {
            GameObject nail = nail_collection.GetComponent<GameObject>();
            GameObject nailInstance = Instantiate(nail, locators[i].position, locators[i].rotation);
            nailInstance.layer = gameObject.layer +1;
            SpriteRenderer rend = nailInstance.GetComponent<SpriteRenderer>();
            //rend.color = nail_color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
