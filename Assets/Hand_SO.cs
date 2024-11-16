using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New hand", menuName = "Hand")]
public class Hand_SO : ScriptableObject
{
    public GameObject nailPrefab;
    public Vector3[] nailLocations;

    public Sprite sprite;

    public Sprite nail_particle;

    public int nbFingers;
    public bool isMoving;
    public bool isClean;
    
}
