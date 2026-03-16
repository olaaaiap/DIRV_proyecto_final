using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnPush : MonoBehaviour
{
    public void LoadNextScene()
    {
        SceneLoadingManagement.instance.LoadNextScene();
    }
}
