using UnityEngine;

public class SkyboxColorOverTime : MonoBehaviour
{
    public Color startColor = Color.blue;
    public Color endColor = Color.black;
    public float speed = 0.5f;

    private Material skyboxMat;

    void Start()
    {
       
        skyboxMat = RenderSettings.skybox;
    }

    void Update()
    {
        float t = Mathf.PingPong(Time.time * speed, 1f);
        Color current = Color.Lerp(startColor, endColor, t);

        
     
            skyboxMat.SetColor("TopColor", current);
     
       
    }
}