using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{

    public GameObject explosionPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExploteAndDie()
    {
        Instantiate(explosionPrefab, new Vector2(transform.position.x, transform.position.y+0.5f), Quaternion.identity);
        Destroy(this.gameObject);
    }
}
