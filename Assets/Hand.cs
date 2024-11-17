using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hand : MonoBehaviour
{

    public GameObject nail_collectionPrefab;

    public int clean_nails_number = 0;
    //public Color nail_color;

    public Transform[] locators;

    private GameObject[] nailList;
    private GameObject nail_collectionInstance;

    // Start is called before the first frame update
    void Start()
    {
        nail_collectionInstance = Instantiate(nail_collectionPrefab, new Vector3(1000f, 1000f, 1000f), Quaternion.identity); //On instancie le prefab de nails pour qu'il soit dans la scene
        
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();

        renderer.color = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f); //couleur aleatoire de la main

        getChildInNailCollection();
        InstantiateNailsAtLocators();


    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void getChildInNailCollection()
    {
        nailList = new GameObject[nail_collectionInstance.transform.childCount]; //On cree la liste de gameobjects 'nail' depuis la nail_collection

        for (int i = 0; i <  nail_collectionInstance.transform.childCount; i++)
        {
            nailList[i] = nail_collectionInstance.transform.GetChild(i).gameObject;
        }

        foreach (var nail in nailList)
        {
            Debug.Log("Nail: " + nail.name);
        }
    }

    public void InstantiateNailsAtLocators()
    {
        for (int i = 0;i < locators.Length; i++)
        {
            GameObject nailInstance = Instantiate(nailList[Random.Range(0,nailList.Length)], locators[i].position, locators[i].rotation, this.transform); //On choisit un nail au hasard dans la nailList, et on en spawn un a chaque locator
        }
    }


    public void OneMoreCleanNail()
    {
        print("un ongle en plus!");
        clean_nails_number++;

        if (clean_nails_number == locators.Length)
        {
            HandDone();
        }

    }

    public void HandDone()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // loads current scene
    }

}
