using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class ObjectSpinner : MonoBehaviour
{
    public float rotationSpeed = 100f;

    private Transform parentTransform;

    private void Start()
    {
        // Create an empty parent object
        GameObject parentObject = new GameObject("ParentObject");
        parentObject.transform.position = transform.position;
        parentTransform = parentObject.transform;

        // Make the current object a child of the parent object
        transform.SetParent(parentTransform);

        // Start the rotation coroutine
        StartCoroutine(SpinObject());
    }

    private IEnumerator SpinObject()
    {
        while (true)
        {
            // Rotate the parent object around its local up direction
            parentTransform.Rotate(parentTransform.up, rotationSpeed * Time.deltaTime);

            // Wait for the next frame
            yield return null;
        }
    }
}