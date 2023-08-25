using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class cameraZoom : MonoBehaviour
{
    //install cinemachine
    //change mouse scrollwhell sensitivity in input manager to -0.1 to disabled invert
    new public CinemachineFreeLook camera;
    public CinemachineFreeLook.Orbit[] orbits;

    [Range(0.01f,0.5f)]
    public float minZoom = 0.5f;
    [Range(1f,5f)]
    public float maxZoom = 1.0f;

    [AxisStateProperty]
    public AxisState zAxis = new AxisState(0,1,false,true,50f,0.1f,0.1f,"Mouse ScrollWheel",false);

    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponentInChildren<CinemachineFreeLook>();
        if(camera != null)
        {
            orbits = new CinemachineFreeLook.Orbit[camera.m_Orbits.Length];
            for(int i=0;i<orbits.Length;i++)
            {
                orbits[i].m_Height = camera.m_Orbits[i].m_Height;
                orbits[i].m_Radius = camera.m_Orbits[i].m_Radius;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(orbits != null)
        {
            zAxis.Update(Time.deltaTime);
            float zoomScale = Mathf.Lerp(minZoom,maxZoom,zAxis.Value);
            for(int i=0;i<orbits.Length;i++)
            {
                camera.m_Orbits[i].m_Height = orbits[i].m_Height * zoomScale;
                camera.m_Orbits[i].m_Radius = orbits[i].m_Radius * zoomScale;
            }
        }
    }
}
