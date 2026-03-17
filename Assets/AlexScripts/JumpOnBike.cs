using UnityEngine;

public class JumpOnBike : MonoBehaviour
{

    public GameObject bici;
    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {

            player.transform.parent = bici.transform;
            player.transform.localPosition = new Vector3(0.18f, -0.4f, 0f);
            player.transform.localRotation = Quaternion.Euler(new Vector3(0f, 270f, 0f));

            Destroy(gameObject);
        }
    }
}
