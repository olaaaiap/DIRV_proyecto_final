using UnityEngine;

public class ChangeSceneOnTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;

        SceneLoadingManagement.instance.LoadNextScene();
    }
}
