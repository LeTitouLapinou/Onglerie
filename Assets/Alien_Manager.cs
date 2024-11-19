using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEditor;
using UnityEngine;

public class Alien_Manager : MonoBehaviour
{

    public GameObject alienPrefabCollection;
    public GameObject handManager;


    private GameObject[] alienList;

    // Start is called before the first frame update
    void Start()
    {
        GetChildInAlienCollection();
        NewAlien();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GetChildInAlienCollection()
    {
        alienList = new GameObject[alienPrefabCollection.transform.childCount]; //On cree la liste de gameobjects 'nail' depuis la nail_collection

        for (int i = 0; i < alienPrefabCollection.transform.childCount; i++)
        {
            alienList[i] = alienPrefabCollection.transform.GetChild(i).gameObject;
        }
    }

    public void NewAlien()
    {
        GameObject alien_01 = Instantiate(alienList[Random.Range(0, alienList.Length)], transform.position, Quaternion.identity);

        SpriteRenderer renderer = alien_01.GetComponent<SpriteRenderer>();
        Color randomColor = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f); //couleur aleatoire du alien

        renderer.color = randomColor;

        Alien alienScript = alien_01.GetComponent<Alien>();
        Hand_Manager handManagerScript = handManager.GetComponent<Hand_Manager>();

        if (alienScript != null)
        {
            alienScript.handManager = handManager;
        }
        if (handManagerScript != null)
        {
            handManagerScript.color = randomColor;
        }

    }



}
