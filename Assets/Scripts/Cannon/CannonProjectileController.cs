using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

// Add namespaces
public class CannonProjectileController : MonoBehaviour
{
    // Create new list object at class creation
    // Clear & fill the list at OnValidate(), destroy the old objects -> Editor
    private List<GameObject> _pooledProjectiles;

    // Attributes should be on the same line as the field
    [SerializeField]
    private GameObject prefabToPool;

    [SerializeField]
    private int amountToPool = 20;

    // Create the object using new(); this way it's not null
    [SerializeField]
    public UnityEvent OnCannonFire;

    // Start is called before the first frame update
    // Inconsistency with the access modifier
    void Start()
    {
        // Put this in a named function
        _pooledProjectiles = new List<GameObject>();
        // Unclear naming, abbreviations not allowed
        GameObject tmp;
        // Explicit type declaration over implicit, please use var
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(prefabToPool);
            tmp.SetActive(false);
            _pooledProjectiles.Add(tmp);
        }
    }

    // Returns a pooled projectile if available, otherwise returns null
    // Docstrings!
    public GameObject GetPooledProjectile()
    {
        // Explicit type declaration over implicit, please use var
        for (int i = 0; i < amountToPool; i++)
        {
            if (!_pooledProjectiles[i].activeInHierarchy)
            {
                return _pooledProjectiles[i];
            }
        }
        return null;
    }

    // Activates a pooled projectile and invokes the cannon fired event
    // Docstrings!
    public void ShootProjectile()
    {
        // Explicit type declaration over implicit, please use var
        GameObject projectile = GetPooledProjectile();
        
        if (projectile)
        {
            projectile.transform.position = transform.position;
            projectile.SetActive(true);
            OnCannonFire.Invoke();
        }
        else
        {
            // Spelling mistake
            // Use string interpolation over formatting
            Debug.LogWarning("Not enough pooled projectiles! Consider increasing the current ammount: " + amountToPool);
        }
    }
}
