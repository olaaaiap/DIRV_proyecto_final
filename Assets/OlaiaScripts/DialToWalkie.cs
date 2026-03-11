using UnityEngine.XR.Content.Interaction;
using UnityEngine;

public class DialToWalkie : MonoBehaviour
{
    public XRKnob knob;
    public WalkieDial walkie;

    public bool controlsFrequency;

    void Update()
    {
        if (controlsFrequency)
        {
            walkie.frequencyDial = knob.value;
        }
        else
        {
            walkie.volumeDial = knob.value;
        }
    }

}