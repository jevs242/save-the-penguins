using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; 

    public int pinguinsSave { get { return _pinguinsSave; } set { _pinguinsSave = value; } }
    private int _pinguinsSave;

	public int pinguinsDead { get { return _pinguinsDead; } set { _pinguinsDead = value; } }
    private int _pinguinsDead;

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
}
