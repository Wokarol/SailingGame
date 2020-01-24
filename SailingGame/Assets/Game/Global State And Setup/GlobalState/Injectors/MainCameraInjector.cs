using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wokarol.Global;

[RequireComponent(typeof(Camera))]
public class MainCameraInjector : MonoBehaviour
{
    new private Camera camera;

    private void Awake()
    {
        camera = GetComponent<Camera>();
    }

    private void OnEnable()
    {
        Game.World.MainCamera.Set(camera);
    }

    private void OnDisable()
    {
        Game.World.MainCamera.Remove(camera);
    }
}
