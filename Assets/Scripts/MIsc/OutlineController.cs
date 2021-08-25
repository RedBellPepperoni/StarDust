using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineController : MonoBehaviour
{
    // Reference to the shader material defined in the next section
    public Material outlineMaterial;
    public float outlineSize = 1f;

    private List<Material> attachedMaterials = new List<Material> ();

    void Start () {
        foreach (var s in GetComponentsInChildren<SpriteRenderer> ()) {
            AddOutline (s);
        }
    }

    void OnMouseEnter () {
        StartCoroutine (Animate (
            (m, progress) => m.SetFloat ("_Alpha", progress)));
    }

    void OnMouseExit () {
        StartCoroutine (Animate (
            (m, progress) => m.SetFloat ("_Alpha", 1 - progress)));
    }

    private IEnumerator Animate (Action<Material, float> updateAction) {
        for (int i = 0; i < 20; i++) {
            var progress = Mathf.SmoothStep (0f, 1f, (i + 1) / 20f);
            foreach (var m in attachedMaterials) {
                updateAction (m, progress);
            }
            yield return new WaitForSeconds (0.02f);
        }
    }

    private void AddOutline (SpriteRenderer sprite) {
        var width = sprite.bounds.size.x;
        var height = sprite.bounds.size.x;

        var widthScale = 1 / width;
        var heightScale = 1 / height;

        // Add child object with sprite renderer
        var outline = new GameObject ("Outline");
        outline.transform.parent = sprite.gameObject.transform;
        outline.transform.localScale = new Vector3 (1f, 1f, 1f);
        outline.transform.localPosition = new Vector3 (0f, 0f, 0f);
        outline.transform.localRotation = Quaternion.identity;
        var outlineSprite = outline.AddComponent<SpriteRenderer> ();
        outlineSprite.sprite = sprite.sprite;
        outlineSprite.material = outlineMaterial;
        // The UV coordinates of the texture is always from 0..1 no matter
        // what the aspect ratio is so we need to specify both the
        // horizontal and vertical size of the outline
        outlineSprite.material.SetFloat (
            "_HSize", 0.1f * widthScale * outlineSize);
        outlineSprite.material.SetFloat (
            "_VSize", 0.1f * heightScale * outlineSize);
        outlineSprite.sortingOrder = -10;
        attachedMaterials.Add (outlineSprite.material);
    }
}
