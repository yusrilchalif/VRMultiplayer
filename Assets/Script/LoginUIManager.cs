using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginUIManager : MonoBehaviour
{
    [SerializeField] GameObject connectOptionsPanelGameObject;
    [SerializeField] GameObject connectingWithNamePanelGameObject;

    // Start is called before the first frame update
    void Start()
    {
        connectOptionsPanelGameObject.SetActive(true);
        connectingWithNamePanelGameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
