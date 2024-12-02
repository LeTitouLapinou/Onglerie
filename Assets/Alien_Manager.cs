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

    public int maxWaitingAliens = 2;
    public int currentlyWaitingAliens = 0;
    public bool isSlotFree = true;

    public bool isSlot01Free = false;
    public bool isSlot02Free = true;
    public bool isSlot03Free = false;

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
        if (instantiatedWaitingAliens.Count < maxWaitingAliens)
        {
            NewAlien(); //nouvel alien si il y en a moins que la file d'attente autorisée
        }


        if (isSlot01Free || isSlot02Free || isSlot03Free) //Check si il y a un emplacement libre
        {
            AssignCounterPosition();
        }
        else
        {

            for (int i = 1; i < instantiatedWaitingAliens.Count; i++) //bouger file attente
            {
                GameObject alienInLine = instantiatedWaitingAliens[i];
                Alien alienInLineScript = alienInLine.GetComponent<Alien>();

                alienInLineScript.targetPositionX = -5 - (3*i); //espacement de la file d'attente
            }
        }

    }

                     
    public void GetChildInAlienCollection()
    {
        alienList = new GameObject[alienPrefabCollection.transform.childCount]; //On cree la liste de gameobjects 'alien' depuis la alien_collection

        for (int i = 0; i < alienPrefabCollection.transform.childCount; i++)
        {
            alienList[i] = alienPrefabCollection.transform.GetChild(i).gameObject;
        }
    }


    public void AssignCounterPosition()
    {
        Debug.Log("Assign counter position");

        float tempTargetPositionX = -10;
        int tempAssignedSlot = 0;

        if (isSlot01Free)
        {
            tempTargetPositionX = -5;
            tempAssignedSlot = 1;
            isSlot01Free = false;
        }
        else if (isSlot02Free)
        {
            Debug.Log("Assign counter position Slot 2");

            tempTargetPositionX = 0;
            tempAssignedSlot = 2;
            isSlot02Free = false;
        }
        else if (isSlot03Free)
        {
            tempTargetPositionX = 5;
            tempAssignedSlot = 3;
            isSlot03Free = false;
        }



        // Ensure that there is at least one alien waiting
        if (instantiatedWaitingAliens.Count > 0)
        {
            GameObject firstAlienInLine = instantiatedWaitingAliens[0];
            Alien firstAlienInLineScript = firstAlienInLine.GetComponent<Alien>();


            Debug.Log("First Alien is " + firstAlienInLine.name);
            if (firstAlienInLineScript != null)
            {
                Debug.Log("caca");
                // Update targetPositionX and assignedSlot
                firstAlienInLineScript.targetPositionX = tempTargetPositionX;
                firstAlienInLineScript.assignedSlot = tempAssignedSlot;
            }

            instantiatedWaitingAliens.Remove(firstAlienInLine);
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

        /*
        foreach (var x in instantiatedWaitingAliens)
        {
            Debug.Log(x.ToString());
        }
        */



    }



}
