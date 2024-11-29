using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMoveRB : MonoBehaviour
{
    [SerializeField] Vector2 spdRange;
    [SerializeField] Rigidbody rb;
    public bool isMoveEnable = false;
    Vector3 dir;
    float speed;

    public void RandomizeDirAndSpd()
    {
        if(!isMoveEnable)
        {
            return;
        }
        speed = Random.Range(spdRange.x,spdRange.y);
        float x = Random.Range(-1f, 1f);
        float z = Random.Range(-1f, 1f);
        dir = new Vector3(x,0,z).normalized;
        transform.forward = dir;
        rb.AddForce(dir * speed, ForceMode.VelocityChange);
    }

    public void Move()
    {
        transform.forward = dir;
        rb.velocity = dir * speed;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if(isMoveEnable) RandomizeDirAndSpd();
    }
}
