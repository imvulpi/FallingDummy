using Godot;
using System;

public partial class MeshSplitting : MeshInstance2D
{
    public override void _Ready()
    {
        var originalMeshInstance = this;
        var originalMesh = originalMeshInstance.Mesh;

        // Split the mesh into two pieces
        var pieces = SplitMeshIntoTwo(originalMesh);

        // Create two new MeshInstance2D nodes
        foreach (var piece in pieces)
        {
            var meshInstance = new MeshInstance2D();
            meshInstance.Mesh = piece;
            GetParent().CallDeferred(MethodName.AddChild, meshInstance);
        }
    }

    private ArrayMesh[] SplitMeshIntoTwo(Mesh mesh)
    {
        // Extract vertex and index data
        var arrays = mesh.SurfaceGetArrays(0); // Assumes the mesh has a single surface
        var vertices = (Vector2[])arrays[(int)ArrayMesh.ArrayType.Vertex];
        var indices = (int[])arrays[(int)ArrayMesh.ArrayType.Index];

        // Split indices into two groups
        int midPoint = indices.Length / 2;
        var indicesPart1 = SubArray(indices, 0, midPoint);
        var indicesPart2 = SubArray(indices, midPoint, indices.Length - midPoint);

        // Create two new meshes
        var part1 = CreateMeshWithUpdatedVertices(vertices, indicesPart1);
        var part2 = CreateMeshWithUpdatedVertices(vertices, indicesPart2);

        return new ArrayMesh[] { part1, part2 };
    }

    private int[] SubArray(int[] array, int start, int length)
    {
        var result = new int[length];
        Array.Copy(array, start, result, 0, length);
        return result;
    }

    private ArrayMesh CreateMeshWithUpdatedVertices(Vector2[] originalVertices, int[] originalIndices)
    {
        // Identify unique vertices used in this piece
        var uniqueVertices = new Godot.Collections.Dictionary<int, int>();
        var newVertices = new Godot.Collections.Array<Vector2>();
        var newIndices = new int[originalIndices.Length];

        int newIndex = 0;

        for (int i = 0; i < originalIndices.Length; i++)
        {
            int originalIndex = originalIndices[i];

            if (!uniqueVertices.ContainsKey(originalIndex))
            {
                // Add to unique vertices
                uniqueVertices[originalIndex] = newIndex;
                newVertices.Add(originalVertices[originalIndex]);
                newIndex++;
            }

            // Update the index to point to the new vertex array
            newIndices[i] = uniqueVertices[originalIndex];
        }

        // Create the mesh
        var newMesh = new ArrayMesh();
        var arrays = new Godot.Collections.Array
        {
            [(int)ArrayMesh.ArrayType.Vertex] = newVertices,
            [(int)ArrayMesh.ArrayType.Index] = newIndices
        };

        newMesh.AddSurfaceFromArrays(Mesh.PrimitiveType.Triangles, arrays);

        return newMesh;
    }
}
