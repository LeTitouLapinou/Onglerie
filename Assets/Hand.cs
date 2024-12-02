using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hand : MonoBehaviour
{
    public GameObject nail_collectionPrefab;

    public int clean_nails_number = 0;
    public Transform[] locators;

    public Vector3 onScreenPosition; // Position for the hand when on screen
    private GameObject[] nailList;
    private GameObject nail_collectionInstance;

    private bool isHandDone = false;
    private Vector3 originalPosition;
    private Hand_Manager handManager;

    public Color color;
    public int handSlot = 0;

    Quaternion targetAngle;
    Vector3 targetPosition;
    public float sineSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;

        nail_collectionInstance = Instantiate(nail_collectionPrefab, new Vector3(1000f, 1000f, 1000f), Quaternion.identity); // Instantiate nails in the scene

        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.color = color; // Color set by Hand_Manager

        getChildInNailCollection();
        InstantiateNailsAtLocators();

        handManager = GetComponentInParent<Hand_Manager>();

        InvokeRepeating("ChangeTargetAngle", 0, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        // Decide on the target position based on hand state
        Vector3 targetPos = isHandDone ? originalPosition + new Vector3(0, 5, 0) : originalPosition + new Vector3(0, -8, 0); //le +5 c'est pour sortir plus vite

        // Move the hand towards the target position
        ApplyLerpMovement(targetPos);

        // If the hand is at its goal and it's done, destroy it
        if (isHandDone && Mathf.Abs(transform.position.y - originalPosition.y) < 1f)
        {
            DestroyHand();
        }

        // Apply oscillation if hand is not done
        if (!isHandDone)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetAngle, Time.deltaTime);
            targetPosition = new Vector3(SineAmount().x, SineAmount().y + 3, 0);
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);
        }
    }

    public void getChildInNailCollection()
    {
        nailList = new GameObject[nail_collectionInstance.transform.childCount]; // Get the nails from the prefab

        for (int i = 0; i < nail_collectionInstance.transform.childCount; i++)
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
        for (int i = 0; i < locators.Length; i++)
        {
            GameObject nailInstance = Instantiate(nailList[Random.Range(0, nailList.Length)], locators[i].position, locators[i].rotation, this.transform);
        }
    }

    public void OneMoreCleanNail()
    {
        clean_nails_number++;

        if (clean_nails_number == locators.Length)
        {
            HandDone();
        }
    }

    public void HandDone()
    {
        isHandDone = true;
    }

    private void DestroyHand()
    {
        Destroy(gameObject);
        Destroy(nail_collectionInstance);
        handManager.isAlienDone = true;
        handManager.alienManager.AlienLeaves(handSlot); // Notify alien manager
    }

    private void ApplyLerpMovement(Vector3 positionGoal)
    {
        // Use Lerp to move the hand smoothly to the target position without overshoot
        transform.position = Vector3.Lerp(transform.position, positionGoal, Time.deltaTime * 0.5f); // Adjust speed as needed
    }

    void ChangeTargetAngle()
    {
        float curve = Mathf.Sin(Random.Range(0, Mathf.PI * 2));
        targetAngle = Quaternion.Euler(0, 0, curve * 10f + 180); // Apply random rotation to add some movement
    }

    public Vector2 SineAmount()
    {
        return new Vector2(Mathf.Sin(Time.time * sineSpeed), Mathf.Cos(Time.time * sineSpeed) * 0.5f); // Create oscillations
    }
}
