using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;


    public int damage = 20;

    public float speed = 70f;
    public float explosionRadius = 6f;
    public GameObject impactEffect;
    public void Seek(Transform _target)
    {
        target = _target;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            Debug.Log("DES");
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }
    void HitTarget()
    {
        GameObject effectIns=(GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 5f);

        if (explosionRadius > 0f)
        {
            Explode(); 
        }
        else
        {
            Damage(target);
        }

        void Explode()
        {   
            Collider[] colliders=Physics.OverlapSphere(transform.position, explosionRadius);
            foreach(Collider collider in colliders)
            {
                if (collider.tag == "Enemy")
                {
                    Damage(collider.transform);
                }
            }
        }

        Destroy(gameObject);
    }
    void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        if (e != null)
        {
            e.TakeDamege(damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
