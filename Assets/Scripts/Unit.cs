using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    AudioSource audioSource;
    public string unitName;
    public int unitLevel;

    public int damage;

    public int maxHP;
    public int currentHP;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool TakeDamage(int sufferedDamage)
    {
        audioSource.Play();
        currentHP -= sufferedDamage;
        StartCoroutine(TakeDamageRoutine());
        if(currentHP <= 0)
            return true;

        else return false;
    }

    IEnumerator TakeDamageRoutine()
    {
        GetComponentInChildren<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.4f);
        GetComponentInChildren<SpriteRenderer>().color = Color.white;
    }


}
