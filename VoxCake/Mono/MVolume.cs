using UnityEngine;
using UnityEngine.UI;
using VoxCake;

public class MVolume : MonoBehaviour
{
    public Volume volume;

    [Header("Volume")]
    public Vector3Int Size;
    public InputField Name;

    void Start()
    {
        volume = new Volume(Size.x, Size.y, Size.z, gameObject);

        //volume.LoadVCMOD("CRASniper");

        volume.LoadVXW("Map", 0xffffff);
        StartCoroutine(volume.Update());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            //volume.SaveVCMOD(Name.text);
        }
        
        if (Input.GetKeyDown(KeyCode.L))
        {
            //volume.Save("newVinece");
        }
    }
}
