using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class MoveXRRig : MonoBehaviour
{
    public Transform xrOrigin;
    public Transform[] spawnPoints;

    public void SetSpawnPoint(int spawnIndex)
    {
        Vector3 spawnPos = spawnPoints[spawnIndex].position;
        Quaternion spawnRot = spawnPoints[spawnIndex].rotation;

        print("Avatar position before: " + xrOrigin.position + "," + xrOrigin.rotation);

        xrOrigin.position = new Vector3(spawnPos.x, spawnPos.y, spawnPos.z);
        xrOrigin.rotation = spawnRot;
        print("Avatar position after: " + xrOrigin.position + "," + xrOrigin.rotation);

    }
}
