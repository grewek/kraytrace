// See https://aka.ms/new-console-template for more information
using kraytrace;
using System;
using System.IO;
using System.Text;

string output = "output.ppm";



var aspectRatio = 16.0f / 9.0f;
var width = 400;
var height = (int)((float)width / aspectRatio);

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

        Vector3 color = rayColor(ray);
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

Vector3 rayColor(Ray r)
{
    if(hitSphere(new Vector3(0.0f, 0.0f, -1.0f), .5f, r))
    {
        return new Vector3(1.0f, 0.0f, 0.0f);
    }

    //Normalize the Vector and map its y value axis to a new range (0.0 < y < 1.0, before it was -1.0 < y < 1.0)
    var nDir = Vector3.Normalize(r.Direction);
    var rampPosition = 0.5f * (nDir.Y + 1.0f);

    //Define Two colors a Starting Color and Ending Color.
    var cRampStart = new Vector3(1.0f, 1.0f, 1.0f);
    var cRampEnd = new Vector3(0.5f, 0.7f, 1.0f);

    //Linear Interpolation between the two colors by using our calculated ramp position.
    return (1.0f - rampPosition) * cRampStart + rampPosition * cRampEnd;
}

//TODO: There should be a Sphere Class, maybe a good time for using inheritance ? or even better interface composition ?
bool hitSphere(Vector3 center, float radius, Ray r)
{
    //TODO: Refresh my knowledge about Quadratic Equations...
    Vector3 sphereCenter = r.Origin - center;

    var a = Vector3.DotProduct(r.Direction, r.Direction);
    var b = 2.0f * Vector3.DotProduct(sphereCenter, r.Direction);
    var c = Vector3.DotProduct(sphereCenter, sphereCenter) - radius * radius;

    var discriminant = b * b - 4 * a * c;

    return discriminant > 0;
}