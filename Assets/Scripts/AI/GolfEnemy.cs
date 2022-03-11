using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfEnemy : MonoBehaviour
{
    [SerializeField] Transform target;

    [SerializeField] float maxForce = 5;
    [SerializeField] float jumpForce = 5;
    [SerializeField] ForceMode forceMode;

    Rigidbody rb;
    Vector3 force = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        float health = GetComponent<Health>().health;
        Game.Instance.gameData.Load("EnemyHealth", ref health);

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Vector3.zero;

        force = maxForce * direction;

        Game.Instance.gameData.Save("EnemyHealth", GetComponent<Health>().health);
    }

    private void FixedUpdate()
    {
        rb.AddForce(force, forceMode);
    }

    public void Destroyed()
    {
        Game.Instance.OnEnemyDead();
    }
}
