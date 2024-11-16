using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hand_SO_Manager : MonoBehaviour
{
    public Hand_SO[] handList = null;

    public Image handImage;

    private Hand_SO hand;
    private GameObject[] nailList;

    // Start is called before the first frame update
    void Start()
    {
        hand = handList[Random.Range(0, handList.Length)]; //random hand from list
        print(hand);

        handImage.sprite = hand.sprite;

        getChildInNailCollection();

        for (int i = 0; i < hand.nbFingers; i++) {
            Instantiate(nailList[Random.Range(0,nailList.Length-1)], hand.nailLocations[i], Quaternion.identity);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void getChildInNailCollection()
    {
        nailList = new GameObject[hand.nailPrefab.transform.childCount]; //On cree la liste de gameobjects 'nail' depuis la nailPrefab de hand

        for (int i = 0; i < hand.nailPrefab.transform.childCount; i++)
        {
            nailList[i] = hand.nailPrefab.transform.GetChild(i).gameObject;
        }

        foreach (var nail in nailList)
        {
            Debug.Log("Nail: " + nail.name);
        }
    }
}
