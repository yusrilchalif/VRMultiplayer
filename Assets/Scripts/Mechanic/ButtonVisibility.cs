using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ButtonVisibility : MonoBehaviourPunCallbacks
{
    public string objectTag; // Tag yang akan di-hide/unhide
    public Sprite spriteHidden; // Sprite ketika objek dihide
    public Sprite spriteUnhidden; // Sprite ketika objek diunhide

    private bool isHidden = false;
    public Image buttonImage;
    private GameObject[] objectsWithTag;

    private void Start()
    {
        buttonImage = GetComponent<Image>();
        isHidden = false; // Kondisi awal: unhide
        ToggleObjectState(); // Set kondisi awal objek

        // Mengecek jika objek ini milik pemain lokal
        if (!photonView.IsMine)
        {
            GetComponent<Button>().interactable = false; // Membuat tombol non-interaktif jika bukan pemain lokal
        }
    }

    public void ToggleObjectState()
    {
        photonView.RPC("ToggleObjectStateRPC", RpcTarget.AllBuffered);
    }

    [PunRPC]
    private void ToggleObjectStateRPC()
    {
        isHidden = !isHidden; // Ubah status objek

        if (objectsWithTag == null || objectsWithTag.Length == 0)
        {
            objectsWithTag = GameObject.FindGameObjectsWithTag(objectTag);
        }

        if (objectsWithTag != null && objectsWithTag.Length > 0)
        {
            foreach (GameObject obj in objectsWithTag)
            {
                Renderer rend = obj.GetComponent<Renderer>();

                if (rend != null)
                {
                    rend.enabled = !isHidden; // Ubah visibilitas renderer objek dengan tag yang sesuai
                }
                else
                {
                    obj.SetActive(!isHidden); // Jika Renderer tidak ditemukan, gunakan setActive
                    Debug.LogWarning("Renderer component not found on object with tag: " + objectTag);
                }
            }
        }

        UpdateButtonSprite(); // Perbarui sprite pada tombol
    }

    [PunRPC]
    private void UpdateButtonSpriteRPC(bool isObjHidden)
    {
        if (buttonImage != null)
        {
            isHidden = isObjHidden;
            UpdateButtonSprite(); // Panggil metode UpdateButtonSprite untuk memperbarui sprite
        }
        else
        {
            Debug.LogError("Button Image kosong!");
        }
    }

    private void UpdateButtonSprite()
    {
        if (buttonImage != null)
        {
            buttonImage.sprite = isHidden ? spriteHidden : spriteUnhidden;
        }
        else
        {
            Debug.LogError("Button Image kosong!");
        }
    }
}
