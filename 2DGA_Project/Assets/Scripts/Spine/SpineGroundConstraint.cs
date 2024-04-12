using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

/// <summary>
/// This class should show of the use of Spines Utility Bone component as well as the use of SkeletonUtilityConstraint
/// </summary>

// we inherit from SkeletonUtilityConstraint to get access to the DoUpdate() method
// https://esotericsoftware.com/spine-unity#SkeletonUtilityConstraint
public class SpineGroundConstraint : SkeletonUtilityConstraint
{
    // LayerMask for what objects the raycast can hit
    public LayerMask groundMask;

    // How high above the target bone the ray begins casting from
    public float castDistance = 5f;

    // X-Axis Adjustment
    public float castOffset = 0f;

    // Y-Axis Adjustment
    public float groundOffset = 0f;

    // speed the target IK position adjusts to the ground
    // should be a rather small value to prevent snapping
    public float adjustSpeed = 5f;

    // starting position of the ray
    Vector3 rayOrigin;
    // direction the ray will be cast in
    Vector3 rayDirection = new Vector3(0, -1, 0);

    // track the hit positions of the raycast
    float hitY;
    float lastHitY;

    // override method from the parent class
    protected override void OnEnable()
    {
        // call the base class method first
        base.OnEnable();
        // set lastHit to current position, at the start we didnt hit anything yet
        lastHitY = transform.position.y;
    }

    public override void DoUpdate()
    {
        // setup vars for the raycast
        rayOrigin = transform.position + new Vector3(castOffset, castDistance, 0);
        float adjustDistanceThisFrame = adjustSpeed * Time.deltaTime;
        hitY = float.MinValue;

        // create a variable to store the hit info from the raycast
        RaycastHit hit;
        bool validHit = false;

        // do a raycast (this is a 3D raycast, use Physics2D for a 2D raycast)
        // out hit -> hit info output is written to hit variable
        validHit = Physics.Raycast(rayOrigin, rayDirection, out hit, castDistance + groundOffset, groundMask);

        // if we hit something
        if (validHit)
        {
            // add  offset to the hit poistion if desired
            hitY = hit.point.y + groundOffset;
            // interpolate between the lastHit position and the new hit position
            hitY = Mathf.MoveTowards(lastHitY, hitY, adjustDistanceThisFrame);
        }
        else {
            // otherwise if nothing was hit interpolate to current position
            hitY = Mathf.MoveTowards(lastHitY, transform.position.y, adjustDistanceThisFrame);
        }

        // llimit the minimum y position to either lastHitY or hitY depending on what is the smaller value
        Vector3 vector = transform.position;
        vector.y = Mathf.Clamp(vector.y, Mathf.Min(lastHitY, hitY), float.MaxValue);
        transform.position = vector;

        // set the skeletons bone position to the transform
        bone.bone.X = transform.localPosition.x;
        bone.bone.Y = transform.localPosition.y;

        lastHitY = hitY;
    }

    // draws the rays we use in the scene view
    private void OnDrawGizmos()
    {
        // normal raycast simulation
        Vector3 hitEnd = rayOrigin + (rayDirection * Mathf.Min(castDistance, rayOrigin.y - hitY));
        Gizmos.DrawLine(rayOrigin, hitEnd);

        // ray to show the groundoffset
        Vector3 clearEnd = rayOrigin + (rayDirection * castDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(hitEnd, clearEnd);
    }
}
