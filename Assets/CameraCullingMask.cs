using UnityEngine;

public class CameraCullingMask : MonoBehaviour
{
    public GameObject player;
    public GameObject building;
    public LayerMask exteriorWallsLayer;

    private Camera cameraComponent;
    private Collider buildingCollider;
    private bool isInsideBuilding = false;
    private int originalCullingMask;

    private void Start()
    {
        cameraComponent = GetComponent<Camera>();
        buildingCollider = building.GetComponent<Collider>();
        originalCullingMask = cameraComponent.cullingMask;
    }

   private void Update()
{
    Vector3 playerPosition = player.transform.position;

    if (buildingCollider.bounds.Contains(playerPosition))
    {
        if (!isInsideBuilding)
        {
            cameraComponent.cullingMask = originalCullingMask - exteriorWallsLayer;
            isInsideBuilding = true;
        }
    }
    else
    {
        if (isInsideBuilding)
        {
            // Include exterior walls layer in culling mask
            cameraComponent.cullingMask = originalCullingMask;
            isInsideBuilding = false;
        }
    }
}
}