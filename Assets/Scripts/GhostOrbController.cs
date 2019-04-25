using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostOrbController : MonoBehaviour
{

    public float lowSpeed, highSpeed, currentSpeed, colorSpeed;
    public GameObject centerPoint;
    public Vector3 axis;
    public Color baseColor;

    //References
    private Renderer Orbrenderer;
    private Material OrbMaterial;
    private ParticleSystem ps;

    // Use this for initialization

    void Start()
    {
        Orbrenderer = GetComponent<Renderer>();
        OrbMaterial = Orbrenderer.material;
        baseColor = Color.red;
        ps = GetComponent<ParticleSystem>();
        currentSpeed = highSpeed;
    }

    // Update is called once per frame
    void Update()
    {

        //Math to cause the orbs color to pulse
        float floor = 0.5f;
        float ceiling = 0.8f;
        float emission = Mathf.PingPong(Time.time * colorSpeed, ceiling - floor);

        Color finalColor = baseColor * Mathf.LinearToGammaSpace(emission);

        OrbMaterial.SetColor("_EmissionColor", finalColor); //Sets the orb to the color calculated
        var main = ps.main; // Get a reference to the particle system
        main.startColor = finalColor; // set particle colors
        transform.RotateAround(centerPoint.transform.position, axis, currentSpeed * Time.deltaTime); // Causes orbs to rotate the center point
    }

    public void SetSpeed(string speed) // Function to allow other scripts to change the speed of ghost animations
    {
        if (speed == "High")
        {
            currentSpeed = highSpeed;
        }
        if (speed == "Low")
        {
            currentSpeed = lowSpeed;
        }
    }
}
