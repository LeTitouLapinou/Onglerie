using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    public GameObject handManager;

    private bool isMoving;
    private bool isHanding;
    private Vector3 targetPosition;


    // Start is called before the first frame update
    void Start()
    {
        isMoving = true;
        isHanding = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            targetPosition = new Vector3(0, transform.position.y + SineAmount(), transform.position.z);

            Vector3 newPos = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);
            transform.position = newPos;
        }

        if (Mathf.Abs(transform.position.x - targetPosition.x) < 0.2 && isMoving) //Si alien atteint la position voulue
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

}
