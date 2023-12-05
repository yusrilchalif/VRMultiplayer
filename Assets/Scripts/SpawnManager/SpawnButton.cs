using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnButton : MonoBehaviour
{
    public int spawnIndex;

    public void OnButtonClick()
    {
        AvatarController move = FindObjectOfType<AvatarController>();

        if(move != null)
        {
            move.SetSpawnPosition(spawnIndex);
        }
        else
        {
            Debug.LogError("MoveXRRig not found on scene");
        }
    }

}
