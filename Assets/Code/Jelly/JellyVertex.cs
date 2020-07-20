using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyVertex : MonoBehaviour
{
    //public bool airborn;
    //public bool canSplit;

    //private void OnCollisionEnter2D(Collision2D other)
    //{
    //    var coll = other.collider;
    //    if (airborn && coll.CompareTag("Floor"))
    //    {
    //        // checks if collided with the top of the floor. Doesn't allow wall jumps
    //        Vector3 contactPoint = other.contacts[0].point;
    //        Vector3 center = coll.bounds.center;
    //        Vector3 scale = coll.bounds.extents;

    //        if (contactPoint.y > center.y + scale.y &&
    //            contactPoint.x < center.x + scale.x &&
    //            contactPoint.x > center.x - scale.x)
    //        {
    //            airborn = false;
    //        }
    //    }

    //    canSplit = other.gameObject.CompareTag("Floor") || canSplit;
    //}
}
