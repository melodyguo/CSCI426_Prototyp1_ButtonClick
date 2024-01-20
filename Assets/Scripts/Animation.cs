using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    public GameObject buttonCap; // Assign this in the inspector or find it dynamically
    public float moveDistance = 1.0f; // Distance to move buttonCap
    public bool triggerBool = false; // External trigger

    private Vector3 originalPosition;
    private Coroutine moveCoroutine;

    void Start()
    {
        if (buttonCap == null)
        {
            buttonCap = transform.Find("buttonCap").gameObject;
        }
        originalPosition = buttonCap.transform.position;
    }

    void Update()
    {
        // Check for space key, mouse button or the boolean trigger
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || triggerBool)
        {
            MoveButtonCap(true);
        }
        else if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0) || !triggerBool)
        {
            MoveButtonCap(false);
        }
    }

    void MoveButtonCap(bool downwards)
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }

        Vector3 targetPosition = originalPosition + (downwards ? -Vector3.up * moveDistance : Vector3.zero);
        moveCoroutine = StartCoroutine(MoveOverSeconds(buttonCap, targetPosition, 0.2f));
    }

    IEnumerator MoveOverSeconds(GameObject objectToMove, Vector3 end, float seconds)
    {
        float elapsedTime = 0;
        Vector3 startingPos = objectToMove.transform.position;

        while (elapsedTime < seconds)
        {
            objectToMove.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        objectToMove.transform.position = end;
    }
}