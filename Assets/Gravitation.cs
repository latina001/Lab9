using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Gravitation : MonoBehaviour
{
    Rigidbody rb;
    const float G = 0.006674f;
    public static List<Gravitation> OtherObjectList;
    
    [SerializeField] bool planet = false;
    [SerializeField] int orbitSpeed = 1000;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (OtherObjectList == null)
        {OtherObjectList = new List<Gravitation>();}

        OtherObjectList.Add(this);

        if (!planet) 
        { rb.AddForce(Vector3.left * orbitSpeed); }
    }


    private void FixedUpdate()
    {
        foreach (Gravitation obj in OtherObjectList) 
        {
            if (obj != this)
            { 
            AttractForce(obj);
            }
        }
    }
    void AttractForce(Gravitation other)
    {
        Rigidbody otherRB = other.rb;

        //หาทิศทางของวัตถุ
        Vector3 direction = rb.position - otherRB.position;
        //ระยะหางระหว่างวัตถุ
        float distance = direction.magnitude;
        //ถ้าวัตถุอยู่ตำแหน่งเดี่ยวกัน
        if (distance == 0) 
        { return; }
        //ใช้สูตรหาแรงดึงดูด F= G*(m1*m2/r^2)
        float forceMagnitiude = G * (rb.mass * otherRB.mass) / Mathf.Pow(distance, 2);
        //รวมทิศทาง เข้ากับแรงดึงดูดดที่ได้
        Vector3 gravityForce = forceMagnitiude * direction.normalized;
        //ใส่ทแรงที่ได้ให้กับวัตถุ
        otherRB.AddForce(gravityForce);
    }
}
