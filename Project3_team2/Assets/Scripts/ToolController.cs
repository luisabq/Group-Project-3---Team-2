using UnityEngine;
using System.Collections;


public class PlayerToolController : MonoBehaviour
{


    public enum Tool
    {
        Grapple,
        Steam
    }

    public Tool currentTool;

    public GrappleSystem grappleSystem;
    public SteamGun steamGun;

    private bool isAiming;

    public AudioSource armSwitch;
    public AudioSource steamSwitch;
    public AudioSource grappleSwitch;


    void Update()
    {
        HandleSwitch();
        HandleAim();
        HandleFire();
    }

    void HandleSwitch()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentTool = Tool.Grapple;
            Debug.Log("Grapple Equipped");
            armSwitch.Play();
            grappleSwitch.Play();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentTool = Tool.Steam;
            Debug.Log("Steam Equipped");
            armSwitch.Play();
            steamSwitch.Play();
        }
    }

    void HandleAim()
    {
        isAiming = Input.GetMouseButton(1);
    }

    void HandleFire()
    {
        if (isAiming && Input.GetMouseButtonDown(0))
        {
            if (currentTool == Tool.Grapple)
            {
                grappleSystem.TryGrapple();
            }

            if (currentTool == Tool.Steam)
            {
                steamGun.Fire();
            }
        }

        if (currentTool == Tool.Steam && Input.GetMouseButtonUp(0))
        {
            steamGun.StopFire();
        }
    }
}
