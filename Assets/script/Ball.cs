using UnityEngine;

public class Ball : MonoBehaviour
{
    public ColorType assignedColor;
    public float speed = 20f;        // скорость движени€

    private void Start()
    {
        // ≈сли используетс€ Rigidbody, делаем его кинематическим, чтобы не мешал
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;
    }

    private void Update()
    {
        // –ассто€ние, которое шарик должен пройти в этом кадре
        float step = speed * Time.deltaTime;
        Vector3 direction = transform.forward;

        // –ейкаст перед шариком
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, step))
        {
            // ћы что-то задели
            BlockColor block = hit.collider.GetComponent<BlockColor>();
            if (block != null)
            {
                if (block.colorType == assignedColor)
                {
                    Destroy(block.gameObject); // уничтожаем кубик нужного цвета
                }
            }
            // ¬ любом случае уничтожаем шарик при попадании во что-либо
            Destroy(gameObject);
        }
        else
        {
            // Ќичего не мешает Ц летим дальше
            transform.Translate(direction * step, Space.World);
        }
    }
}