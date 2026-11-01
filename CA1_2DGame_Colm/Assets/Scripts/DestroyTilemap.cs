using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DestroyTilemap : MonoBehaviour
{
    public Tilemap tm;
    //[SerializeField] private GameObject projectile;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tm = GetComponent<Tilemap>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.GetComponent<Projectile>() != null) {
        //    Destroy(this.gameObject);
        //}
    }
}
