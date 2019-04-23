using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostOrbController : MonoBehaviour {
    public float lowSpeed, highSpeed, currentSpeed, colorSpeed;
    public GameObject centerPoint;
    public Vector3 axis;
    public Color baseColor;

    private Renderer Orbrenderer;
    private Material OrbMaterial;
    private ParticleSystem ps;
    
    // Use this for initialization

    void Start() {
        Orbrenderer = GetComponent<Renderer>();
        OrbMaterial = Orbrenderer.material;
        baseColor = Color.red;
        ps = GetComponent<ParticleSystem>();
        currentSpeed = highSpeed;
    }

    // Update is called once per frame
    void Update() {
        float floor = 0.5f;
        float ceiling = 0.8f;
        float emission = Mathf.PingPong(Time.time * colorSpeed, ceiling - floor);

        Color finalColor = baseColor * Mathf.LinearToGammaSpace(emission);

        OrbMaterial.SetColor("_EmissionColor", finalColor);
        var main = ps.main;
        main.startColor = finalColor;
        transform.RotateAround(centerPoint.transform.position, axis, currentSpeed * Time.deltaTime);
    }

    public void SetSpeed(string speed)
    {
        if(speed == "High")
        {
            currentSpeed = highSpeed;
        }
        if(speed == "Low")
        {
            currentSpeed = lowSpeed;
        }
    }
}
