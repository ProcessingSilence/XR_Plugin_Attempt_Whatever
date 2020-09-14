using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ColorChanger : MonoBehaviour
{
    public Material greyMaterial;
    public Material pinkMaterial;

    public MeshRenderer _meshRenderer;
    public XRGrabInteractable _grabInteractable;
    
    void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _grabInteractable = GetComponent<XRGrabInteractable>();
        if (_meshRenderer != null)
        {
            Debug.Log("mesh renderer");
        }
        if (_grabInteractable != null)
        {
            Debug.Log("grab interactable");
        }

        _grabInteractable.onActivate.AddListener(SetPink);
        _grabInteractable.onDeactivate.AddListener(SetGrey);
    }

    private void OnDestroy()
    {
        _grabInteractable.onActivate.RemoveListener(SetPink);
        _grabInteractable.onDeactivate.RemoveListener(SetGrey);
    }

    private void SetGrey(XRBaseInteractor interactor)
    {
        _meshRenderer.material = greyMaterial;
        Debug.Log("GREY");
    }

    private void SetPink(XRBaseInteractor interactor)
    {
        _meshRenderer.material = pinkMaterial;
        Debug.Log("PINK");
    }
}
