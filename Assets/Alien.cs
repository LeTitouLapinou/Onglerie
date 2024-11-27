using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    public GameObject handManager;

    public bool isMoving = false;
    private bool isHanding;
    private Vector3 targetPosition;
    public float targetPositionX;

    private bool isSlot01Free = false;
    private bool isSlot02Free = true;
    private bool isSlot03Free = false;


    // Start is called before the first frame update
    void Start()
    {
        
        isHanding = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isMoving)
        {
            Debug.Log(name + "is moving");

            targetPosition = new Vector3(targetPositionX, transform.position.y + SineAmount(), transform.position.z); //On lui donne la prochaine position


            Vector3 newPos = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);
            transform.position = newPos;
        }

        if (Mathf.Abs(transform.position.x - targetPositionX) < 0.2 && isMoving) //Si alien atteint la position voulue
        {
            isMoving = false;

            if (Mathf.Abs(transform.position.x) <= 0.2) //Si alien est au centre
            {
                GiveHand();
                print("alien au centre");
            }
        }
    }


    public void GiveHand()
    {
        isHanding = true;
        Hand_Manager handManagerScript = handManager.GetComponent<Hand_Manager>();
        handManagerScript.isAlienHanding = true;
        //Mettre code de l'animation de tendage de main
    }

    public float SineAmount()
    {
        return Mathf.Sin(Time.time * 10);
    }

    public void slotSelection(int slotNumber)
    {
        if (slotNumber == 1)
        {

        }
    }
}
