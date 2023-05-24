using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private GameObject _tilePrefab;
    [SerializeField] private Material _dark;
    [SerializeField] private Material _light;

    private void Start()
    {
        for (var z = -3; z <= 3; z++)
        for (var x = -3; x <= 3; x++)
        {
            var tile = Instantiate(_tilePrefab, new Vector3(x, 0, z), Quaternion.identity, transform);
            if (Mathf.Abs(x + z) % 2 == 0)
                tile.GetComponentInChildren<MeshRenderer>().material = _dark;
            else
                tile.GetComponentInChildren<MeshRenderer>().material = _light;
        }
    }
}
