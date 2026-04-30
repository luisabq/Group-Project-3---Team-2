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

    [Header("Steam System")]
    public PlayerMovement playerMovement; 
    public int steamRequirement;
    public bool autoDeactivate;
    private bool deactivated = false;

    public GameObject portalTexture; 

    private AudioSource teleportSound;





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

        if (steamRequirement <= playerMovement.activeSteamReceptors && !deactivated)
        {
            Debug.Log("Steam req met!");
            StartCoroutine(Teleport(rb));
            
        }
        else
        {
            Debug.Log("Steam req not met D:");
            portalTexture.SetActive(false);
        }
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

        teleportSound.Play();

        rb.WakeUp();

      
        yield return new WaitForSeconds(0.1f);

      
        ignoreTrigger = false;
        linkedPortal.ignoreTrigger = false;

       
        yield return new WaitForSeconds(cooldown);

        canUse = true;
        linkedPortal.canUse = true;

        if (autoDeactivate)
            deactivated = true;
        
        
        Destroy(linkedPortal);
        Destroy(this);


    }



    private void Start()
    {
        portalTexture.SetActive(false);
        
        teleportSound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (steamRequirement <= playerMovement.activeSteamReceptors && !deactivated)
            portalTexture.SetActive(true);

        if (playerMovement.activeSteamReceptors == 0)
            portalTexture.SetActive(false);

    }

}