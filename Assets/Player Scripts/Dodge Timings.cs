using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeTimings : MonoBehaviour
{
    private Collider2D hitboxCollider; // Reference to the Collider component
    private AttackControls AttackControlsReference;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Collider component
        hitboxCollider = GetComponent<Collider2D>();
        AttackControlsReference = GetComponentInParent<AttackControls>();
        if (AttackControlsReference != null)
        {
            // Call the function from HitboxTimings
            AttackControlsReference.FunctionToCall();
        }
        else
        {
            Debug.LogWarning("AttackControlsReference reference not found in child GameObject (msg from Dodge Timings).");
        }

        // Disable the collider initially
        hitboxCollider.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DisableHitbox(float timing)
    {

        StartCoroutine(TriggerHitboxTiming(timing));
    }

    public void DisableHitBoxSwitch() 
    {
        hitboxCollider.enabled = false;
    }


    public void EnableHitBoxSwitch()
    {
        hitboxCollider.enabled = true;
    }



    IEnumerator TriggerHitboxTiming(float timing)
    {
        // Code before the delay
        hitboxCollider.enabled = false;
        Debug.Log("Remove Hitbox for Roll.");
        yield return new WaitForSeconds(timing); // Wait for 2 seconds
        hitboxCollider.enabled = true;
        // Code after the delay
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("EnemySwordHitbox"))
        {
            // Perform actions based on the tag
            Debug.Log("Hitbox crossed an object with the tag " + otherCollider.tag);
            AttackControlsReference.Hurt();
        }
    }


//Test Function
public void FunctionToCall()
    {
        Debug.Log("Function in Dodge Script called.");
    }
}
