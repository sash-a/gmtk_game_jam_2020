using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class ProjectileBlock : DirectedBlock
{
    static string PROJECTILE = "projectile";  // type
    static string SPEED_ARG = "speed";
    static string RATE_ARG = "rate";
    static string DELAY_ARG = "delay";


    [HideInInspector] public float shootRate = 0.5f;//every 2 seconds
    [HideInInspector] public float projectileSpeed = 1;
    [HideInInspector] public float delay = 0; //how many seconds to wait before first firing

    float timeSinceShot;

    public GameObject projectilePrefab;

    public float shootPeriod{ get { return 1f / shootRate; } }

    private void Start()
    {
        delay = 0;
        parseArgs(args);
        start();
    }

    public override void start()
    {
        base.start();
        timeSinceShot = shootPeriod - delay; // if delay=0 will fire immediately
    }

    private void Update()
    {
        timeSinceShot += Time.deltaTime;
        if (active && timeSinceShot > shootPeriod) {
            shoot();
        }
    }

    private void shoot()
    {
        GameObject proj = Instantiate(projectilePrefab);
        proj.SetActive(true);
        proj.GetComponent<Rigidbody2D>().velocity = dirVec * projectileSpeed;

        Vector3 pos = transform.position;
        pos.z = projectilePrefab.transform.position.z;
        proj.transform.position = pos;

        proj.GetComponent<Projectile>().spawner = GetComponentInChildren<Collider2D>().gameObject;

        timeSinceShot = 0;
    }

    public override void activateChanged()
    {
        base.activateChanged();
        if (active) {
            // just started firing again
            timeSinceShot = shootPeriod - delay;
        }
    }

    internal override void parseArg(string arg)
    {
        string argVal = arg.Split(':')[1];
        base.parseArg(arg);
        if (arg.Contains(SPEED_ARG + ":"))
        {
            projectileSpeed = float.Parse(argVal, CultureInfo.InvariantCulture);
        }
        if (arg.Contains(RATE_ARG + ":"))
        {
            shootRate = float.Parse(argVal, CultureInfo.InvariantCulture);
        }
        if (arg.Contains(DELAY_ARG)) {
            delay = float.Parse(argVal, CultureInfo.InvariantCulture);
        }
    }

    public override string getTypeString()
    {
        return PROJECTILE;
    }

    
}
