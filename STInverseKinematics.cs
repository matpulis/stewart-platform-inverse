using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class STInverseKinematics : MonoBehaviour
{
    //MOTION BASE
	public double a = 0; 
	public double b = 0; 
	public double c = 0;

    public float base_radius = 300f;
	public float base_angle = 0.0585f;

	public float platform_radius = 300f;
	public float platform_angle = 0.0585f;
	public float platform_height = 300f;


	public float[][] angle_matrix = new float[][] {
        new float[] { 0,0,0 },
        new float[] { 0,0,0 },
        new float[] { 0,0,0 }
    };

	public float[][] base_matrix = new float[][] {
        new float[] { 0,0,0,0,0,0 },
        new float[] { 0,0,0,0,0,0 },
        new float[] { 0,0,0,0,0,0 },
    };

    public List<GameObject> base_points = new List<GameObject>(6);
	public List<GameObject> platform_points = new List<GameObject>(6);
	
	public List<Vector3> initial_platform_positions = new List<Vector3>(6);

    // Start is called before the first frame update
    void Start()
    {
        CreateBasePoints();
		CreatePlatformPoints();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlatformPoints();
    }

    private void UpdatePlatformPoints()
	{
		CalculateRotationMatrix();

		for (int axis_number = 0; axis_number < 6; axis_number++)
		{
			for (var i = 0; i < 3; i++)
			{
				base_matrix[i][axis_number] = angle_matrix[i][0] * initial_platform_positions[axis_number].x + angle_matrix[i][1] * platform_height + angle_matrix[i][2] * initial_platform_positions[axis_number].z;
			}

			var e = 1 + base_matrix[0][axis_number] - base_points[axis_number].transform.position.x;
			var f = 1 + base_matrix[1][axis_number] - base_points[axis_number].transform.position.y;
			var g = 0 + base_matrix[2][axis_number] - base_points[axis_number].transform.position.z;

			platform_points[axis_number].transform.position = new Vector3(base_matrix[0][axis_number], base_matrix[1][axis_number], base_matrix[2][axis_number]);


		}
	}

    private void CreateBasePoints()
	{
		GameObject sphere;

		sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		sphere.name = "Base 0";
		sphere.GetComponent<Renderer>().material.color = new Color(0, 0, 1, 1);
		sphere.transform.position = new Vector3(
			base_radius * (float)Math.Sin(platform_angle),
			0,
			base_radius * (float)Math.Cos(platform_angle)
		);
		sphere.transform.localScale = new Vector3(20, 20, 20);
		base_points.Add(sphere);

		sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		sphere.name = "Base 1";
		sphere.GetComponent<Renderer>().material.color = new Color(0, 0, 1, 1);
		sphere.transform.position = new Vector3(
			-base_radius * (float)Math.Sin(platform_angle),
			0,
			base_radius * (float)Math.Cos(platform_angle)
		);
		sphere.transform.localScale = new Vector3(20, 20, 20);
		base_points.Add(sphere);

		sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		sphere.name = "Base 2";
		sphere.GetComponent<Renderer>().material.color = new Color(0, 0, 1, 1);
		sphere.transform.position = new Vector3(
			-base_radius * (float)Math.Cos(((float)Math.PI / 6) - platform_angle),
			0,
			-base_radius * (float)Math.Sin(((float)Math.PI / 6) - platform_angle)
		);
		sphere.transform.localScale = new Vector3(20, 20, 20);
		base_points.Add(sphere);

		sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		sphere.name = "Base 3";
		sphere.GetComponent<Renderer>().material.color = new Color(0, 0, 1, 1);
		sphere.transform.position = new Vector3(
			-base_radius * (float)Math.Cos(((float)Math.PI / 6) + platform_angle),
			0,
			-base_radius * (float)Math.Sin(((float)Math.PI / 6) + platform_angle)
		);
		sphere.transform.localScale = new Vector3(20, 20, 20);
		base_points.Add(sphere);

		sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		sphere.name = "Base 4";
		sphere.GetComponent<Renderer>().material.color = new Color(0, 0, 1, 1);
		sphere.transform.position = new Vector3(
			base_radius * (float)(float)Math.Cos(((float)Math.PI / 6) + platform_angle),
			0,
			-base_radius * (float)Math.Sin(((float)Math.PI / 6) + platform_angle)
		);
		sphere.transform.localScale = new Vector3(20, 20, 20);
		base_points.Add(sphere);

		sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		sphere.name = "Base 5";
		sphere.GetComponent<Renderer>().material.color = new Color(0, 0, 1, 1);
		sphere.transform.position = new Vector3(
			base_radius * (float)Math.Cos(((float)Math.PI / 6) - platform_angle),
			0,
			-base_radius * (float)Math.Sin(((float)Math.PI / 6) - platform_angle)
		);
		sphere.transform.localScale = new Vector3(20, 20, 20);
		base_points.Add(sphere);

		

	}

	private void CreatePlatformPoints()
	{
		GameObject sphere;
		Vector3 position;


		sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		sphere.name = "Platform 0";
		sphere.GetComponent<Renderer>().material.color = new Color(0, 1, 0, 1);
		position = new Vector3(
			platform_radius * (float)Math.Cos((Math.PI / 6) + base_angle),
			platform_height,
			platform_radius * (float)Math.Sin((Math.PI / 6) + base_angle)
		);
		sphere.transform.position = position;
		sphere.transform.localScale = new Vector3(20, 20, 20);
		platform_points.Add(sphere);
		initial_platform_positions.Add(position);

		sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		sphere.name = "Platform 1";
		sphere.GetComponent<Renderer>().material.color = new Color(0, 1, 0, 1);
		position = new Vector3(
			-platform_radius * (float)Math.Cos((Math.PI / 6) + base_angle),
			platform_height,
			platform_radius * (float)Math.Sin((Math.PI / 6) + base_angle)
		);
		sphere.transform.position = position;
		sphere.transform.localScale = new Vector3(20, 20, 20);
		platform_points.Add(sphere);
		initial_platform_positions.Add(position);

		sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		sphere.name = "Platform 2";
		sphere.GetComponent<Renderer>().material.color = new Color(0, 1, 0, 1);
		position = new Vector3(
			-platform_radius * (float)Math.Cos((Math.PI / 6) - base_angle),
			platform_height,
			platform_radius * (float)Math.Sin((Math.PI / 6) - base_angle)
		);
		sphere.transform.position = position;
		sphere.transform.localScale = new Vector3(20, 20, 20);
		platform_points.Add(sphere);
		initial_platform_positions.Add(position);

		sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		sphere.name = "Platform 3";
		sphere.GetComponent<Renderer>().material.color = new Color(0, 1, 0, 1);
		position = new Vector3(
			-platform_radius * (float)Math.Sin(base_angle),
			platform_height,
			-platform_radius * (float)Math.Cos(base_angle)
		);
		sphere.transform.position = position;
		sphere.transform.localScale = new Vector3(20, 20, 20);
		platform_points.Add(sphere);
		initial_platform_positions.Add(position);

		sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		sphere.name = "Platform 4";
		sphere.GetComponent<Renderer>().material.color = new Color(0, 1, 0, 1);
		position = new Vector3(
			platform_radius * (float)Math.Sin(base_angle),
			platform_height,
			-platform_radius * (float)Math.Cos(base_angle)
		);
		sphere.transform.position = position;
		sphere.transform.localScale = new Vector3(20, 20, 20);
		platform_points.Add(sphere);
		initial_platform_positions.Add(position);

		sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		sphere.name = "Platform 5";
		sphere.GetComponent<Renderer>().material.color = new Color(0, 1, 0, 1);
		position = new Vector3(
			platform_radius * (float)Math.Cos((Math.PI / 6) - base_angle),
			platform_height,
			platform_radius * (float)Math.Sin((Math.PI / 6) - base_angle)
		);
		sphere.transform.position = position;
		sphere.transform.localScale = new Vector3(20, 20, 20);
		platform_points.Add(sphere);
		initial_platform_positions.Add(position);
		
	}

    private void CalculateRotationMatrix()
	{
		angle_matrix[0][0] = (float)Math.Cos(b) * (float)Math.Cos(a);
		angle_matrix[0][1] = (float)-Math.Sin(b) * (float)Math.Cos(c) + (float)Math.Cos(b) * (float)Math.Sin(a) * (float)Math.Sin(c);
		angle_matrix[0][2] = (float)Math.Sin(b) * (float)Math.Sin(c) + (float)Math.Cos(b) * (float)Math.Sin(a) * (float)Math.Cos(c);

		angle_matrix[1][0] = (float)Math.Sin(b) * (float)Math.Cos(a);
		angle_matrix[1][1] = (float)Math.Cos(b) * (float)Math.Cos(c) + (float)Math.Sin(b) * (float)Math.Sin(a) * (float)Math.Sin(c);
		angle_matrix[1][2] = (float)-Math.Cos(b) * (float)Math.Sin(c) + (float)Math.Sin(b) * (float)Math.Sin(a) * (float)Math.Cos(c);

		angle_matrix[2][0] = (float)-Math.Sin(a);
		angle_matrix[2][1] = (float)Math.Cos(a) * (float)Math.Sin(c);
		angle_matrix[2][2] = (float)Math.Cos(a) * (float)Math.Cos(c);
	}

    private void OnDrawGizmos()
	{
		if (base_points.Count > 0)
		{
			for (int x = 0; x < 6; x++)
			{
				// Draws a blue line from this transform to the target
				//RAMS
				Gizmos.color = Color.red;
				Gizmos.DrawLine(base_points[x].transform.position, platform_points[x].transform.position);

				//BASE
				Gizmos.color = Color.blue;
				Gizmos.DrawLine(base_points[x].transform.position, base_points[(x < 5) ? (x + 1) : 0].transform.position);

				//TOP
				Gizmos.color = Color.green;
				Gizmos.DrawLine(platform_points[x].transform.position, platform_points[(x < 5) ? (x + 1) : 0].transform.position);
			}
		}
	}
}
