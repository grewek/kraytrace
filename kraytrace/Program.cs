// See https://aka.ms/new-console-template for more information
using kraytrace;
using kraytrace.LinearAlgebra;
using kraytrace.Shapes;
using kraytrace.Surfaces;

using System;
using System.IO;
using System.Text;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static Random rand = new Random();

        static void Main(string[] args)
        {
            string output = "output.ppm";

            var aspectRatio = 16.0f / 9.0f;
            var width = 400;
            var height = (int)((float)width / aspectRatio);
            var samplesPerPixel = 100;
            var depth = 5;

            var world = new ShapeContainer();

            var groundSurface = new LambertianSurface(new Vector3(.8f, .8f, 0f));
            var surfaceLeft = new MetalSurface(new Vector3(1.0f, 1.0f, 1.0f));
            var surfaceCenter = new LambertianSurface(new Vector3(0.7f, 0.3f, 0.3f));
            var surfaceRight = new MetalSurface(new Vector3(.8f, .6f, .2f));


            world.AddShape(new Sphere(new Vector3(0f, 0f, -1f), .5f, surfaceCenter));
            world.AddShape(new Sphere(new Vector3(-1f, 0f, -1f), .5f, surfaceLeft));
            world.AddShape(new Sphere(new Vector3(1f, 0f, -1f), .5f, surfaceRight));
            world.AddShape(new Sphere(new Vector3(0f, -100.5f, -1f), 100, groundSurface));

            Camera camera = new Camera();

            var myTestImage = new Image(width, height);

            //TODO: Actual Raytracing :)
            for (int y = height - 1; y >= 0; y--)
            {
                Console.WriteLine("Scanlines remaining: {0}", y);
                for (int x = 0; x < width; x++)
                {
                    Vector3 color = new Vector3();
                    for(int sample = 0; sample < samplesPerPixel; sample++)
                    {
                        var u = ((float)x + MathHelpers.RandomFloat()) / (width - 1);
                        var v = ((float)y + MathHelpers.RandomFloat()) / (height - 1);

                        var ray = camera.GetRay(u, v);
                        color += rayColor(ray, world, depth);
                    }

                    myTestImage.SetPixel(y, x, color, samplesPerPixel);
                }
            }

            Console.WriteLine("Raytracing Done Starting serializing the output file !");

            //Serialize the pixels into a file 
            using (FileStream fs = File.Open(output, FileMode.Create))
            {
                Byte[] outputBuffer = new UTF8Encoding(true).GetBytes(myTestImage.ToString());

                fs.Write(outputBuffer, 0, outputBuffer.Length);
            }
        }

        static Vector3 rayColor(Ray r, ShapeContainer world, int depth)
        {
            if(depth <= 0)
            {
                return new Vector3(0f, 0f, 0f);
            }

            HitRecord? rec;
            //NOTE: Prevent Z-Fighting by using a value that is close to zero but __not__ zero !
            if (world.FindClosestIntersection(r, 0.0001f, float.PositiveInfinity, out rec))
            {
                Vector3 attenuation;
                Ray scatterRay;
                if(rec.Value.Material.Scatter(r, rec.Value, out attenuation, out scatterRay))
                {
                    return attenuation * rayColor(scatterRay, world, depth - 1);
                }

                return new Vector3(0f, 0f, 0f);
                //return 0.5f * rayColor(new Ray(rec.Value.Position, target - rec.Value.Position), world, depth - 1);
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

        static float GenrateFloatMinMax(float min, float max)
        {
            return min + (max - min) * (float)rand.NextDouble();
        }
    }
}




