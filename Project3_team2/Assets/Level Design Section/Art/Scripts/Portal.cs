using System.Collections;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [Header("Linked Portal")]
    public Portal linkedPortal;

    [Header("Cooldown")]
    public float cooldown = 1f;

    private bool ignoreTrigger;
    private bool canUse = true;

    private void OnTriggerEnter(Collider other)
    {
        if (ignoreTrigger || !canUse) return;

        Rigidbody rb = other.GetComponentInParent<Rigidbody>();
        if (rb == null) return;

        if (linkedPortal == null)
        {
            Debug.LogError("[Portal] Missing linked portal on " + name);
            return;
        }

        StartCoroutine(Teleport(rb));
    }

    private IEnumerator Teleport(Rigidbody rb)
    {
        
        ignoreTrigger = true;
        linkedPortal.ignoreTrigger = true;

        canUse = false;
        linkedPortal.canUse = false;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;


        Vector3 exitPos = linkedPortal.transform.position + linkedPortal.transform.forward;
        rb.position = exitPos;
        rb.rotation = linkedPortal.transform.rotation;

        rb.WakeUp();

      
        yield return new WaitForSeconds(0.1f);

      
        ignoreTrigger = false;
        linkedPortal.ignoreTrigger = false;

       
        yield return new WaitForSeconds(cooldown);

        canUse = true;
        linkedPortal.canUse = true;
    }
}