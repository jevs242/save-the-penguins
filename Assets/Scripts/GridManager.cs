using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager instance;
    [SerializeField] private GameObject _grid;
	[SerializeField] private GameObject _gridPrefabs;

	private Transform _beginTransform;

    // Start is called before the first frame update
    void Start()
    {
        _beginTransform = _grid.transform;
        _grid = GameObject.Find("Grid");
    }

    public void DestroyGrid()
    {
        //Destroy(_grid.gameObject);
    }

    public void RestartGrid()
    {
        _grid = Instantiate(_gridPrefabs, _beginTransform);
    }
}
