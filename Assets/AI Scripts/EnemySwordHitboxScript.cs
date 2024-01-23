using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwordHitboxScript : MonoBehaviour
{
    private Collider2D hitboxCollider; // Reference to the Collider component
    // Start is called before the first frame update
    void Start()
    {
        // Get the Collider component
        hitboxCollider = GetComponent<Collider2D>();
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

    public void TriggerHitBox(float Delay, float stayEnabledTime) {
        StartCoroutine(TriggerHitboxTiming(Delay, stayEnabledTime));
    }

    IEnumerator TriggerHitboxTiming(float Delay, float stayEnabledTime ) 
    {
        yield return new WaitForSeconds(Delay);
        hitboxCollider.enabled = true;
        yield return new WaitForSeconds(stayEnabledTime);
        hitboxCollider.enabled = false;

    }

    public void FunctionToCall()
    {
        Debug.Log("Function in EnemySwordHitboxScript called.");
    }

}
