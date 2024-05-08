// Unused namespaces should be removed
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Missing namespace
public class HitScript : MonoBehaviour
{
    // Incorrect access modifier, should be private serialized field
    public UnityEvent onDieEvent;
    // Fields on their own line
    [SerializeField]
    private int maxHp = 3, hp = 3, damageOnHit = 1;
    // Default values should be removed, bool is always false by default
    [SerializeField]
    private bool damageable = true, testBool = false;

    [SerializeField]
    private Material damageColor;

    private Material _startMaterial;

    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    // Incorrect naming of private field, should be bloodSprayParticles
    private GameObject _bloodsprayParticles;
    private void Start()
    {
        // Please don't use this unless necessary
        _spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        _startMaterial = _spriteRenderer.material;
    }
    // Inconsistent access modifier, implicit instead of explicit...
    void OnBlockHit(int damage)
    {
        if (damageable)
        {
            hp = hp - damage;
            StartCoroutine(FlashRed());
            if (hp <= 0)
            {
                onDieEvent.Invoke();
            }
        }
    }
    // Random commends
    //impact from fall
    //impact from things falling on it
    
    void FixedUpdate()
    {
        // Look for a way to have this not in the update... e.g. custom editor with a button etc.
        if (testBool)
        {
            OnBlockHit(damageOnHit);
            testBool = false;
        }
    }
    // Use this inside the class to make it clear that it is a method for this class
    // Or move to separate script
    public void OnDie()
    {
        Destroy(this.gameObject);
    }
    // Use this inside the class to make it clear that it is a method for this class
    // Or move to separate script
    public void EnemyOnDie()
    {
        // PLease find a way for it already to exist, but just deactivate it, something along those lines
        Instantiate(_bloodsprayParticles, transform.position, Quaternion.identity);
        GameManager.instance._enemyKillCount++;
    }
    
    private IEnumerator FlashRed()
    {
        _spriteRenderer.material = damageColor;
        yield return new WaitForSeconds(0.1f);
        _spriteRenderer.material = _startMaterial;
    }
}
