using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{

    public Alien_Manager alienManager;

    public void RestartGame()
    {
        alienManager.isSlot03Free = true;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name); // loads current scene
    }
}
