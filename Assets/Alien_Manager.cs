using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEditor;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class Alien_Manager : MonoBehaviour
{

    public GameObject alienPrefabCollection;
    public GameObject handManager;

    public int maxWaitingAliens = 3;
    public int currentlyWaitingAliens = 0;
    public bool isSlotFree = true;

    private GameObject[] alienList;
    private List<GameObject> instantiatedWaitingAliens = new List<GameObject>(); // List to store instantiated aliens waiting in queue

    // Start is called before the first frame update
    void Start()
    {
        GetChildInAlienCollection();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentlyWaitingAliens < maxWaitingAliens)
        {
            NewAlien();
        }


        
        if (isSlotFree)
        {
            if (instantiatedWaitingAliens.Count > 0)
            {
                GameObject firstAlienInLine = instantiatedWaitingAliens[0];
                Debug.Log(firstAlienInLine);

                Alien firstAlienInLineScript = instantiatedWaitingAliens[0].GetComponent<Alien>(); //On recup le script du premier alien dans la file d'attente

                firstAlienInLineScript.isMoving = true;
                firstAlienInLineScript.targetPositionX = 0; //on lui dit ou aller

                isSlotFree = false;
                instantiatedWaitingAliens.Remove(firstAlienInLine);
                //Debug.Log("nb Alien waiting: " + instantiatedWaitingAliens.Count);
            }
        }

        else
        {
            for (int i = 0; i < currentlyWaitingAliens; i++)
            {
                GameObject alienInLine = instantiatedWaitingAliens[i];
                Alien alienInLineScript = alienInLine.GetComponent<Alien>();

                alienInLineScript.isMoving = true;
                alienInLineScript.targetPositionX = -5 - i * 2;
                if (Mathf.Abs(alienInLine.transform.position.x - alienInLineScript.targetPositionX) < 0.2 && alienInLineScript.isMoving) //Si alien atteint la position voulue
                {
                    alienInLineScript.isMoving = false;
                }
            }
        }
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
        GameObject alien = Instantiate(alienList[Random.Range(0, alienList.Length)], transform.position, Quaternion.identity, transform);

        SpriteRenderer renderer = alien.GetComponent<SpriteRenderer>();
        Color randomColor = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f); //couleur aleatoire du alien

        renderer.color = randomColor;
        renderer.sortingLayerID = SortingLayer.NameToID("Alien_client");

        Alien alienScript = alien.GetComponent<Alien>();
        Hand_Manager handManagerScript = handManager.GetComponent<Hand_Manager>();

        if (alienScript != null)
        {
            alienScript.handManager = handManager;
        }
        if (handManagerScript != null)
        {
            handManagerScript.color = randomColor;
        }

        // Add instantiated alien to the list
        instantiatedWaitingAliens.Add(alien);

        currentlyWaitingAliens++;

    }



}
