using UnityEngine;

public class WatchTransformSwitch : MonoBehaviour
{
    public Transform attachTransform;

    public Transform leftControllerAttach;
    public Transform leftHandAttach;

    void LateUpdate()
    {
        if (leftHandAttach != null && leftHandAttach.gameObject.activeInHierarchy)
        {
            attachTransform.position = leftHandAttach.position;
            attachTransform.rotation = leftHandAttach.rotation;
        }
        else if (leftControllerAttach != null)
        {
            attachTransform.position = leftControllerAttach.position;
            attachTransform.rotation = leftControllerAttach.rotation;
        }
    }
}