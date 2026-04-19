using UnityEngine;

public class ReticleController : MonoBehaviour
{
    public GameObject reticle;
    public PlayerToolController toolController;

    void Update()
    {
        if (reticle == null || toolController == null) return;

        bool isAiming = Input.GetMouseButton(1);

        bool isGrapple = toolController.currentTool == PlayerToolController.Tool.Grapple;


        //take out is grapple if you want reticle for steam too
        reticle.SetActive(isAiming && isGrapple);
    }
}