using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter(Collider other)
	{
        GameManager gm = GameManager.instance;
        PlayerController pc = PlayerController.instance;

		if(other.CompareTag("Player") && !pc.dead)
        {
            gm.pinguinsDead++;
            pc.dead = true;
			SoundManager.instance.PlaySFX(3, 0.5f , 1);
            UIManager.instance.UpdateUI();
			StartCoroutine(RestartGame());
        }
	}

    private IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(2);
        DestroyIceController.instance.RestartGrid();
        PlayerController.instance.RestartPinguin();
    }
}
