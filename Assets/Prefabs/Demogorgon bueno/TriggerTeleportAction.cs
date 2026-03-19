using System.Collections;
using UnityEngine;

public class TriggerTeleportAction : MonoBehaviour
{
    public Transform character;
    public Transform targetPoint;
    private AudioSource audioSource;
    private Animator animator;
    private bool activated = false;

    public float waitTime = 2f;

    void Start()
    {
        if (character != null)
        {
            animator = character.GetComponent<Animator>();
            audioSource = character.GetComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (activated) return;

        if (other.tag != "Player") return;
        character.position = targetPoint.position;

        //character.rotation = targetPoint.rotation;

        if (animator != null)
        {
            animator.SetTrigger("Action");
        }

        if (audioSource != null)
        {
            audioSource.Play();
        }
        activated = true;

        //changeScene();

        StartCoroutine(changeScene());

    }

    private IEnumerator changeScene()
    {
        yield return new WaitForSeconds(waitTime);

        if (SceneLoadingManagement.instance != null)
        {
            SceneLoadingManagement.instance.Finish();
        }
        else
        {
            Debug.LogError("SceneLoadingManagement no existe en la escena");

            // OPCIONAL: fallback para probar en build
            //Application.Quit();
        }
    }
}
