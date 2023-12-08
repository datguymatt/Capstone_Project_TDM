using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    private Ray ray;
    public RaycastHit hit;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private float maxInteractionDistance;
    public bool isHitting;

    public void Update()
    {
        ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        isHitting = Physics.Raycast(ray, out hit, maxInteractionDistance, interactableLayer);
    }
}
