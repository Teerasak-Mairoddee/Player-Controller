using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTimings : MonoBehaviour
{
    private Collider2D hitboxCollider; // Reference to the Collider component

    // Start is called before the first frame update
    void Start()
    {
        // Get the Collider component
        hitboxCollider = GetComponent<Collider2D>();

        // Disable the collider initially
        hitboxCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DisableHitBoxSwitch()
    {
        hitboxCollider.enabled = false;
    }


    public void EnableHitBoxSwitch()
    {
        hitboxCollider.enabled = true;
    }



    //Test Function
    public void FunctionToCall()
    {
        Debug.Log("Function in Jump Script called.");
    }
}
