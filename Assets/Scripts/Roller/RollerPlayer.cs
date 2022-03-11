using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RollerPlayer : MonoBehaviour, IDestructable
{
    [SerializeField] float maxForce = 5;
    [SerializeField] float jumpForce = 5;
    [SerializeField] ForceMode forceMode;
    [SerializeField] Transform viewTransform;
    float sprintForce = 2;

    Rigidbody rb;
    Vector3 force = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        float health = GetComponent<Health>().health;
        Game.Instance.gameData.Load("Health", ref health);

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Vector3.zero;

        direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical");

        //convert spaceDirection to cameraSpace
        Quaternion viewSpace = Quaternion.AngleAxis(viewTransform.rotation.eulerAngles.y, Vector3.up);
        direction = viewSpace * direction;

        //worldSpace
        force = direction * maxForce;

        //viewSpace
        //force = viewSpace * force;

        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            //set IsGrounded() ???
        }

        //sprinting???

        Game.Instance.gameData.Save("Health", GetComponent<Health>().health);
    }

    //if consistently applied goes here
    private void FixedUpdate()
    {
        //the new transform position
        rb.AddForce(force, forceMode);

    }

    public void Destroyed()
    {
        Game.Instance.OnPlayerDead();
    }
}
