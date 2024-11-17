using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class Hand_Manager : MonoBehaviour
{

    public GameObject hand_collection_prefab;
    public Vector3 spawn_position;
    
    private GameObject[] handList;
    
    

    // Start is called before the first frame update
    void Start()
    {
        getChildInHandCollection();


        Instantiate(handList[Random.Range(0, handList.Length - 1)], spawn_position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void getChildInHandCollection()
    {
        //GameObject originalGameObject = GameObject.Find("Hand_Manager"); // On recupere le Hand_manager de la scene

        handList = new GameObject[hand_collection_prefab.transform.childCount]; //On cree la liste de gameobjects 'nail' depuis la nail_collection

        for (int i = 0; i < hand_collection_prefab.transform.childCount; i++)
        {
            handList[i] = hand_collection_prefab.transform.GetChild(i).gameObject;
        }

    }

    public void NewHand()
    {

    }

}
