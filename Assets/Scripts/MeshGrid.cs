using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
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
#warning Test it if it works it works if it doesnt add remainder
            if (width <= 0 || depth <= 0)
                return;

            int halfSizeX = width / 2;
            int halfSizeZ = depth / 2;

            List<Vector3> lines = new List<Vector3>();

            // Add leftmost line
            lines.Add(new Vector3(- halfSizeX, 0,- halfSizeZ));
            lines.Add(new Vector3(- halfSizeX, 0, halfSizeZ));

            // Add lowermost line
            lines.Add(new Vector3(- halfSizeX, 0,- halfSizeZ));
            lines.Add(new Vector3(halfSizeX, 0,-halfSizeZ));

            for (int i = 0; i < width; i++)
            {
                // Add horizontal line
                lines.Add(new Vector3(i - halfSizeX, 0,- halfSizeZ));
                lines.Add(new Vector3(i - halfSizeX, 0, halfSizeZ));
            }
            
            for (int i = 0; i < depth; i++)
            {
                // Add vertical line
                lines.Add(new Vector3(- halfSizeX, 0, i + 1 - halfSizeZ));
                lines.Add(new Vector3(halfSizeX , 0, i + 1 -  halfSizeZ));
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