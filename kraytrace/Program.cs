// See https://aka.ms/new-console-template for more information
using kraytrace;
using System;
using System.IO;
using System.Text;

string output = "output.ppm";

int width = 256;
int height = 256;

Image myTestImage = new Image(width, height);

//TODO: Actual Raytracing :)
for(int y = height-1; y >= 0; y--)
{
    Console.WriteLine("Scanlines remaining: {0}", y);
    for (int x = 0; x < width; x++)
    {
        var r = (float)x / (width - 1);
        var g = (float)y / (height - 1);
        myTestImage.SetPixel(y, x, r, g, 0.25f);
    }
}

Console.WriteLine("Raytracing Done Starting serializing the output file !");

//Serialize the pixels into a file 
using (FileStream fs = File.Open(output, FileMode.Create))
{
    Byte[] outputBuffer = new UTF8Encoding(true).GetBytes(myTestImage.ToString());

    fs.Write(outputBuffer, 0, outputBuffer.Length);
}

    