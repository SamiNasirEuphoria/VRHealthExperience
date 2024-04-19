using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GazeEvent : MonoBehaviour {
    public UnityEvent OnGaze;

    public void OnGazeComplete () {
        OnGaze.Invoke();
	}
}
