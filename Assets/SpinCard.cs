using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinCard : MonoBehaviour
{
    // make the card spin in place
    [SerializeField] float speed = 3.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(transform.up, speed * Time.deltaTime);
    }
}
