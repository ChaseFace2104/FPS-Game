using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireProjectile : MonoBehaviour
{
    public Transform[] guns;
    private int currentGun;
    public GameObject bullet;
    public Transform spawn;
    public float speed;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) currentGun = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2)) currentGun = 1;

        switch (currentGun)
        {
            case 0:
                SetModel(0);
                if (Input.GetButtonDown("Fire1"))
                {
                    GameObject projectile = Instantiate(bullet, spawn.position, spawn.rotation);
                    projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * speed);
                }
                break;
            case 1:
                SetModel(1);
                if (Input.GetButton("Fire1"))
                {
                    GameObject projectile = Instantiate(bullet, spawn.position, spawn.rotation);
                    projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * speed);
                }
                break;

        }
    }

    void SetModel(int currentGun)
    {
        for (int i = 0; i < guns.Length; i++)
        {
            if (i == currentGun)
            {
                guns[i].gameObject.SetActive(true);
            } else
            {
                guns[i].gameObject.SetActive(false);
            }
        }
    }
}
