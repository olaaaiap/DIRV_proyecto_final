using System.Collections;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(prueba());

    }

    private IEnumerator prueba()
    { 
        Debug.Log("prueba");
        StartCoroutine(ActivarLuz("letraA"));
        yield return new WaitForSeconds(5);
        StartCoroutine(ActivarLuz("letraC"));
    }


    private IEnumerator ActivarLuz(string tagName, float time = 3)
    {
        Debug.Log("ActivarLuz");

        GameObject luzObj = GameObject.FindGameObjectWithTag(tagName);
        Debug.Log("luzObj", luzObj);

        if (luzObj != null)
        {
            //Light pointLight = luzObj.GetComponentInChildren<Light>();
            
            Transform pointLightTranform = luzObj.transform.Find("Point Light");
            if (pointLightTranform != null) {
                GameObject pointLight = pointLightTranform.gameObject;

                if (pointLight != null)
                {

                    pointLight.SetActive(true);

                    yield return new WaitForSeconds(time);

                    pointLight.SetActive(false);
                }
                else
                {
                    Debug.Log("no se encuentra la luz");
                }
            }
            else {
                Debug.Log("no se encuentra la luz (Transform)");
                yield return new WaitForSeconds(time);
            }
        }

        yield return new WaitForSeconds(1);
    }
}
