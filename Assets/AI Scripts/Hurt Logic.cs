using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtLogic : MonoBehaviour
{
    private Collider2D hitboxCollider; // Reference to the Collider component
    private AIAnimationScript AIAnimationScriptReference;
    // Start is called before the first frame update
    void Start()
    {
        AIAnimationScriptReference = GetComponentInParent<AIAnimationScript>();
        if (AIAnimationScriptReference != null)
        {
            // Call the function from HitboxTimings
            AIAnimationScriptReference.FunctionToCall();
        }
        else
        {
            Debug.LogWarning("AIAnimationScriptReference reference not found in child GameObject in Hurt Logic.");
        }
    }

    // Update is called once per frame
    void Update()
    {


    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("SwordHitbox"))
        {
            // Perform actions based on the tag
            Debug.Log("Hitbox crossed an object with the tag "+ otherCollider.tag);
            AIAnimationScriptReference.Hurt();
        }
    }
}
