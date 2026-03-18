using System.Collections;
using UnityEngine;

public class JumpOnBike : MonoBehaviour
{

    public GameObject bici;
    public GameObject player;
    public GameObject player2;

    public CharacterController characterController;

    private void Start()
    {

       // StartCoroutine(MountPlayer());
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {


            StartCoroutine(MountPlayer());
            
        }
    }

    private IEnumerator MountPlayer()
    {
        characterController.enabled = false;
        yield return new WaitForSeconds(.1f);

        //player.transform.parent = bici.transform;
        //player.transform.localPosition = new Vector3(0.18f, -0.4f, -0f);
        //player.transform.localRotation = Quaternion.Euler(new Vector3(0f, 270f, 0f));

        player.SetActive(false);
        player2.SetActive(true);

        Destroy(gameObject);
    }
}
