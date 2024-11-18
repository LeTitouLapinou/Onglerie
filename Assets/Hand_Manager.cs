using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class Hand_Manager : MonoBehaviour
{

    public GameObject hand_collection_prefab;
    //public Vector3 spawn_position;
    //public Quaternion spawn_rotation;

    public float springDuration = 1f; // Duration of one full oscillation (spring cycle)
    public float springStrength = 0.2f; // How much overshoot the spring has
    public Vector3 centerPosition = new Vector3(0f, 3.5f, 0f); // The maximum off-screen position


    private GameObject[] handList;
    
    

    // Start is called before the first frame update
    void Start()
    {
        getChildInHandCollection();

        NewHand();
        
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
        GameObject selectedHand = Instantiate(handList[Random.Range(0, handList.Length - 1)], this.transform.position, this.transform.rotation, this.transform);

        //On atrtibue au child les valeurs mises dans le manager
        Hand handScript = selectedHand.GetComponent<Hand>();
        if (handScript != null)
        {
            handScript.springDuration = springDuration;
            handScript.springStrength = springStrength;
            handScript.centerPosition = centerPosition;
        }
    }

}
