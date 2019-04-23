using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostOrbController : MonoBehaviour {
    public float speed, colorSpeed;
    public GameObject centerPoint;
    public Vector3 axis;
    public Color baseColor;
    private Renderer Orbrenderer;
    private Material OrbMaterial;
    // Use this for initialization

    void Start() {
        Orbrenderer = GetComponent<Renderer>();
        OrbMaterial = Orbrenderer.material;
        baseColor = Color.red;
    }

    // Update is called once per frame
    void Update() {
        float floor = 0.3f;
        float ceiling = 1.0f;
        float emission = Mathf.PingPong(Time.time * colorSpeed, ceiling - floor);

        Color finalColor = baseColor * Mathf.LinearToGammaSpace(emission);

        OrbMaterial.SetColor("_EmissionColor", finalColor);
        transform.RotateAround(centerPoint.transform.position, axis, speed * Time.deltaTime);
    }
}
