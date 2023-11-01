using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarInputConverter : MonoBehaviour
{
    //Avatar Transform
    [SerializeField] Transform mainAvatarTransform;
    [SerializeField] Transform avatarHead;
    [SerializeField] Transform avatarBody;
    [SerializeField] Transform avatarHandLeft;
    [SerializeField] Transform avatarHandRight;

    //XRRig Transform
    [SerializeField] Transform XRHead;
    [SerializeField] Transform xrHandLeft;
    [SerializeField] Transform xrHandRight;

    public Vector3 headPositionOffest;
    public Vector3 handRotationOffset;

    //Prefab emulator
    public GameObject emulator;

    // Start is called before the first frame update
    void Start()
    {
        ChangeEmulatorCondition();
    }

    // Update is called once per frame
    void Update()
    {
        //Head and body sync
        mainAvatarTransform.position = Vector3.Lerp(mainAvatarTransform.position, XRHead.position + headPositionOffest, 0.5f);
        avatarHead.rotation = Quaternion.Lerp(avatarHead.rotation, XRHead.rotation, 0.5f);
        avatarBody.rotation = Quaternion.Lerp(avatarBody.rotation, Quaternion.Euler(new Vector3(0, avatarHead.rotation.eulerAngles.y, 0)), 0.05f);

        //Hands sync
        avatarHandRight.position = Vector3.Lerp(avatarHandRight.position, xrHandRight.position, 0.5f);
        avatarHandRight.rotation = Quaternion.Lerp(avatarHandRight.rotation, xrHandRight.rotation, 0.5f) * Quaternion.Euler(handRotationOffset);

        avatarHandLeft.position = Vector3.Lerp(avatarHandLeft.position, xrHandLeft.position, 0.5f);
        avatarHandLeft.rotation = Quaternion.Lerp(avatarHandRight.rotation, xrHandRight.rotation, 0.5f) * Quaternion.Euler(handRotationOffset);
    }

    void ChangeEmulatorCondition()
    {
        if (Application.isEditor)
        {
            emulator.SetActive(true);
        }
        else if (!Application.isEditor)
        {
            emulator.SetActive(false);
        }
    }
}