using UnityEngine;
using UnityEngine.SceneManagement;


public class ExitPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            SceneManager.LoadScene("Map");
        }
    }
}
