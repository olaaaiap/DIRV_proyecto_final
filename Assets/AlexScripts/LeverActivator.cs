using UnityEngine;
using System;

public class LeverActivator : MonoBehaviour
{
    private bool activated;

    public Action OnActivated;

    void Update()
    {
        if (activated) return;

        float x = NormalizeAngle(transform.localEulerAngles.x);
        Debug.Log(x);

        if (x <= -45f)
        {
            activated = true;
            OnActivated?.Invoke();
            GetComponent<AudioSource>().Play();
        }
    }

    float NormalizeAngle(float angle)
    {
        if (angle > 180f)
            angle -= 360f;

        return angle;
    }
}
