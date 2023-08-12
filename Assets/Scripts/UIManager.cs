using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public static UIManager instance;  
    [SerializeField] private Text _howManySave;
	[SerializeField] private Text _howManyDead;
    [SerializeField] private Image _canMove;

	private void Awake()
	{
		instance = this;
	}

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateUI()
    {
        _howManySave.text = GameManager.instance.pinguinsSave.ToString("00");
        _howManyDead.text = GameManager.instance.pinguinsDead.ToString("00");
        
        _canMove.color = PlayerController.instance.canMove ? UnityEngine.Color.green : UnityEngine.Color.red;
    }

    
}
