using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GazeManager : MonoBehaviour {

    private Ray ray;
    private RaycastHit hitInfo;
    public Image loadingCircle;
    float currentFillAmount;
    public float loadingSpeed = 1.0f;
	void Start () {
        loadingCircle.fillAmount = 0;
	}
	
	// Update is called once per frame
	void Update () {
        ray = new Ray(transform.position, gameObject.transform.forward);
        Debug.DrawRay(transform.position, gameObject.transform.forward * 100, Color.yellow);
        if (Physics.Raycast(ray, out hitInfo))
        {

            if (hitInfo.collider.tag == "GazeObject")
            {
                //Debug.Log ("Acute Cude Detected");
                currentFillAmount = currentFillAmount + Time.deltaTime * loadingSpeed;
                loadingCircle.fillAmount = currentFillAmount;
                if (loadingCircle.fillAmount >= 0.99f)
                {
                    hitInfo.collider.gameObject.GetComponent<GazeEvent>().OnGazeComplete();
                }
            }

        }
        else
        {
            currentFillAmount = 0;
            loadingCircle.fillAmount = 0;
        }
	}
}
