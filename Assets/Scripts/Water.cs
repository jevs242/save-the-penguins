using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
        GameManager gm = GameManager.Instance;
        PlayerController pc = PlayerController.Instance;

		if(other.CompareTag("Player") && !pc.Dead)
        {
            gm.PenguinsDead++;
            pc.Dead = true;
			SoundManager.Instance.PlaySFX(3, 1 , 1);
            UIManager.Instance.UpdateUI();
			StartCoroutine(RestartGame());
        }
	}

    private IEnumerator RestartGame()
    {
        DestroyIceController.Instance.Show();
		yield return new WaitForSeconds(5);
		GameManager.Instance.CheckGame();
		DestroyIceController.Instance.RestartGrid();
        PlayerController.Instance.RestartPenguin();
    }
}
