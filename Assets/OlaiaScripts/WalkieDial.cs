using System.Collections;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class WalkieDial : MonoBehaviour
{
    public AudioSource staticAudio;
    public AudioSource voiceAudio;
    public AudioSource radioRotoAudio;

    [Range(0f, 1f)]
    public float frequencyDial;

    [Range(0f, 1f)]
    public float volumeDial;

    public float correctFrequency = 0.65f;
    public float range = 0.08f;

    public float maxStaticVolume = 0.4f;
    public float maxVoiceVolume = 0.7f;
    bool triggered = false;
    bool exploded = false;

    private void Start()
    {
        //StartCoroutine(ExplodeCountdown());
    }
    void Update()
    {
        if (!exploded)
        {
            float distance = Mathf.Abs(frequencyDial - correctFrequency);
            float clarity = Mathf.Clamp01(1 - (distance / range));

            voiceAudio.volume = clarity * volumeDial * maxVoiceVolume;
            staticAudio.volume = (1 - clarity) * volumeDial * maxStaticVolume;


            if (!triggered && distance < 0.01f)
            {
                triggered = true;
                StartCoroutine(ExplodeCountdown());
            }
        }
    }

    IEnumerator ExplodeCountdown()
    {
        exploded = true;
        float delay = Random.Range(7f, 10f);
        yield return new WaitForSeconds(delay);
        Debug.Log("ExplodeCountdown");
        voiceAudio.volume = 0;
        staticAudio.volume = 0;

        radioRotoAudio.Play();
        radioRotoAudio.volume = 0.5f;
        Debug.Log(radioRotoAudio.volume);
        Debug.Log(voiceAudio.volume);
        Debug.Log(staticAudio.volume);
        StartCoroutine(PararAudio(6f));

    }

    IEnumerator PararAudio(float time)
    {

        yield return new WaitForSeconds(time);

        voiceAudio.volume = 0;
        staticAudio.volume = 0;
        radioRotoAudio.volume = 0;

        yield return null;
    }
}