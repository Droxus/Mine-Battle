using System.Collections;
using System.Collections.Generic;
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
        mapWidth = 10;
        mapHeight = 3;
        mapDepth = 10;
        generateMap(mapWidth, mapHeight, mapDepth);
    }

    // Update is called once per frame
    void Update() {
        
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
                    // newCube.GetComponent<Renderer>().material = materials[x % 2];
                }
            }
        }
    }
}
