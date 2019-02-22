# VoxCakeFramework [Public]
VoxCakeFramework is the framework which provides easy and efficiency work with voxel graphics.
## Features
- [X] Handling of voxel data;
- [X] Generating geometry for the voxels;
    - [X] Greedy meshing;
    - [X] Greedy meshing;
    - [X] Greedy meshing;
    - [X] Greedy meshing with Ambient Occlussion and Texturing;
- [X] Saving and Loading of voxel volumes;
- [ ]
### Installing
1. Copy VoxCake folder to your Unity project.
2. You don`t need the second step, youre ready to create great things!
3. You also don`t need the third step :D

### First steps
Well, i think what at first, you would see the fast results, dont you?
Okay, let`s create your first voxel volume in few lines of code!
```csharp
using UnityEngine;
using VoxCake; // Connecting the VoxCake framework

public class MVolume : MonoBehaviour
{
    void Start()
    {
        Volume volume = new Volume(256, 256, 256, gameObject); // Create the volume with 256x128x256 size in the gameobject which have that script
        volume.LoadVXW("Map", 0xff0000); // Load volume which called "Map" and set the ground color in hex format
        StartCoroutine(volume.Update()); // Update all our chunk to generate map geometry
    }
}
```
