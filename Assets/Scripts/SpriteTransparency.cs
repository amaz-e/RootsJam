using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteTransparency : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float maxDistance = 20f;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Ray ray = new Ray(camera.transform.position, transform.position - camera.transform.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance, layerMask))
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);
        }
        else
        {
            spriteRenderer.color = Color.white;
        }
    }
}
