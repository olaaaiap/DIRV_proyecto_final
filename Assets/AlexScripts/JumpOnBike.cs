using System.Collections;
using UnityEngine;

public class JumpOnBike : MonoBehaviour
{

    public GameObject bici;
    public GameObject player;
    public GameObject player2;

    public CharacterController characterController;


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

        player.SetActive(false);
        player2.SetActive(true);

        Destroy(gameObject);
    }
}
