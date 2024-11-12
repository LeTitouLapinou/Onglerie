using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Hand : MonoBehaviour
{

    public GameObject nail_collectionPrefab;

    //public int nail_number = 5;
    //public Color nail_color;

    public Transform[] locators;

    private GameObject[] nailList;
    private GameObject nail_collectionInstance;

    // Start is called before the first frame update
    void Start()
    {
        nail_collectionInstance = Instantiate(nail_collectionPrefab, new Vector3(1000f, 1000f, 1000f), Quaternion.identity);

        //nail_color = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);

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
            GameObject nailInstance = Instantiate(nailList[Random.Range(0,nailList.Length)], locators[i].position, locators[i].rotation); //On choisit un nail au hasard dans la nailList, et on en spawn un a chaque locator
        }
    }


    //GameObject nailInstance = Instantiate(nail, locators[i].position, locators[i].rotation);

}
