using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHit : MonoBehaviour
{
    [SerializeField] private Transform target;
    
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Playermovement.ableToFly = false;
    }

}
