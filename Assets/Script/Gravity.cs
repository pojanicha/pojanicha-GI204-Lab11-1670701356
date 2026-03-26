using UnityEngine;
using System.Collections.Generic;

public class Gravity : MonoBehaviour
{
    public static List<Gravity> otherObj;
    private Rigidbody rb;
    const float G = 667f;

    [SerializeField] bool planet = false;
    [SerializeField] int orbitSpeed = 1000;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        
        rb = GetComponent<Rigidbody>();
        if (otherObj == null)
        { 
            otherObj = new List<Gravity>();
        }
        otherObj.Add(this);

        if (!planet)
        {
            rb.AddForce(Vector3.left * orbitSpeed);
        
        
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (Gravity obj in otherObj)
        {
            if (obj != this)
            {
                Attract(obj);
            }
        }
    }

    void Attract(Gravity other)
    {
        Rigidbody otherRb = other.rb;
        Vector3 direction = rb.position - otherRb.position;

        float distance = direction.magnitude;
        if (distance == 0f) return;


        float forceMagnitude = G * (rb.mass * otherRb.mass) / Mathf.Pow(distance, 2);
        Vector3 gravitationForce = forceMagnitude * direction.normalized;
        otherRb.AddForce(gravitationForce);

    
    }






}
