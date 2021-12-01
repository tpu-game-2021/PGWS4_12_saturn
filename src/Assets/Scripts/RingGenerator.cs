using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingGenerator : MonoBehaviour
{
    [SerializeField] private Material matreial;
    private MeshFilter meshFilter_;
    private Mesh mesh_;

    float count = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        count = 0.0f;
        GenerateRing();

        this.transform.localPosition = Vector3.zero;
        this.transform.localRotation = new Quaternion();
        this.transform.localScale = Vector3.one;
    }

    // Update is called once per frame
    void Update()
    {
        count += Time.deltaTime;
        if(3.0f < count){
            count = 0.0f;
        }

        float r = count * 3.0f;
        UpdateRing(r);
    }

    void GenerateRing()
    {
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.sharedMaterial = matreial;
        meshFilter_ = gameObject.AddComponent<MeshFilter>();
        mesh_ = new Mesh();
        meshFilter_.mesh = mesh_;
        UpdateRing(0.0f);
    }

    void UpdateRing(float radius)
    {
        const float WIDTH = 1.0f;
        const int DIVISION_COUNT = 100;// 分割数

        mesh_.Clear ();

        Vector3[] vertices = new Vector3[2*DIVISION_COUNT];
        Vector2[] UVs = new Vector2[2*DIVISION_COUNT];
        Color[] colors = new Color[2*DIVISION_COUNT];
        for(int i = 0; i < DIVISION_COUNT; i++){
            float c = Mathf.Cos(2 * Mathf.PI * (float)i / (float)(DIVISION_COUNT-1));
            float s = Mathf.Sin(2 * Mathf.PI * (float)i / (float)(DIVISION_COUNT-1));

            vertices[2 * i + 0] = new Vector3(radius * c, 0, radius * s);
            vertices[2 * i + 1] = new Vector3((radius+WIDTH) * c, 0, (radius+WIDTH) * s);
            colors[2 * i + 0] = new Color(1, 1, 1, 0);// 内側は透明
            colors[2 * i + 1] = new Color(1, 1, 1, 1);
        }

        int triangle_count = 6 * (DIVISION_COUNT - 1);
        int[] triangles = new int[triangle_count];
        int idx = 0;
        int vtx = 0;
        while(idx < triangle_count)
        {
            triangles[idx + 0] = vtx + 0;
            triangles[idx + 1] = vtx + 1;
            triangles[idx + 2] = vtx + 2;

            triangles[idx + 3] = vtx + 2;
            triangles[idx + 4] = vtx + 1;
            triangles[idx + 5] = vtx + 3;

            idx += 6;
            vtx += 2;
        }

        mesh_.vertices = vertices;
        mesh_.uv = UVs;
        mesh_.colors = colors;
        mesh_.triangles = triangles;

        mesh_.RecalculateBounds();
        mesh_.RecalculateNormals();
    }
}
