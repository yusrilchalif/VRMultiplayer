using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarInputConverter : MonoBehaviour
{
    //Avatar Transform
    public Transform mainAvatarTransform;
    public Transform avatarHead;
    public Transform avatarBody;
    public Transform avatarHandLeft;
    public Transform avatarHandRight;

    //XRRig Transform
    public Transform XRHead;
    public Transform xrHandLeft;
    public Transform xrHandRight;

    public Vector3 headPositionOffest;
    public Vector3 handRotationOffset;

    //Prefab emulator
    //public GameObject emulator;

    // Start is called before the first frame update
    void Start()
    {
        //ChangeEmulatorCondition();
    }

    // Update is called once per frame
    void Update()
    {
        // Head and body sync
        mainAvatarTransform.position = Vector3.Lerp(mainAvatarTransform.position, XRHead.position + headPositionOffest, 0.5f);
        avatarHead.rotation = Quaternion.Lerp(avatarHead.rotation, XRHead.rotation, 0.5f);
        avatarBody.rotation = Quaternion.Lerp(avatarBody.rotation, Quaternion.Euler(new Vector3(0, avatarHead.rotation.eulerAngles.y, 0)), 0.05f);

        // Right hand sync
        avatarHandRight.position = Vector3.Lerp(avatarHandRight.position, xrHandRight.position, 0.5f);
        avatarHandRight.rotation = Quaternion.Lerp(avatarHandRight.rotation, xrHandRight.rotation, 0.5f) * Quaternion.Euler(handRotationOffset);

        // Left hand does not follow the right hand
        // Remove the interpolation for the left hand
        avatarHandLeft.position = xrHandLeft.position;
        avatarHandLeft.rotation = xrHandLeft.rotation * Quaternion.Euler(handRotationOffset);
    }
}
