using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Apple;

public class ShootProjectile : MonoBehaviour
{
    public Camera Camera;
    public GameObject Projectile;
    public Transform FirePoint;
    public float projectileSpeed = 30;
    public float fireRate=2;

    private Vector3 destination;
    private float timeToFire;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= timeToFire) 
        {
            timeToFire = Time.time + 1/fireRate;
            Shoot(); 
        }
        
    }

    void Shoot()
    {
        Ray ray = Camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
            destination = hit.point;
        else
            destination = ray.GetPoint(1000);

        InstantiateProjectile(FirePoint);
    }

    void InstantiateProjectile(Transform FirePoint)
    { 
        var projectileObj = Instantiate(Projectile, FirePoint.position, Quaternion.identity) as GameObject;
        projectileObj.GetComponent<Rigidbody>().velocity = (destination - FirePoint.position).normalized * projectileSpeed;
    }
}
