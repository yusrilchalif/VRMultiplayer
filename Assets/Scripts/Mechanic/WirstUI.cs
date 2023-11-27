using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Photon.Pun;

public class WirstUI : MonoBehaviourPunCallbacks
{
    public InputActionAsset inputActions;

    [SerializeField] private Canvas wristUICanvas;
    private InputAction menu;

    private void Start()
    {
        wristUICanvas.enabled = false;

        wristUICanvas = GetComponent<Canvas>();
        menu = inputActions.FindActionMap("XRI LeftHand").FindAction("Menu");
        menu.Enable();
        menu.performed += ToggleMenu;

        if(!photonView.IsMine)
        {
            return;
        }
    }

    private void OnDestroy()
    {
        menu.performed -= ToggleMenu;
    }

    public void ToggleMenu(InputAction.CallbackContext context)
    {
        print("check ui");
        wristUICanvas.enabled = !wristUICanvas.enabled;
    }
}
