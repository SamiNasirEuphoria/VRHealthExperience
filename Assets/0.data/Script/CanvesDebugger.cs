using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CanvesDebugger : MonoBehaviour
{
    public Text debugText; // Reference to the Text element in the Canvas

    public GameObject[] gameObjects; // Array of GameObjects to monitor
    public string[] objectNames; // Array of names corresponding to the GameObjects


    void Start()
    {
        StartCoroutine(Wait());   
    }
    public void PrintValues()
    {
        for (int i=0; i< gameObjects.Length; i++)
        {
            debugText.text += objectNames[i] + " " + gameObjects[i].transform.position + "\n"+ gameObjects[i].transform.rotation;
        }
    }
    IEnumerator Wait()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.5f);
            PrintValues();
        }
    }
   
}
