using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEvent : MonoBehaviour {

    public GameObject staticCanvas;
    public Animation staticAnimation;

    private void Start()
    {
        staticCanvas.SetActive(false);
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
        staticCanvas.SetActive(true);
        yield return new WaitForSeconds(1f);
        staticCanvas.SetActive(false);
    }
}
