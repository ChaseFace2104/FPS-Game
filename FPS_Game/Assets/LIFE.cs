using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LIFE : MonoBehaviour
{
    public float LIFEEETIMEEE = 5;
    public int damage;

    private float grase = 0.5f;
    private float graseTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, LIFEEETIMEEE);
    }

    private void Update()
    {
        graseTime += Time.fixedDeltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        target targetHit = collision.gameObject.GetComponent<target>();
        if (targetHit)
        {
            targetHit.Damage(damage);
        }
        
        if (graseTime > grase) Destroy(gameObject);
    }
}
