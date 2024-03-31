using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class RandomColorOnAwake : MonoBehaviour
{
    private static readonly int Color = Shader.PropertyToID("_Color");

    private void Awake()
    {
        // Generate a random hue, with saturation and value set to maximum
        Color randomColor = UnityEngine.Color.HSVToRGB(Random.value, 1f, 1f);

        // Get the renderer component attached to this GameObject
        Renderer renderer = GetComponent<Renderer>();

        // Create a new material property block
        MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();

        // Load the current property block of the renderer
        renderer.GetPropertyBlock(propertyBlock);

        // Set the color property using "_Color" as the property name. This name might change based on the shader you are using.
        // If it's a standard shader, "_Color" is usually correct for the main color.
        propertyBlock.SetColor(Color, randomColor);

        // Apply the modified property block back to the renderer
        renderer.SetPropertyBlock(propertyBlock);
    }
}

