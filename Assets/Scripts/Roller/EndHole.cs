using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EndHole : MonoBehaviour
{
    public Game gameManager;

    private void OnTriggerEnter(Collider other)
    {
        //gameManager.OnEndGame();
    }
}
