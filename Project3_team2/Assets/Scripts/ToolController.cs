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
        //number keys
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.JoystickButton4))
        {
            SetTool(Tool.Grapple);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.JoystickButton5))
        {
            SetTool(Tool.Steam);
        }

        //mouse wheel
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll > 0f)
        {
            CycleTool(1);
        }
        else if (scroll < 0f)
        {
            CycleTool(-1);
        }
    }

    void CycleTool(int direction)
    {
        int toolCount = System.Enum.GetValues(typeof(Tool)).Length;
        int newIndex = ((int)currentTool + direction + toolCount) % toolCount;

        SetTool((Tool)newIndex);
    }

    void SetTool(Tool newTool)
    {
        if (currentTool == newTool) return;

        currentTool = newTool;

        armSwitch.Play();

        switch (currentTool)
        {
            case Tool.Grapple:
                Debug.Log("Grapple Equipped");
                grappleSwitch.Play();
                break;

            case Tool.Steam:
                Debug.Log("Steam Equipped");
                steamSwitch.Play();
                break;
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