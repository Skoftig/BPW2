using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Firewall : MonoBehaviour
{
    public float waitTime = 1f;
    public GameObject deathScreen;
    public PauseMenu pauseMenu;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.collider.gameObject.name);
        StartCoroutine(FireReset());
    }    

    public void TurnOff()
    {
        gameObject.SetActive(false);
    }

    private IEnumerator FireReset()
    {
        Debug.Log("Nu zit je in de couroutine");
        deathScreen.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        deathScreen.SetActive(false);
        pauseMenu.ResetLevel();
    }
}
