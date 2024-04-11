using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

/// <summary>
/// This class shows how to control the attachments and slot system of Spine
/// </summary>

public class ToggleGoggles : MonoBehaviour
{
    SkeletonAnimation skeletonAnimation;

    // Start is called before the first frame update
    void Start()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) {
            // Find the slot on the SkeletonDataAsset with the name goggles
            // to see the names of all slots on a skeleton, fo to your data asset in the project tab
            // then press slots (also check "show Attachments")
            if (skeletonAnimation.skeleton.FindSlot("goggles").Attachment == null)
            {
                // set the attachment on the chosen slot
                skeletonAnimation.skeleton.SetAttachment("goggles", "goggles");
            }
            else {
                skeletonAnimation.skeleton.SetAttachment("goggles", null);
            }
        }
    }
}
