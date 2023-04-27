using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arc : MonoBehaviour
{
    public float maxHeight = 1.5f;
    public IEnumerator TravelArc(Vector3 destination, float duration)
    {
        Vector3 startPosition = transform.position;
        float percentComplete = 0.0f;
        while (percentComplete < 1.0f)
        {
            percentComplete += Time.deltaTime / duration;
            float currentHeight = Mathf.Sin(Mathf.PI * percentComplete) * maxHeight;
            transform.position = Vector3.Lerp(startPosition, destination, percentComplete) +
                                     Vector3.up * currentHeight;
            yield return null;
        }
        gameObject.SetActive(false);
    }
}

