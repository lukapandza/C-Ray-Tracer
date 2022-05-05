using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    class Camera
    {
        public double hsize;
        public double vsize;
        public double field_of_view;
        public Matrix transform;
        public double halfWidth;
        public double halfHeight;

        public Camera(double hor, double ver, double fov)
        {
            this.hsize = hor;
            this.vsize = ver;
            this.field_of_view = fov;
            this.transform = Matrix.identity(4, 4);
        }

        public double pixelSize()
        {
            double half_view = Math.Tan(this.field_of_view / 2);
            double aspect = this.hsize / this.vsize;

            if (aspect >= 1)
            {
                this.halfWidth = half_view;
                this.halfHeight = half_view / aspect;
            }
            else
            {
                this.halfWidth = half_view * aspect;
                this.halfHeight = half_view;
            }
            return (this.halfWidth * 2) / this.hsize;
        }

        public static Ray rayForPixel(Camera cam, int px, int py)
        {
            double xoffset = (px + .5) * cam.pixelSize();
            double yoffset = (py + .5) * cam.pixelSize();

            double worldX = cam.halfWidth - xoffset;
            double worldY = cam.halfHeight - yoffset;

            Matrix inv = Matrix.getInverse(cam.transform);
            Tuple.Point pixel = new Tuple.Point((inv * new Tuple(worldX, worldY, -1, 1)).x, (inv * new Tuple(worldX, worldY, -1, 1)).y, (inv * new Tuple(worldX, worldY, -1, 1)).z);
            Tuple.Point origin = new Tuple.Point((inv * new Tuple(0, 0, 0, 1)).x, (inv * new Tuple(0, 0, 0, 1)).y, (inv * new Tuple(0, 0, 0, 1)).z);
            Tuple.Vector direction = (pixel - origin).normalize();

            return new Ray(origin, direction);
        }

        public static Canvas render(Camera cam, World w)
        {
            Canvas image = new Canvas(Convert.ToInt32(cam.hsize), Convert.ToInt32(cam.vsize));
            for(int i = 0; i < cam.vsize; i++)
            {
                for(int j = 0; j < cam.hsize; j++)
                {
                    Ray raymond = Camera.rayForPixel(cam, i, j);
                    Color col = Ray.color_at(w, raymond, 5);
                    Canvas.writePixel(image, i, j, col);
                }
            }
            return image;
        }
    }
}
