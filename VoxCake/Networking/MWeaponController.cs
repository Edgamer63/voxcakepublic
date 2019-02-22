using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MWeaponController : MonoBehaviour
{
    public IWeapon weapon;
    private float nextFire;

    private void Start()
    {
        weapon = new BoltRifle(1);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && Time.time > nextFire)
        {
            nextFire = Time.time + weapon.FireRate;
            Debug.Log("BOOM!");
            //Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        }
    }

    private void Fire()
    {
        if (Input.GetMouseButton(0))
        {
            Debug.Log("BOOM!");
        }
    }
}
