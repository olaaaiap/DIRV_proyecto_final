using UnityEngine;
using UnityEngine.Android;

public class PermisosManager : MonoBehaviour
{
    const string k_Permission = "android.permission.HAND_TRACKING";

#if UNITY_ANDROID
    void Start()
    {
        

        if (!Permission.HasUserAuthorizedPermission(k_Permission))
        {
            Debug.Log("pidiendo permissions");
            var callbacks = new PermissionCallbacks();
            callbacks.PermissionDenied += OnPermissionDenied;
            callbacks.PermissionGranted += OnPermissionGranted;

            Permission.RequestUserPermission(k_Permission, callbacks);
        }
    }

    void OnPermissionDenied(string permission)
    {
        Debug.Log("Permission denied: " + permission);
    }

    void OnPermissionGranted(string permission)
    {
        Debug.Log("Permission granted: " + permission);
    }
#endif // UNITY_ANDROID

}
