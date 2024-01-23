using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryTimings : MonoBehaviour
{
    private Collider2D hitboxCollider; // Reference to the Collider component
    private DodgeTimings DodgeTimingsReference;
    private AttackControls AttackControlsReference;
    // Start is called before the first frame update
    void Start()
    {
        // Get the Collider component
        hitboxCollider = GetComponent<Collider2D>();
        // Get the parent GameObject using Transform.parent
        Transform parentTransform = transform.parent;

        //Get Dependant Code
        if (parentTransform != null)
        {
            GameObject parentObject = parentTransform.gameObject;
            Debug.Log("Parent GameObject: " + parentObject.name);

            DodgeTimingsReference = parentObject.GetComponentInChildren<DodgeTimings>();
            if (DodgeTimingsReference != null)
            {
                // Call the function from HitboxTimings
                DodgeTimingsReference.FunctionToCall();
            }
            else
            {
                Debug.LogWarning("DodgeTimingsReference reference not found (msg from Parry Timings).");
            }

            // Disable the collider initially
            hitboxCollider.enabled = false;
        }
        else
        {
            Debug.Log("No parent GameObject found.");
        }

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
            Debug.LogWarning("AttackControlsReference reference not found in child GameObject (msg from Parry Timings).");
        }

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
        DodgeTimingsReference.DisableHitBoxSwitch();
        yield return new WaitForSeconds(timing); // Wait for 2 seconds
        hitboxCollider.enabled = false;
        DodgeTimingsReference.EnableHitBoxSwitch();
        // Code after the delay
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("EnemySwordHitbox"))
        {
            // Perform actions based on the tag
            Debug.Log("Hitbox crossed an object with the tag " + otherCollider.tag);
            AttackControlsReference.Parry();
        }
    }

    //Test Function
    public void FunctionToCall()
    {
        Debug.Log("Function in Parry Script called.");
    }
}
