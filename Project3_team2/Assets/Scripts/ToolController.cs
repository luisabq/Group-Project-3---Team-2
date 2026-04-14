using UnityEngine;

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
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentTool = Tool.Steam;
            Debug.Log("Steam Equipped");
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
