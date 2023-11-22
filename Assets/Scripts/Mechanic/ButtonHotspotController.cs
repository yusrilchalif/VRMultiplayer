using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG;
using DG.Tweening;

public class ButtonHotspotController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public GameObject panelInformation;

    private Quaternion initialQuaternion;

    [SerializeField] Transform targetObject;
    [SerializeField] float scaleDuration = 1.5f;
    [SerializeField] Vector3 minScale = new Vector3(0.5f, 0.5f, 1.0f);
    [SerializeField] Vector3 maxScale = new Vector3(1.5f, 1.5f, 1.0f);
    void Start()
    {
        panelInformation.SetActive(false);
        initialQuaternion = transform.rotation;

        if(targetObject != null)
        {
            PlayScaleAnimation();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if (target != null)
        // {
        //     panelInformation.transform.position = target.position + panelPosition;
        //     lineRendererController.SetLinePoints(transform.position, target.position);
        // }
        // else
        // {
        //     //reset position
        //     transform.rotation = initialQuaternion;
        // }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(new Vector3(1.2f, 1.2f, 1.22f), 0.3f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnPointerExit(eventData);
        TogglePopUp();
    }

    void TogglePopUp()
    {
        if (panelInformation.activeSelf)
        {
            panelInformation.SetActive(false);
            // lineRendererController.DisableLine();
        }
        else
        {
            panelInformation.SetActive(true);
            // lineRendererController.EnableLine();
        }
    }

    void PlayScaleAnimation()
    {
        Sequence scaleSequence = DOTween.Sequence();

        scaleSequence.Append(targetObject.DOScale(maxScale, scaleDuration));
        scaleSequence.AppendInterval(1.0f);
        scaleSequence.Append(targetObject.DOScale(minScale, scaleDuration));
        scaleSequence.SetLoops(-1);
    }
}
