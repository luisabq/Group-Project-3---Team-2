using UnityEngine;

public class ReticleController : MonoBehaviour
{
    public GameObject reticle;
    public PlayerToolController toolController;

    void Update()
    {
        if (reticle == null || toolController == null) return;

        float lt = Input.GetAxis("LT");

        bool isAiming = Input.GetMouseButton(1) || lt > 0.1f;

        bool isGrapple = toolController.currentTool == PlayerToolController.Tool.Grapple;


        //take out is grapple if you want reticle for steam too
        reticle.SetActive(isAiming && isGrapple);
    }
}