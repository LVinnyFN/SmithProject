using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject doorFrame;
    public Vector3 closedSpot, openOffset;
    public bool isOpen;
    public bool isMoving;
    public float openPercentage;
    public float openingDuration;

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Open());
    }
    private void OnTriggerExit(Collider other)
    {
        StartCoroutine(Close());
    }

    private IEnumerator Open()
    {
        float counter = openingDuration * openPercentage;
        isMoving = true;
        while (counter < openingDuration)
        {
            Debug.Log("Opening");
            counter += Time.deltaTime;
            openPercentage = counter / openingDuration;
            doorFrame.transform.localPosition = Vector3.Lerp(closedSpot, closedSpot + openOffset, openPercentage);
            yield return null;
        }
        openPercentage = Mathf.Min(openPercentage, 1);
        isMoving = false;
        isOpen = true;
    }

    private IEnumerator Close()
    {
        float counter = openingDuration - openingDuration * openPercentage;
        isMoving = true;
        while (counter > 0)
        {
            Debug.Log("Closing");
            counter -= Time.deltaTime;
            openPercentage = counter / openingDuration;
            doorFrame.transform.localPosition = Vector3.Lerp(closedSpot + openOffset, closedSpot, openPercentage);
            yield return null;
        }
        openPercentage = Mathf.Max(openPercentage, 0);
        isMoving = false;
        isOpen = false;
    }

    private void OnValidate()
    {
        doorFrame.transform.localPosition = isOpen ? closedSpot + openOffset : closedSpot;
    }
}
