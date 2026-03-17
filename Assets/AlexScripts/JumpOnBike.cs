using System.Collections;
using UnityEngine;

public class JumpOnBike : MonoBehaviour
{

    public GameObject bici;
    public GameObject player;

    public CharacterController characterController;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        player.transform.localPosition = new Vector3(0.18f, -0.4f, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {

            characterController.enabled = false;

            StartCoroutine(MountPlayer());
            
        }
    }

    private IEnumerator MountPlayer()
    {
        yield return new WaitForSeconds(.1f);

        player.transform.parent = bici.transform;
        player.transform.localPosition = new Vector3(0.18f, -0.4f, -0f);
        player.transform.localRotation = Quaternion.Euler(new Vector3(0f, 270f, 0f));

        Destroy(gameObject);
    }
}
