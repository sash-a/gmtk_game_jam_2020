using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class ProjectileBlock : ElevatorBlock
{
    static string SHOOT_DIR_ARG = "shootDir";
    [HideInInspector] public string shootDir = "u";//u,d,l,r

    static string PROJECTILE = "projectile";  // type
    static string SHOOT_SPEED_ARG = "shootSpeed";
    static string RATE_ARG = "rate";
    static string DELAY_ARG = "delay";

    [HideInInspector] public float shootRate;//every 2 seconds
    [HideInInspector] public float projectileSpeed;
    [HideInInspector] public float delay = 0; //how many seconds to wait before first firing

    [HideInInspector] public Vector2 shootDirVec;


    float timeSinceShot;

    public GameObject projectilePrefab;

    public float shootPeriod{ get { return 1f / shootRate; } }

    void setShootDirVec()
    {
        shootDir = shootDir.ToLower();
        shootDirVec = new Vector2(shootDir == "l" ? -1 : (shootDir == "r" ? 1 : 0), shootDir == "d" ? -1 : (shootDir == "u" ? 1 : 0));
    }

    private void Start()
    {
        delay = 0;
        chunkID = null;
        projectileSpeed = 2;
        shootRate = 0.5f;
        travelDistance = -1;
        speed = -1;
        parseArgs(args);
        start();
    }

    public override void start()
    {
        base.start();
        timeSinceShot = shootPeriod - delay; // if delay=0 will fire immediately
    }

    public override void update()
    {
        base.update();
        timeSinceShot += Time.deltaTime;
        if (active && timeSinceShot > shootPeriod)
        {
            shoot();
        }
    }

    private void shoot()
    {
        GameObject proj = Instantiate(projectilePrefab);
        proj.SetActive(true);
        proj.GetComponent<Rigidbody2D>().velocity = shootDirVec * projectileSpeed;

        Vector3 pos = transform.position;
        pos.z = projectilePrefab.transform.position.z;
        proj.transform.position = pos;
    
        proj.transform.rotation = Quaternion.Euler(0, 0, getAimAngle());
        proj.GetComponent<Projectile>().spawner = GetComponentInChildren<Collider2D>().gameObject;

        timeSinceShot = 0;
    }

    public float getAimAngle() {
        float angle = 0;
        if (shootDirVec.x == 0)
        {
            angle = shootDirVec.y > 0 ? 90 : 270;
        }
        if (shootDirVec.y == 0)
        {
            angle = shootDirVec.x > 0 ? 0 : 180;
        }
        return angle;
    }

    public override void activateChanged()
    {
        base.activateChanged();
        if (active) {
            // just started firing again
            timeSinceShot = shootPeriod - delay;
        }
    }

    public override void parseArgs(string args)
    {
        base.parseArgs(args);
        setShootDirVec();
        transform.rotation = Quaternion.Euler(0, 0, getAimAngle());
    }

    internal override void parseArg(string arg)
    {
        string argVal = arg.Split(':')[1];
        base.parseArg(arg);
        if (arg.Contains(SHOOT_SPEED_ARG + ":"))
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
        if (arg.Contains(SHOOT_DIR_ARG)) {
            shootDir = argVal;
        }
    }

    public override string getTypeString()
    {
        return PROJECTILE;
    }

    
}
