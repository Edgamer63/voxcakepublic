using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxCake;

public class MEditor : MonoBehaviour
{
    public GameObject Volume;

    byte tool = 1;  // 1 - Line, 2 - Cube; 3 - Sphere

    void Update()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Editor.SetVectorOn();
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (tool == 1)
                {
                    StartCoroutine(Editor.SetLineOn(UColor.CurrentColor, Volume.GetComponent<MVolume>().volume));
                }

                if (tool == 2)
                {
                    StartCoroutine(Editor.SetCubeOn(UColor.CurrentColor, Volume.GetComponent<MVolume>().volume));
                }

                if (tool == 3)
                {

                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                Editor.SetVectorIn();
            }

            if (Input.GetMouseButtonUp(1))
            {
                if (tool == 1)
                {
                    StartCoroutine(Editor.SetLineIn(0, Volume.GetComponent<MVolume>().volume));
                }

                if (tool == 2)
                {
                    StartCoroutine(Editor.SetCubeIn(0, Volume.GetComponent<MVolume>().volume));
                }

                if (tool == 3)
                {

                }
            }

            if (Input.GetMouseButtonDown(2))
            {
                Editor.SetVectorIn();
            }

            if (Input.GetMouseButtonUp(2))
            {
                if (tool == 1)
                {
                    StartCoroutine(Editor.SetLineIn(UColor.CurrentColor, Volume.GetComponent<MVolume>().volume));
                }

                if (tool == 2)
                {

                }

                if (tool == 3)
                {

                }
            }

            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                StartCoroutine(Editor.GetVoxel(Volume.GetComponent<MVolume>().volume));
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                tool = 1;
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                tool = 2;
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                tool = 3;
            }


            /*
            if (Input.GetAxis("7th Axis"))
            {
                StartCoroutine(Editor.SetVoxel(Color, Volume.GetComponent<VCVolume>().volume));
            }
            */

            /*
            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine(Editor.SetVoxel(Color, Volume.GetComponent<VCVolume>().volume));
            }
            
            if (Input.GetMouseButtonUp(0))
            {
                Editor.Add("Line");
            }

            if (Input.GetMouseButtonDown(1))
            {
                Editor.Delete();
            }

            if (Input.GetMouseButtonUp(1))
            {
                Editor.Delete("Line");
            }

            if (Input.GetMouseButtonDown(2))
            {
                Editor.Paint();
            }

            if (Input.GetMouseButtonUp(2))
            {
                Editor.Paint("Line");
            }
            */
        }
    }
}