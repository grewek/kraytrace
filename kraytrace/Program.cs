// See https://aka.ms/new-console-template for more information
using kraytrace;
using kraytrace.LinearAlgebra;
using kraytrace.Shapes;

using System;
using System.IO;
using System.Text;

string output = "output.ppm";



var aspectRatio = 16.0f / 9.0f;
var width = 400;
var height = (int)((float)width / aspectRatio);

var world = new ShapeContainer();

world.AddShape(new Sphere(new Vector3(0f, 0f, -1f), .5f));
world.AddShape(new Sphere(new Vector3(0f, -100.5f, -1f), 100));

var viewportHeight = 2.0f;
var viewportWidth = aspectRatio * viewportHeight;
var focalLength = 1.0f;

var origin = new Vector3();
var right = new Vector3(viewportWidth, 0.0f, 0.0f);
var up = new Vector3(0.0f, viewportHeight, 0.0f);
var forward = new Vector3(0.0f, 0.0f, focalLength);
var lowerLeftCorner = origin - right / 2.0f - up / 2.0f - forward;

var myTestImage = new Image(width, height);

//TODO: Actual Raytracing :)
for(int y = height-1; y >= 0; y--)
{
    Console.WriteLine("Scanlines remaining: {0}", y);
    for (int x = 0; x < width; x++)
    {
        var u = (float) x / (width - 1);
        var v = (float) y / (height - 1);

        var ray = new Ray(origin, lowerLeftCorner + u * right + v * up - origin);

        Vector3 color = rayColor(ray, world);
        myTestImage.SetPixel(y, x, color);
    }
}

Console.WriteLine("Raytracing Done Starting serializing the output file !");

//Serialize the pixels into a file 
using (FileStream fs = File.Open(output, FileMode.Create))
{
    Byte[] outputBuffer = new UTF8Encoding(true).GetBytes(myTestImage.ToString());

    fs.Write(outputBuffer, 0, outputBuffer.Length);
}

Vector3 rayColor(Ray r, ShapeContainer world)
{
    if(world.FindClosestIntersection(r, 0f, float.PositiveInfinity))
    {
        Vector3 sphereNormalColor = new Vector3(
            world.ClosestHit.Normal.X + 1f,
            world.ClosestHit.Normal.Y + 1f,
            world.ClosestHit.Normal.Z + 1f);

        return 0.5f * sphereNormalColor;
    }

    //Normalize the Vector and map its y value axis to a new range (0.0 < y < 1.0, before it was -1.0 < y < 1.0)
    var nDir = Vector3.Normalize(r.Direction);
    var rampTValue = 0.5f * (nDir.Y + 1.0f);

    //Define Two colors a Starting Color and Ending Color.
    var cRampStart = new Vector3(1.0f, 1.0f, 1.0f);
    var cRampEnd = new Vector3(0.5f, 0.7f, 1.0f);

    //Linear Interpolation between the two colors by using our calculated ramp position.
    return (1.0f - rampTValue) * cRampStart + rampTValue * cRampEnd;
}