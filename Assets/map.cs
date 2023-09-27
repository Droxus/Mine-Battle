using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class map : MonoBehaviour
{
    public GameObject cube;
    public int mapWidth;
    public int mapHeight;
    public int mapDepth;
    public Material[] materials;
    // Start is called before the first frame update
    void Start() {
        Debug.Log("Hello");
        mapWidth = 10;
        mapHeight = 3;
        mapDepth = 10;
        generateMap(mapWidth, mapHeight, mapDepth);
    }

    // Update is called once per frame
    void Update() {
        onBlockClick();
    }

    void generateMap(int width, int height, int depth) {
        Material yMaterial = materials[0];
        for (int y = 0; y < height; y++) {
            if (y > 0) yMaterial = materials[y+1];
            for (int x = 0; x < width; x++) {
                for (int z = 0; z < depth; z++) {
                    Vector3 position = new Vector3(x, y, z);
                    GameObject newCube = Instantiate(cube, position, transform.rotation);
                    newCube.GetComponent<Renderer>().material = yMaterial;
                }
            }
        }
    }
    void onBlockClick() {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) {
                GameObject clickedCube = hit.collider.gameObject;
                DefineBlock(clickedCube);
            }
        }
    }
    async void DefineBlock(GameObject cube) {
        Debug.Log(cube.transform.position);
        cube.GetComponent<Renderer>().material.color = Color.cyan;
        await Task.Delay(500);
        cube.GetComponent<Renderer>().material.color = Color.white;
    }
}
