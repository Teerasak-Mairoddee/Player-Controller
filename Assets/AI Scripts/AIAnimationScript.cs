using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnimationScript : MonoBehaviour
{
    private Animator animator;
    public float waitForNextAttack = 3f;
    public float attackDelay = 0.5f;

    public float minDelay = 2f; // Minimum delay in seconds
    public float maxDelay = 5f; // Maximum delay in seconds

    public float swordHitboxDelay = 0.2f; // Minimum delay in seconds
    public float swordHitboxStayEnabledTime = 0.8f; // Maximum delay in seconds

    private EnemySwordHitboxScript EnemySwordHitboxScriptReference;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isTelegraphing", false);
        animator.SetBool("isAttacking", false); 
        StartCoroutine(TriggerHitboxTiming());

        EnemySwordHitboxScriptReference = GetComponentInChildren<EnemySwordHitboxScript>();

        if (EnemySwordHitboxScriptReference != null)
        {
            // Call the function from HitboxTimings
            EnemySwordHitboxScriptReference.FunctionToCall();
            //EnemySwordHitboxScriptReference.DisableHitBoxSwitch();
        }
        else
        {
            Debug.LogWarning("HitboxTimingsReference reference not found in child GameObject.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }



        
    

    IEnumerator TriggerHitboxTiming()
    {
        while (true)
        {
            // Wait for a random delay between minDelay and maxDelay
            float randomDelay = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(randomDelay);

            // Trigger the animation
            // Code before the delay
            yield return new WaitForSeconds(randomDelay);
            animator.SetBool("isTelegraphing", true);
            animator.SetBool("isAttacking", false);
            yield return new WaitForSeconds(attackDelay); // Wait for 2 seconds
            EnemySwordHitboxScriptReference.TriggerHitBox(swordHitboxDelay,swordHitboxStayEnabledTime);
            animator.SetBool("isTelegraphing", false);
            animator.SetBool("isAttacking", true);
            yield return new WaitForSeconds(attackDelay);
  
            animator.SetBool("isTelegraphing", false);
            animator.SetBool("isAttacking", false);
            // Code after the delay
        }

    }

    //Test Function
    public void FunctionToCall()
    {
        Debug.Log("Function in AIAnimationScript called.");
    }

    public void Hurt() {
        animator.SetTrigger("Hurt");
    }
}
