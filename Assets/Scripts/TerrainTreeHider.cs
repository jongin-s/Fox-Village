using UnityEngine;

public class TerrainTreeHider : MonoBehaviour
{
    public Terrain terrain; // 터레인 참조
    public Camera mainCamera; // 카메라 참조
    public float hideDistance = 10f; // 나무를 숨길 거리

    private TreeInstance[] originalTrees;

    void Start()
    {
        if (terrain == null)
            terrain = Terrain.activeTerrain; // 기본 Terrain 참조

        if (mainCamera == null)
            mainCamera = Camera.main; // 기본 카메라 참조

        // 나무 데이터를 복사하여 저장
        originalTrees = terrain.terrainData.treeInstances.Clone() as TreeInstance[];
    }

    void Update()
    {
        UpdateTrees();
    }

    void UpdateTrees()
    {
        TreeInstance[] trees = originalTrees.Clone() as TreeInstance[];
        Vector3 cameraPosition = mainCamera.transform.position;

        for (int i = 0; i < trees.Length; i++)
        {
            // 나무의 월드 좌표 계산
            Vector3 treeWorldPosition = Vector3.Scale(trees[i].position, terrain.terrainData.size) + terrain.transform.position;

            // 카메라와의 거리 계산
            float distance = Vector3.Distance(cameraPosition, treeWorldPosition);

            if (distance < hideDistance)
            {
                // 나무를 숨기기 위해서 나무의 높이를 0으로 설정
                trees[i].heightScale = 0;
                trees[i].widthScale = 0;
            }
        }

        // Terrain 데이터 업데이트
        terrain.terrainData.treeInstances = trees;
    }
}
