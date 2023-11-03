using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalPlayerUIManager : MonoBehaviour
{
    [SerializeField] GameObject goHomeBtn;

    // Start is called before the first frame update
    void Start()
    {
        goHomeBtn.GetComponent<Button>().onClick.AddListener(VirtualWorldManager.Instance.LeaveRoomAndLoadHomeScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
