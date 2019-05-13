using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEvent : MonoBehaviour {

    public GameObject staticCanvas;
    public Animation staticAnimation;

    private void Start()
    {
        StartCoroutine(playStaticEvent());
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            staticCanvas.SetActive(true);
            StartCoroutine(playStaticEvent());
        }
    }

    IEnumerator playStaticEvent()
    {
        yield return new WaitForSeconds(1f);
        staticCanvas.SetActive(false);
    }
}
