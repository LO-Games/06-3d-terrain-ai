using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] bool Card_Door = false;

    // Update is called once per frame
    void Update()
    {
        CheckCollision();
    }

    void CheckCollision()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1f); // Change the radius as per your requirement

        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Card")
            {
                Card_Door = true;
                Destroy(collider.gameObject);
                break; // Exit the loop since we found a card
            }
        }
    }

    // Return if player has card
    public bool HasCard()
    {
        return Card_Door;
    }
}