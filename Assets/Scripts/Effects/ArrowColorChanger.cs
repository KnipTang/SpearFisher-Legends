using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowColorChanger : MonoBehaviour
{
    [SerializeField]
    private Material[] _materials = null;
    [SerializeField]
    private float _materialChangeInterval = 1.5f;

    private int _currentMaterialIndex = 0;
    private void Start()
    {
        // Change material every 3 seconds starting from now
        InvokeRepeating("ChangeMaterial", 0f, _materialChangeInterval);
    }

    private void ChangeMaterial()
    {
        // Check if materials array is not empty
        if (_materials != null && _materials.Length > 0)
        {
            // Assign the current material
            GetComponent<Renderer>().material = _materials[_currentMaterialIndex];

            // Increment the material index and wrap around if necessary
            _currentMaterialIndex = (_currentMaterialIndex + 1) % _materials.Length;
        }
        else
        {
            Debug.LogError("Materials array is not set or empty.");
        }
    }
}
