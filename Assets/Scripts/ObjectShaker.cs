using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectShaker : MonoBehaviour
{
    public float shakeAmount = 0.1f;
    public float shakeDuration = 0.2f;

    private Vector3 originalPosition;

    BoxCollider2D collider;
    public SpriteRenderer spriteRenderer;

    void Start()
    {
        originalPosition = transform.position;
        collider = GetComponent<BoxCollider2D>();
    }

    public void ShakeObject()
    {
        originalPosition = transform.position;

        StartCoroutine(ShakeCoroutine());
        StartCoroutine(BlinkCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        float elapsedTime = 0f;
        collider.enabled = false;

        while (elapsedTime < shakeDuration)
        {
            transform.position = originalPosition + (Vector3)Random.insideUnitCircle * shakeAmount;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        collider.enabled = true;
        transform.position = originalPosition;
    }

    private IEnumerator BlinkCoroutine()
    {
        spriteRenderer.color = new Color(255,0,0,85);
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = new Color(255, 255, 255, 255);
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = new Color(255, 0, 0, 85);
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = new Color(255, 255, 255, 255);
    }
}
