using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject);
    }
}
