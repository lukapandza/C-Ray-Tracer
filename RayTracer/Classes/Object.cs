using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    class Object
    {
        public Material material;
        private Matrix pTransform;
        public Matrix inverse_transform; //saving the inverse of the transform made renders about 5 times faster
        public Matrix transform
        {
            get
            {
                return pTransform;
            }
            set
            {
                inverse_transform = Matrix.getInverse(value);
                pTransform = value;
            }
        }
        

        public Object(Material m, Matrix transform)
        {
            this.material = m;
            this.transform = transform;
        }

        public Object()
        {
            this.material = new Material();
            this.transform = Matrix.identity(4, 4);
        }

        public virtual Tuple.Vector normalAt(Tuple.Point p)
        {
            return new Tuple.Vector(0, 0, 0);
        }

        public virtual Intersection[] intersectWith(Ray raymond)
        {
            return null;
        }

        public void translate(double x, double y, double z)
        {
            Matrix mat = Matrix.getTranslationMatrix(x, y, z);
            this.transform = mat * this.transform;
        }

        public void scale(double x, double y, double z)
        {
            Matrix mat = Matrix.getScalingMatrix(x, y, z);
            this.transform = mat * this.transform;
        }

        public void rotateAroundX(double angle)
        {
            Matrix mat = Matrix.rotationX(angle);
            this.transform = mat * this.transform;
        }

        public void rotateAroundY(double angle)
        {
            Matrix mat = Matrix.rotationY(angle);
            this.transform = mat * this.transform;
        }

        public void rotateAroundZ(double angle)
        {
            Matrix mat = Matrix.rotationZ(angle);
            this.transform = mat * this.transform;
        }

        public static bool operator ==(Object o1, Object o2)
        {
            if (o1.material == o2.material && o1.transform == o2.transform) return true;
            else return false;
        }

        public static bool operator !=(Object o1, Object o2)
        {
            if (o1.material != o2.material || o1.transform != o2.transform) return true;
            else return false;
        }

        public class Sphere : Object
        {

            public Sphere() : base()
            {
                this.material = new Material();
                this.transform = Matrix.identity(4, 4);
            }

            public Sphere(Material m)
            {
                this.material = m;
                this.transform = Matrix.identity(4, 4);
            }

            public override Tuple.Vector normalAt(Tuple.Point p)
            {
                Tuple.Point localPoint = new Tuple.Point((this.inverse_transform * p).x, (this.inverse_transform * p).y, (this.inverse_transform * p).z);
                Tuple.Vector localNormal = localPoint - new Tuple.Point(0, 0, 0);
                Tuple.Vector worldNormal = new Tuple.Vector((Matrix.getTranspose(this.inverse_transform) * localNormal).x, (Matrix.getTranspose(this.inverse_transform) * localNormal).y, (Matrix.getTranspose(this.inverse_transform) * localNormal).z);
                worldNormal.w = 0;
                return worldNormal.normalize();
            }

            public override Intersection[] intersectWith(Ray raymond)
            {
                Ray newRay = Ray.transform(raymond, this.inverse_transform);
                Tuple.Vector o = newRay.origin - new Tuple.Point(0, 0, 0);
                double a = Tuple.Vector.dotProduct(newRay.direction, newRay.direction);
                double b = Tuple.Vector.dotProduct(newRay.direction, o) * 2;
                double c = Tuple.Vector.dotProduct(o, o) - 1;
                double discriminant = (b * b) - (4 * a * c);

                if (discriminant > 0)
                {
                    double[] xs = new double[2];
                    xs[0] = (-b - Math.Sqrt(discriminant)) / (2 * a);
                    xs[1] = (-b + Math.Sqrt(discriminant)) / (2 * a);
                    return new Intersection[2] { new Intersection(xs[0], this, raymond), new Intersection(xs[1], this, raymond) };
                }
                else if (discriminant == 0)
                {
                    double[] xs = new double[2];
                    xs[0] = -b / (2 * a);
                    xs[1] = xs[0];
                    return new Intersection[2] { new Intersection(xs[0], this, raymond), new Intersection(xs[1], this, raymond) };
                }
                else return new Intersection[0];
            }

            public static void set_transform(Sphere inSphere, Matrix newTransform)
            {
                inSphere.transform = newTransform;
            }
        }

        public class Triangle : Object
        {
            public Tuple.Point a;
            public Tuple.Point b;
            public Tuple.Point c;

            public Triangle(Tuple.Point a, Tuple.Point b, Tuple.Point c)
            {
                this.a = a;
                this.b = b;
                this.c = c;
            }

            public Triangle() : base()
            {
                this.a = new Tuple.Point(1, 0, 0);
                this.b = new Tuple.Point(0, 1, 0);
                this.c = new Tuple.Point(0, 0, 1);
            }

            public override Tuple.Vector normalAt(Tuple.Point p)
            {
                Tuple.Point localPoint = new Tuple.Point((this.inverse_transform * p).x, (this.inverse_transform * p).y, (this.inverse_transform * p).z);
                Tuple.Vector localNormal = Tuple.Vector.crossProduct((this.b - this.a), (this.c - this.a));
                Tuple.Vector worldNormal = new Tuple.Vector((Matrix.getTranspose(this.inverse_transform) * localNormal).x, (Matrix.getTranspose(this.inverse_transform) * localNormal).y, (Matrix.getTranspose(this.inverse_transform) * localNormal).z);
                worldNormal.w = 0;
                return worldNormal.normalize();
            }

            public override Intersection[] intersectWith(Ray ray)
            {
                double beta;
                double gamma;
                double t;

                Ray raymond = Ray.transform(ray, this.inverse_transform);

                beta = Matrix.getDeterminant(new Matrix(new double[3, 3] { { (this.a.x - raymond.origin.x), (this.a.x - this.c.x), (raymond.direction.x) }, { (this.a.y - raymond.origin.y), (this.a.y - this.c.y), (raymond.direction.y) }, { (this.a.z - raymond.origin.z), (this.a.z - this.c.z), (raymond.direction.z) } })) / Matrix.getDeterminant(new Matrix(new double[3, 3] { { (this.a.x - this.b.x), (this.a.x - this.c.x), (raymond.direction.x) }, { (this.a.y - this.b.y), (this.a.y - this.c.y), (raymond.direction.y) }, { (this.a.z - this.b.z), (this.a.z - this.c.z), (raymond.direction.z) } }));
                gamma = Matrix.getDeterminant(new Matrix(new double[3, 3] { { (this.a.x - this.b.x), (this.a.x - raymond.origin.x), (raymond.direction.x) }, { (this.a.y - this.b.y), (this.a.y - raymond.origin.y), (raymond.direction.y) }, { (this.a.z - this.b.z), (this.a.z - raymond.origin.z), (raymond.direction.z) } })) / Matrix.getDeterminant(new Matrix(new double[3, 3] { { (this.a.x - this.b.x), (this.a.x - this.c.x), (raymond.direction.x) }, { (this.a.y - this.b.y), (this.a.y - this.c.y), (raymond.direction.y) }, { (this.a.z - this.b.z), (this.a.z - this.c.z), (raymond.direction.z) } }));
                t = Matrix.getDeterminant(new Matrix(new double[3, 3] { { (this.a.x - this.b.x), (this.a.x - this.c.x), (this.a.x - raymond.origin.x) }, { (this.a.y - this.b.y), (this.a.y - this.c.y), (this.a.y - raymond.origin.y) }, { (this.a.z - this.b.z), (this.a.z - this.c.z), (this.a.z - raymond.origin.z) } })) / Matrix.getDeterminant(new Matrix(new double[3, 3] { { (this.a.x - this.b.x), (this.a.x - this.c.x), (raymond.direction.x) }, { (this.a.y - this.b.y), (this.a.y - this.c.y), (raymond.direction.y) }, { (this.a.z - this.b.z), (this.a.z - this.c.z), (raymond.direction.z) } }));

                if (beta >= 0 && gamma >= 0 && beta + gamma <= 1) return new Intersection[1] { new Intersection(t, this, raymond) };
                else return new Intersection[0];
            }
        }

        public class Plane : Object
        {
            public Plane() : base()
            {
            }

            public override Tuple.Vector normalAt(Tuple.Point p)
            {
                Tuple.Point localPoint = new Tuple.Point((this.inverse_transform * p).x, (this.inverse_transform * p).y, (this.inverse_transform * p).z);
                Tuple.Vector localNormal = new Tuple.Vector(0, 1, 0);
                Tuple.Vector worldNormal = new Tuple.Vector((Matrix.getTranspose(this.inverse_transform) * localNormal).x, (Matrix.getTranspose(this.inverse_transform) * localNormal).y, (Matrix.getTranspose(this.inverse_transform) * localNormal).z);
                worldNormal.w = 0;
                return worldNormal.normalize();
            }

            public override Intersection[] intersectWith(Ray ray)
            {
                Ray raymond = Ray.transform(ray, Matrix.getInverse(this.transform));
                if (Math.Abs(raymond.direction.y) < Constants.EPSILON) return new Intersection[0];
                else 
                {
                    double t = -raymond.origin.y / raymond.direction.y;
                    return new Intersection[1] { new Intersection(t, this, ray) };
                }
            }
        }
    }
}
