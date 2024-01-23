using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HitboxTimings : MonoBehaviour


{

    private Collider2D hitboxCollider; // Reference to the Collider component
    private AttackControls AttackControlsReference;

    // Start is called before the first frame update
    void Start()
    {
        AttackControlsReference = GetComponentInParent<AttackControls>();
        if (AttackControlsReference != null)
        {
            // Call the function from HitboxTimings
            AttackControlsReference.FunctionToCall();
        }
        else
        {
            Debug.LogWarning("AttackControlsReference reference not found in child GameObject.");
        }


        // Get the Collider component
        hitboxCollider = GetComponent<Collider2D>();

        // Disable the collider initially
        hitboxCollider.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableHitbox(float timing)
    {
        
        StartCoroutine(TriggerHitboxTiming(timing));
    }

    IEnumerator TriggerHitboxTiming(float timing)
    {
        // Code before the delay
        hitboxCollider.enabled = true;
        yield return new WaitForSeconds(timing); // Wait for 2 seconds
        hitboxCollider.enabled = false;
        // Code after the delay
    }

    //Test Function
    public void FunctionToCall()
    {
        Debug.Log("Function in Hitbox Script called.");
    }

}
