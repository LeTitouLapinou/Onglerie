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

    public float springDuration; // Duration of one full oscillation (spring cycle)
    public float springStrength; // How much overshoot the spring has
    public Vector3 centerPosition = new Vector3(0, 3, 0); // The maximum off-screen position
    

    private GameObject[] nailList;
    private GameObject nail_collectionInstance;

    private bool isHandDone = false;
    private Vector3 originalPosition;
    private float timeElapsed = 0f;
    private bool isHandNew = true;
    private Hand_Manager handManager;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;

        print(transform.position);
        nail_collectionInstance = Instantiate(nail_collectionPrefab, new Vector3(1000f, 1000f, 1000f), Quaternion.identity); //On instancie le prefab de nails pour qu'il soit dans la scene
        
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();

        renderer.color = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f); //couleur aleatoire de la main

        getChildInNailCollection();
        InstantiateNailsAtLocators();

        handManager = GetComponentInParent<Hand_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        
        //Si nouvelle main, alors on la fait descendre
        if(isHandNew)
        {
            HandEnterOrLeave(centerPosition);
        }

        //Si main terminee, on la fait sortir
        if (isHandDone)
        {
            HandEnterOrLeave(originalPosition);            
        }
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
        isHandDone = true;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name); // loads current scene
    }


    public void HandEnterOrLeave(Vector3 positionGoal)
    {
        
        if(Mathf.Abs(transform.position.y-centerPosition.y)<0.01 && isHandNew)
        {
            transform.position = centerPosition;
            isHandNew = false;
        }
        if (Mathf.Abs(transform.position.y - originalPosition.y) < 0.05 && !isHandNew)
        {
            Destroy(gameObject);
            handManager.NewHand();
        }

        timeElapsed = 0f;
        if (timeElapsed < springDuration)
        {
            timeElapsed += Time.deltaTime;
            float t = Mathf.Clamp01(timeElapsed / springDuration);

            // Apply a spring effect using a sine wave for smoothness
            // Overshoot the target position for that "spring" effect
            float dropValue = Mathf.Sin(t * Mathf.PI * 0.5f);  // Sin function for smooth bounce
            float overshootFactor = Mathf.Sin(t * Mathf.PI * 1.5f); // Slight overshoot effect
            Vector3 newPos = Vector3.Lerp(transform.position, positionGoal, dropValue + overshootFactor);

            transform.position = newPos;
        }
    }

}
