using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Alien : MonoBehaviour
{
    public GameObject handManager;

    public bool isMoving = false;
    private bool canHand = true;
    private Vector3 targetPosition;
    public float targetPositionX;

    public int assignedSlot = 0;



    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Mathf.Abs(targetPositionX - transform.position.x) >= 0.2) //Si la position actuelle et la position target sont trop loin, on avance
        {
            isMoving = true;
        }
        else if (canHand)
        {
            if (assignedSlot != 0)
            {
                SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                GiveHand(transform.position.x, spriteRenderer.color);
            }

            isMoving = false;
        }


        if (isMoving)
        {
            //Debug.Log(name + " is moving to " + targetPositionX);

            targetPosition = new Vector3(targetPositionX, transform.position.y + SineAmount(), transform.position.z); //On lui donne la prochaine position


            Vector3 newPos = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);
            transform.position = newPos;
        }
        /*
        if (Mathf.Abs(transform.position.x - targetPositionX) < 0.2 && isMoving) //Si alien atteint la position voulue
        {
            isMoving = false;

            if (Mathf.Abs(transform.position.x) <= 0.2) //Si alien est au centre A MODIFIER PAR SLOT POSITIONS
            {
                GiveHand();
                print("alien au centre");
            }
        }
        */
    }


    public void GiveHand(float positionX, UnityEngine.Color colorAlien)
    {
        canHand = false;
        Hand_Manager handManagerScript = handManager.GetComponent<Hand_Manager>();
        handManagerScript.canSpawnHand = true;
        handManagerScript.NewHand(positionX, assignedSlot, colorAlien);
        //Mettre code de l'animation de tendage de main
    }

    public float SineAmount()
    {
        return Mathf.Sin(Time.time * 10);
    }

    
 }

