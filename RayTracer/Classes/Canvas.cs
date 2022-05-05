using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    class Canvas
    {
        public int w, h;
        public Color[] pixels;

        public Canvas(int W, int H)
        {
            this.w = W;
            this.h = H;

            pixels = new Color[W * H];
            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = new Color(0, 0, 0);
            }
        }

        public static void writePixel(Canvas can, int width, int height, Color col)
        {
            can.pixels[height * can.w + width] = col;
        }

        public static Color pixelAt(Canvas can, int width, int height)
        {
            return can.pixels[height * can.w + width];
        }

        public static String[] canvasToPPM(Canvas can)
        {
            String[] lines = new String[3 + can.h];
            lines[0] = "P3";
            lines[1] = can.w.ToString() + " " + can.h.ToString();
            lines[2] = "255";
            for (int i = 0; i < can.h; i++)
            {
                lines[i + 3] = "";
                for (int j = 0; j < can.w; j++)
                {
                    Color col = pixelAt(can, j, i);

                    int red = Convert.ToInt32(col.r * 255);
                    if (red < 0) { red = 0; }
                    if (red > 255) { red = 255; }
                    lines[i + 3] = lines[i + 3] + red.ToString() + " ";

                    int green = Convert.ToInt32(col.g * 255);
                    if (green < 0) { green = 0; }
                    if (green > 255) { green = 255; }
                    lines[i + 3] = lines[i + 3] + green.ToString() + " ";

                    int blue = Convert.ToInt32(col.b * 255);
                    if (blue < 0) { blue = 0; }
                    if (blue > 255) { blue = 255; }
                    lines[i + 3] = lines[i + 3] + blue.ToString() + " ";
                }
            }
            lines[lines.Length - 1] = lines[lines.Length - 1] + "\n";
            return lines;
        }

        public static void writePPM(Canvas can, String filepath)
        {
            String[] lines = canvasToPPM(can);
            System.IO.File.WriteAllLines(filepath, lines);
        }
    }
}
