using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private GameObject _tilePrefab;
    [SerializeField] private Material _dark;
    [SerializeField] private Material _light;

    private void Start()
    {
        SetupBoard();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            DestroyBoard();
            SetupBoard();
        }
    }

    private void DestroyBoard()
    {
        for (var i = 0; i < transform.childCount; i++)
            Destroy(transform.GetChild(i).gameObject);
    }

    private void SetupBoard()
    {
        for (var z = -6; z <= 6; z++)
        for (var x = -6; x <= 6; x++)
        {
            var pos = new Vector3(x, 0, z);
            var p = pos.sqrMagnitude / 36;
            if (Random.value < p)
                continue;
            var tile = Instantiate(_tilePrefab, pos, Quaternion.identity, transform);
            if (Mathf.Abs(x + z) % 2 == 0)
                tile.GetComponentInChildren<MeshRenderer>().material = _dark;
            else
                tile.GetComponentInChildren<MeshRenderer>().material = _light;
        }
    }
}
