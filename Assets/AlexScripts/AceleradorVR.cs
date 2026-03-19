using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class AceleradorVR : MonoBehaviour
{
    public XRGrabInteractable grabInteractable;

    public Animator animator;

    public bool agarrado = false;

    void Start()
    {
        grabInteractable.selectEntered.AddListener(_ => agarrado = true);
        grabInteractable.selectExited.AddListener(_ => agarrado = false);
    }

    void Update()
    {
        if (agarrado)
        {
            animator.SetBool("Going", true);
            animator.speed = 1f;
        }
        else
        {
            animator.speed = 0f;

        }      
    }
}