using System.Collections.Generic;
using UnityEngine;

namespace GridSystem
{
    public class MeshGrid : MonoBehaviour
    {
        MeshFilter meshFilter;
        MeshRenderer meshRenderer;

        void Awake()
        {
            meshFilter = GetComponent<MeshFilter>();
            meshRenderer = GetComponent<MeshRenderer>();
        }

        public void ToggleVisibility() => meshRenderer.enabled = !meshRenderer.enabled;

        /// <summary>Generates grid mesh with </summary>
        /// <param name="size">The </param>
        public void Generate(int width, int depth)
        {
            if (width <= 0 || depth <= 0)
                return;

            // Adjust halfSizeX and halfSizeZ to account for odd sizes
            float halfSizeX = ((width - 1) / 2.0f) + 0.5f;
            float halfSizeZ = ((depth - 1) / 2.0f) + 0.5f;

            List<Vector3> lines = new List<Vector3>();

            // Add leftmost line
            lines.Add(new Vector3(-halfSizeX, 0, -halfSizeZ));
            lines.Add(new Vector3(-halfSizeX, 0, halfSizeZ));

            // Add rightmost line
            lines.Add(new Vector3(halfSizeX, 0, -halfSizeZ));
            lines.Add(new Vector3(halfSizeX, 0, halfSizeZ));

            // Add lowermost line
            lines.Add(new Vector3(-halfSizeX, 0, -halfSizeZ));
            lines.Add(new Vector3(halfSizeX, 0, -halfSizeZ));

            // Add uppermost line
            lines.Add(new Vector3(-halfSizeX, 0, halfSizeZ));
            lines.Add(new Vector3(halfSizeX, 0, halfSizeZ));

            for (int i = 0; i < width + 1; i++)
            {
                // Add vertical lines
                float x = i - halfSizeX;
                lines.Add(new Vector3(x, 0, -halfSizeZ));
                lines.Add(new Vector3(x, 0, halfSizeZ));
            }

            for (int i = 0; i < depth + 1; i++)
            {
                // Add horizontal lines
                float z = i - halfSizeZ;
                lines.Add(new Vector3(-halfSizeX, 0, z));
                lines.Add(new Vector3(halfSizeX, 0, z));
            }

            int[] indices = new int[lines.Count];
            for (int i = 0; i < lines.Count; i++)
                indices[i] = i;

            meshFilter.mesh.Clear();
            meshFilter.mesh.SetVertices(lines);
            meshFilter.mesh.SetIndices(indices, MeshTopology.Lines, 0, true);

            Debug.Log("Grid Generated!");
        }
    }
}