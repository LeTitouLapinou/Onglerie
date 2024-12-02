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
    public bool isAlienHanding = false;
    public Color color;

    public int handSlot = 0;

    Quaternion targetAngle;
    Vector3 targetPosition;
    public float WaitBetweenWobbles = 1;
    public float sineSpeed = 1;


    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        centerPosition = new Vector3(originalPosition.x, originalPosition.y - 3, originalPosition.z);


        nail_collectionInstance = Instantiate(nail_collectionPrefab, new Vector3(1000f, 1000f, 1000f), Quaternion.identity); //On instancie le prefab de nails pour qu'il soit dans la scene

        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.color = color; //couleur aleatoire definie dans le script Alien, recuperee par Hand Manager

        getChildInNailCollection();
        InstantiateNailsAtLocators();

        handManager = GetComponentInParent<Hand_Manager>();

        InvokeRepeating("ChangeTargetAngle", 0, 2f);
        
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
            HandEnterOrLeave(originalPosition + new Vector3(0,5,0));    
        }

        if(!isHandDone)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetAngle, Time.deltaTime);
            targetPosition = new Vector3(SineAmount().x, SineAmount().y + 3, 0);
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);
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
        
        if(Mathf.Abs(transform.position.y-positionGoal.y)<0.05 && isHandNew)
        {
            transform.position = positionGoal;
            isHandNew = false;
        }
        
        if (Mathf.Abs(transform.position.y - originalPosition.y) < 1 && isHandDone) //si la main est sortie, on la detruit
        {
            
            Destroy(gameObject);
            Destroy(nail_collectionInstance);
            handManager.isAlienDone = true;

            Debug.Log(handSlot);

            handManager.alienManager.AlienLeaves(handSlot); //Call de la fonction qui fait partir le alien

            //SI ON VEUT UNE NOUVELLE MAIN
            //handManager.NewHand();
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

    void ChangeTargetAngle()
    {
        print("angle change");
        float curve = Mathf.Sin(Random.Range(0, Mathf.PI * 2));
        targetAngle = Quaternion.Euler(0, 0, curve * 10f + 180); // Apply some random rotation within a range
        
    }

    public Vector2 SineAmount()
    {
        return new Vector2(Mathf.Sin(Time.time * sineSpeed), Mathf.Cos(Time.time * sineSpeed) * 0.5f);
    }

}
