using UnityEngine;

public class CartBackflip : MonoBehaviour
{
    public Rigidbody rb;
    public float backflipForce = 50f;  // Augmente la force
    public float backflipInterval = 3f;

    private float timer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= backflipInterval)
        {
            DoBackflip();
            timer = 0f;
        }
    }

    void DoBackflip()
    {
        rb.AddForce(Vector3.up * 20f, ForceMode.Impulse);
        rb.AddTorque(-transform.right * backflipForce, ForceMode.Impulse);

        Debug.Log("Backflip!");
    }
}