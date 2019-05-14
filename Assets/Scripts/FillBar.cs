using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillBar : MonoBehaviour {

    public Image fillBar;
    private ProgCrashEvent crashEvent;

	// Use this for initialization
	void Start () {
        fillBar = GetComponent<Image>();
        fillBar.fillAmount = 0f;
        crashEvent = GetComponentInParent<ProgCrashEvent>();
    }
	
	public void startFixingProcess()
    {
        StartCoroutine(fillTheBar());
    }

    IEnumerator fillTheBar()
    {
        while(fillBar.fillAmount < 1f)
        {
            fillBar.fillAmount = fillBar.fillAmount + 600f * Time.deltaTime;
            Debug.Log(fillBar.fillAmount);
            yield return new WaitForSeconds(0.0001f);
        }

        yield return new WaitForSeconds(0.03f);

        crashEvent.disableCrashSequence();
    }
}
