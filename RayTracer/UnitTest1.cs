using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RayTracer
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test1()
        {
            Tuple tuple1 = new Tuple(4.3, -4.2, 3.1, 1.0);
            Assert.AreEqual(tuple1.x, 4.3);
            Assert.AreEqual(tuple1.y, -4.2);
            Assert.AreEqual(tuple1.z, 3.1);
            Assert.AreEqual(tuple1.w, 1.0);
            Assert.IsTrue(tuple1.isPoint);
            Assert.IsFalse(tuple1.isVector);
        }
        [TestMethod]
        public void Test2()
        {
            Tuple tuple1 = new Tuple(4.3, -4.2, 3.1, 0.0);
            Assert.AreEqual(tuple1.x, 4.3);
            Assert.AreEqual(tuple1.y, -4.2);
            Assert.AreEqual(tuple1.z, 3.1);
            Assert.AreEqual(tuple1.w, 0.0);
            Assert.IsFalse(tuple1.isPoint);
            Assert.IsTrue(tuple1.isVector);
        }
        [TestMethod]
        public void Test3()
        {
            Tuple tuple1 = new Tuple.Point(4, -4, 3);
            Assert.AreEqual(tuple1.x, 4);
            Assert.AreEqual(tuple1.y, -4);
            Assert.AreEqual(tuple1.z, 3);
            Assert.AreEqual(tuple1.w, 1.0);
            Assert.IsTrue(tuple1.isPoint);
            Assert.IsFalse(tuple1.isVector);
        }
        [TestMethod]
        public void Test4()
        {
            Tuple tuple1 = new Tuple.Vector(4, -4, 3);
            Assert.AreEqual(tuple1.x, 4);
            Assert.AreEqual(tuple1.y, -4);
            Assert.AreEqual(tuple1.z, 3);
            Assert.AreEqual(tuple1.w, 0.0);
            Assert.IsFalse(tuple1.isPoint);
            Assert.IsTrue(tuple1.isVector);
        }
        [TestMethod]
        public void Test5()
        {
            Tuple tuple1 = new Tuple(3, -2, 5, 1);
            Tuple tuple2 = new Tuple(-2, 3, 1, 0);
            Tuple tuple3 = tuple1 + tuple2;
            Assert.AreEqual(tuple3.x, 1);
            Assert.AreEqual(tuple3.y, 1);
            Assert.AreEqual(tuple3.z, 6);
            Assert.AreEqual(tuple3.w, 1);
        }

        [TestMethod]
        public void Test6()
        {
            Tuple.Point tuple1 = new Tuple.Point(3, 2, 1);
            Tuple.Point tuple2 = new Tuple.Point(5, 6, 7);
            Tuple.Vector tuple3 = tuple1 - tuple2;
            Assert.AreEqual(tuple3.x, -2);
            Assert.AreEqual(tuple3.y, -4);
            Assert.AreEqual(tuple3.z, -6);
            Assert.AreEqual(tuple3.w, 0);
            Assert.IsTrue(tuple1 == new Tuple.Point(3, 2, 1));
        }

        [TestMethod]
        public void Test7() // subtracting vector from point
        {
            Tuple.Point tuple1 = new Tuple.Point(3, 2, 1);
            Tuple.Vector tuple2 = new Tuple.Vector(5, 6, 7);
            Tuple.Point tuple3 = tuple1 - tuple2;
            Assert.AreEqual(tuple3.x, -2);
            Assert.AreEqual(tuple3.y, -4);
            Assert.AreEqual(tuple3.z, -6);
            Assert.AreEqual(tuple3.w, 1);
        }

        [TestMethod]
        public void Test8() // subtracting vector from vector
        {
            Tuple.Vector tuple1 = new Tuple.Vector(3, 2, 1);
            Tuple.Vector tuple2 = new Tuple.Vector(5, 6, 7);
            Tuple.Vector tuple3 = tuple1 - tuple2;
            Assert.AreEqual(tuple3.x, -2);
            Assert.AreEqual(tuple3.y, -4);
            Assert.AreEqual(tuple3.z, -6);
            Assert.AreEqual(tuple3.w, 0);
        }

        [TestMethod]
        public void Test9() // subtracting vector from zero vector
        {
            Tuple.Vector tuple1 = new Tuple.Vector(0, 0, 0);
            Tuple.Vector tuple2 = new Tuple.Vector(5, 6, 7);
            Tuple.Vector tuple3 = tuple1 - tuple2;
            Assert.AreEqual(tuple3.x, -5);
            Assert.AreEqual(tuple3.y, -6);
            Assert.AreEqual(tuple3.z, -7);
            Assert.AreEqual(tuple3.w, 0);
        }

        [TestMethod]
        public void Test10() // negating a tuple
        {
            Tuple tuple1 = new Tuple(1, -2, 3, -4);
            Tuple tuple2 = -tuple1;
            Assert.AreEqual(tuple2.x, -1);
            Assert.AreEqual(tuple2.y, 2);
            Assert.AreEqual(tuple2.z, -3);
            Assert.AreEqual(tuple2.w, 4);
        }

        [TestMethod]
        public void Test11() // multiplying tuple by scalar
        {
            Tuple tuple1 = new Tuple(1, -2, 3, -4);
            Tuple tuple2 = 3.5 * tuple1;
            Assert.AreEqual(tuple2.x, 3.5);
            Assert.AreEqual(tuple2.y, -7);
            Assert.AreEqual(tuple2.z, 10.5);
            Assert.AreEqual(tuple2.w, -14);
        }

        [TestMethod]
        public void Test12() // multiplying tuple by fraction
        {
            Tuple tuple1 = new Tuple(1, -2, 3, -4);
            Tuple tuple2 = 0.5 * tuple1;
            Assert.AreEqual(tuple2.x, 0.5);
            Assert.AreEqual(tuple2.y, -1);
            Assert.AreEqual(tuple2.z, 1.5);
            Assert.AreEqual(tuple2.w, -2);
        }

        [TestMethod]
        public void Test13() // dividing a tuple by scalar
        {
            Tuple tuple1 = new Tuple(1, -2, 3, -4);
            Tuple tuple2 = tuple1 / 2;
            Assert.AreEqual(tuple2.x, 0.5);
            Assert.AreEqual(tuple2.y, -1);
            Assert.AreEqual(tuple2.z, 1.5);
            Assert.AreEqual(tuple2.w, -2);
        }

        [TestMethod]
        public void Test14() // computing the magnitude of a vector 1
        {
            Tuple.Vector vec = new Tuple.Vector(1, 0, 0);
            double mag = vec.magnitude();
            Assert.AreEqual(mag, 1);
        }

        [TestMethod]
        public void Test15() // computing the magnitude of a vector 2
        {
            Tuple.Vector vec = new Tuple.Vector(0, 1, 0);
            double mag = vec.magnitude();
            Assert.AreEqual(mag, 1);
        }

        [TestMethod]
        public void Test16() // computing the magnitude of a vector 3
        {
            Tuple.Vector vec = new Tuple.Vector(0, 0, 1);
            double mag = vec.magnitude();
            Assert.AreEqual(mag, 1);
        }

        [TestMethod]
        public void Test17() // computing the magnitude of a vector 4
        {
            Tuple.Vector vec = new Tuple.Vector(1, 2, 3);
            double mag = vec.magnitude();
            Assert.AreEqual(mag, Math.Sqrt(14));
        }

        [TestMethod]
        public void Test18() // computing the magnitude of a vector 5
        {
            Tuple.Vector vec = new Tuple.Vector(-1, -2, -3);
            double mag = vec.magnitude();
            Assert.AreEqual(mag, Math.Sqrt(14));
        }

        [TestMethod]
        public void Test19() // normalize
        {
            Tuple.Vector vec = new Tuple.Vector(4, 0, 0);
            Tuple.Vector norm = vec.normalize();
            Assert.AreEqual(norm.x, 1);
            Assert.AreEqual(norm.y, 0);
            Assert.AreEqual(norm.z, 0);
            Assert.IsTrue(norm.isVector);
        }

        [TestMethod]
        public void Test20() // normalize 2
        {
            Tuple.Vector vec = new Tuple.Vector(1, 2, 3);
            Tuple.Vector norm = vec.normalize();
            Assert.AreEqual(norm.x, 1 / Math.Sqrt(14));
            Assert.AreEqual(norm.y, 2 / Math.Sqrt(14));
            Assert.AreEqual(norm.z, 3 / Math.Sqrt(14));
            Assert.IsTrue(norm.isVector);
        }

        [TestMethod]
        public void Test21() // magnitude of normalized vector
        {
            Tuple.Vector vec = new Tuple.Vector(1, 2, 3);
            Tuple.Vector norm = vec.normalize();
            double mag = norm.magnitude();
            Assert.AreEqual(mag, 1);
        }

        [TestMethod]
        public void Test22() // dot product
        {
            Tuple.Vector v1 = new Tuple.Vector(1, 2, 3);
            Tuple.Vector v2 = new Tuple.Vector(2, 3, 4);
            double dP = Tuple.Vector.dotProduct(v1, v2);
            Assert.AreEqual(dP, 20);
        }

        [TestMethod]
        public void Test23() // cross product
        {
            Tuple.Vector v1 = new Tuple.Vector(1, 2, 3);
            Tuple.Vector v2 = new Tuple.Vector(2, 3, 4);
            Tuple.Vector c12 = Tuple.Vector.crossProduct(v1, v2);
            Assert.AreEqual(c12.x, -1);
            Assert.AreEqual(c12.y, 2);
            Assert.AreEqual(c12.z, -1);
            Tuple.Vector c21 = Tuple.Vector.crossProduct(v2, v1);
            Assert.AreEqual(c21.x, 1);
            Assert.AreEqual(c21.y, -2);
            Assert.AreEqual(c21.z, 1);
        }

        [TestMethod]
        public void Test24() // color decleration
        {
            Color c = new Color(-.5, .4, 1.7);
            Assert.AreEqual(c.r, -.5);
            Assert.AreEqual(c.g, .4);
            Assert.AreEqual(c.b, 1.7);
        }

        [TestMethod]
        public void Test25() // color addition
        {
            Color c1 = new Color(.9, .6, .75);
            Color c2 = new Color(.7, .1, .25);
            Color cSum = c1 + c2;
            Assert.AreEqual(cSum.r, 1.6);
            Assert.AreEqual(cSum.g, .7);
            Assert.AreEqual(cSum.b, 1);
        }

        [TestMethod]
        public void Test26() // color subtraction
        {
            Color c1 = new Color(.9, .6, .75);
            Color c2 = new Color(.7, .1, .25);
            Color cSub = c1 - c2;
            Assert.IsTrue(Math.Abs(cSub.r - .2) < 0.00001);
            Assert.IsTrue(Math.Abs(cSub.g - .5) < 0.00001);
            Assert.IsTrue(Math.Abs(cSub.b - .5) < 0.00001);
        }

        [TestMethod]
        public void Test27() // color*scalar
        {
            Color c = new Color(.2, .3, .4);
            Color cK = c * 2;
            Assert.AreEqual(cK.r, .4);
            Assert.AreEqual(cK.g, .6);
            Assert.AreEqual(cK.b, .8);
            Assert.IsTrue(cK == new Color(.4, .6, .8));
            //Assert.AreEqual(cK, new Color(.4, .6, .8));
        }

        [TestMethod]
        public void Test28() // color * color
        {
            Color c1 = new Color(1, .2, .4);
            Color c2 = new Color(.9, 1, .1);
            Color c = c1 * c2;
            //Assert.IsTrue(c1 == c2); FIX
        }

        [TestMethod]
        public void Test29() // declare a canvas
        {
            Canvas c = new Canvas(10, 20);
            Assert.AreEqual(c.w, 10);
            Assert.AreEqual(c.h, 20);
            Assert.IsTrue(c.pixels[0].r == 0);
            Assert.IsTrue(c.pixels[71].b == 0);
        }

        [TestMethod]
        public void Test30() // read/write pixel to canvas
        {
            Canvas c = new Canvas(10, 20);
            Color red = new Color(1, 0, 0);
            Canvas.writePixel(c, 2, 3, red);
            Assert.IsTrue(Canvas.pixelAt(c, 2, 3) == red);
        }

        [TestMethod]
        public void Test31() // canvas to ppm
        {
            Canvas c = new Canvas(5, 3);
            String[] str = Canvas.canvasToPPM(c);
            Assert.AreEqual(str[0], "P3");
            Assert.AreEqual(str[1], "5 3");
            Assert.AreEqual(str[2], "255");
        }

        [TestMethod]
        public void Test32() // canvas to ppm 2
        {
            Canvas c = new Canvas(5, 3);
            Color c1 = new Color(1.5, 0, 0);
            Color c2 = new Color(0, 0.5, 0);
            Color c3 = new Color(-0.5, 0, 1);
            Canvas.writePixel(c, 0, 0, c1);
            Canvas.writePixel(c, 2, 1, c2);
            Canvas.writePixel(c, 4, 2, c3);
            String[] str = Canvas.canvasToPPM(c);
            Assert.AreEqual(str[3], "255 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ");
            Assert.AreEqual(str[4], "0 0 0 0 0 0 0 128 0 0 0 0 0 0 0 ");
            Assert.AreEqual(str[5], "0 0 0 0 0 0 0 0 0 0 0 0 0 0 255 \n");
        }

        [TestMethod]
        public void Test33() // ppm ends with newline
        {
            Canvas c = new Canvas(5, 3);
            String[] str = Canvas.canvasToPPM(c);
            Assert.IsTrue(str.Length == 6);
        }

        [TestMethod]
        public void Test34() // creating ray
        {
            Tuple.Point origin = new Tuple.Point(1, 2, 3);
            Tuple.Vector direction = new Tuple.Vector(4, 5, 6);
            Ray ray1 = new Ray(origin, direction);
            Assert.AreEqual(ray1.origin, origin);
            Assert.AreEqual(ray1.direction, direction);
        }

        [TestMethod]
        public void Test35() // position()
        {
            Tuple.Point o = new Tuple.Point(2, 3, 4);
            Tuple.Vector d = new Tuple.Vector(1, 0, 0);
            Ray ray = new Ray(o, d);

            Assert.IsTrue(Ray.position(ray, 0) == new Tuple.Point(2, 3, 4));
            /*Tuple.Point p1 = Ray.position(ray, 0);
            Tuple.Point p2 = new Tuple.Point(2, 3, 4);
            Assert.AreEqual(p1.x, p2.x);
            Assert.AreEqual(p1.y, p2.y);
            Assert.AreEqual(p1.z, p2.z);*/
            Assert.IsTrue(Ray.position(ray, 1) == new Tuple.Point(3, 3, 4));
            /*Tuple.Point p3 = Ray.position(ray, 1);
            Tuple.Point p4 = new Tuple.Point(3, 3, 4);
            Assert.AreEqual(p3.x, p4.x);
            Assert.AreEqual(p3.y, p4.y);
            Assert.AreEqual(p3.z, p4.z);*/
            Assert.IsTrue(Ray.position(ray, -1) == new Tuple.Point(1, 3, 4));
            /*Tuple.Point p5 = Ray.position(ray, -1);
            Tuple.Point p6 = new Tuple.Point(1, 3, 4);
            Assert.AreEqual(p5.x, p6.x);
            Assert.AreEqual(p5.y, p6.y);
            Assert.AreEqual(p5.z, p6.z);*/
            Assert.IsTrue(Ray.position(ray, 2.5) == new Tuple.Point(4.5, 3, 4));
            /*Tuple.Point p7 = Ray.position(ray, 2.5);
            Tuple.Point p8 = new Tuple.Point(4.5, 3, 4);
            Assert.AreEqual(p7.x, p8.x);
            Assert.AreEqual(p7.y, p8.y);
            Assert.AreEqual(p7.z, p8.z);*/
        }

        [TestMethod]
        public void Test36() // ray intersects sphere at 2 points
        {
            Ray ray = new Ray(new Tuple.Point(0, 0, -5), new Tuple.Vector(0, 0, 1));
            Object.Sphere s = new Object.Sphere(new Material(new Color(0, 0, 0), 1, 1, 1, 1, 1, 1, 1));
            Intersection[] xs = s.intersectWith(ray);
            Assert.AreEqual(xs.Length, 2);
            Assert.AreEqual(xs[0].t, 4.0);
            Assert.AreEqual(xs[1].t, 6.0);
        }

        [TestMethod]
        public void Test37() // ray intersects sphere at tangent
        {
            Ray ray = new Ray(new Tuple.Point(0, 1, -5), new Tuple.Vector(0, 0, 1));
            Object.Sphere s = new Object.Sphere(new Material(new Color(0, 0, 0), 1, 1, 1, 1, 1, 1, 1));
            Intersection[] xs = s.intersectWith(ray);
            Assert.AreEqual(xs.Length, 2);
            Assert.AreEqual(xs[0].t, 5.0);
            Assert.AreEqual(xs[1].t, 5.0);
        }

        [TestMethod]
        public void Test38() // ray misses sphere
        {
            Ray ray = new Ray(new Tuple.Point(0, 2, -5), new Tuple.Vector(0, 0, 1));
            Object.Sphere s = new Object.Sphere(new Material(new Color(0, 0, 0), 1, 1, 1, 1, 1, 1, 11));
            Intersection[] xs = s.intersectWith(ray);
            Assert.AreEqual(xs.Length, 0);
        }

        [TestMethod]
        public void Test39() // ray originates inside the sphere
        {
            Ray ray = new Ray(new Tuple.Point(0, 0, 0), new Tuple.Vector(0, 0, 1));
            Object.Sphere s = new Object.Sphere(new Material(new Color(0, 0, 0), 1, 1, 1, 1, 1, 1, 1));
            Intersection[] xs = s.intersectWith(ray);
            Assert.AreEqual(xs.Length, 2);
            Assert.AreEqual(xs[0].t, -1.0);
            Assert.AreEqual(xs[1].t, 1.0);
        }

        [TestMethod]
        public void Test40() // sphere is behind ray
        {
            Ray ray = new Ray(new Tuple.Point(0, 0, 5), new Tuple.Vector(0, 0, 1));
            Object.Sphere s = new Object.Sphere(new Material(new Color(0, 0, 0), 1, 1, 1, 1, 1, 1, 1));
            Intersection[] xs = s.intersectWith(ray);
            Assert.AreEqual(xs.Length, 2);
            Assert.AreEqual(xs[0].t, -6.0);
            Assert.AreEqual(xs[1].t, -4.0);
        }

        [TestMethod]
        public void Test41() // normal on a sphere 1
        {
            Object.Sphere s = new Object.Sphere(new Material(new Color(0, 0, 0), 1, 1, 1, 1, 1, 1, 1));
            Tuple.Vector norm = s.normalAt(new Tuple.Point(1, 0, 0));
            Assert.IsTrue(norm == new Tuple.Vector(1, 0, 0));
        }

        [TestMethod]
        public void Test42() // normal on a sphere 2
        {
            Object.Sphere s = new Object.Sphere(new Material(new Color(0, 0, 0), 1, 1, 1, 1, 1, 1, 1));
            Tuple.Vector norm = s.normalAt(new Tuple.Point(0, 1, 0));
            Assert.IsTrue(norm == new Tuple.Vector(0, 1, 0));
        }

        [TestMethod]
        public void Test43() // normal on a sphere 3
        {
            Object.Sphere s = new Object.Sphere(new Material(new Color(0, 0, 0), 1, 1, 1, 1, 1, 1, 1));
            Tuple.Vector norm = s.normalAt(new Tuple.Point(0, 0, 1));
            Assert.IsTrue(norm == new Tuple.Vector(0, 0, 1));
        }

        [TestMethod]
        public void Test44() // normal on a sphere 4
        {
            Object.Sphere s = new Object.Sphere(new Material(new Color(0, 0, 0), 1, 1, 1, 1, 1, 1, 1));
            Tuple.Vector norm = s.normalAt(new Tuple.Point(Math.Sqrt(3) / 3, Math.Sqrt(3) / 3, Math.Sqrt(3) / 3));
            Assert.IsTrue(norm == new Tuple.Vector(Math.Sqrt(3) / 3, Math.Sqrt(3) / 3, Math.Sqrt(3) / 3));
        }

        [TestMethod]
        public void Test45() // normal is normalized
        {
            Object.Sphere s = new Object.Sphere(new Material(new Color(0, 0, 0), 1, 1, 1, 1, 1, 1, 1));
            Tuple.Vector norm = s.normalAt(new Tuple.Point(Math.Sqrt(3) / 3, Math.Sqrt(3) / 3, Math.Sqrt(3) / 3));
            Assert.IsTrue(norm == norm.normalize());
        }
        [TestMethod]
        public void Test46() // normal is normalized
        {
            Object.Sphere s = new Object.Sphere(new Material(new Color(0, 0, 1), 1, 1, 1, 1, 1, 1, 1));
            Material m = new Material(new Color(0, 0, 1), 1, 1, 1, 1, 1, 1, 1);
            Assert.IsTrue(m == s.material);
        }
        [TestMethod]
        public void Test47() // normal is normalized
        {
            Light light = Light.point_light(new Tuple.Point(0, 0, 0), new Color(1, 1, 1));
            Assert.IsTrue(light.position == new Tuple.Point(0, 0, 0));
            Assert.IsTrue(light.intensity == new Color(1, 1, 1));
        }
        [TestMethod]
        public void Test48() // material
        {
            Material mat = new Material();
            Assert.IsTrue(mat.color == new Color(1, 1, 1));
            Assert.IsTrue(mat.diffuse == .9);
        }
        [TestMethod]
        public void Test49() // lighting 1
        {
            Material mat = new Material();
            Tuple.Point position = new Tuple.Point(0, 0, 0);
            Tuple.Vector eyev = new Tuple.Vector(0, (Math.Sqrt(2) / 2), (-1 * Math.Sqrt(2) / 2));
            Tuple.Vector norm = new Tuple.Vector(0, 0, -1);
            Light light = Light.point_light(new Tuple.Point(0, 0, -10), new Color(1, 1, 1));
            Color outCol = Light.lighting(mat, light, position, eyev, norm, false, null);
            Assert.IsTrue(outCol == new Color(1, 1, 1));
        }
        [TestMethod]
        public void Test50() // lighting 2
        {
            Material mat = new Material();
            Tuple.Point position = new Tuple.Point(0, 0, 0);
            Tuple.Vector eyev = new Tuple.Vector(0, 0, -1);
            Tuple.Vector norm = new Tuple.Vector(0, 0, -1);
            Light light = Light.point_light(new Tuple.Point(0, 10, -10), new Color(1, 1, 1));
            Color outCol = Light.lighting(mat, light, position, eyev, norm, false, null);
            Assert.IsTrue(outCol == new Color(.7364, .7364, .7364));
            //Assert.AreEqual(outCol.r, .7364);
        }
        [TestMethod]
        public void Test51() // lighting 3
        {
            Material mat = new Material();
            Tuple.Point position = new Tuple.Point(0, 0, 0);
            Tuple.Vector eyev = new Tuple.Vector(0, 0, -1);
            Tuple.Vector norm = new Tuple.Vector(0, 0, -1);
            Light light = Light.point_light(new Tuple.Point(0, 0, 10), new Color(1, 1, 1));
            Color outCol = Light.lighting(mat, light, position, eyev, norm, false, null);
            Assert.IsTrue(outCol == new Color(0.1, 0.1, 0.1));
            //Assert.AreEqual(outCol.r, 0);
        }
        [TestMethod]
        public void Test52() // matrix 4x4
        {
            double[,] e = new double[4, 4] { { 1, 2, 3, 4 }, { 5.5, 6.5, 7.5, 8.5 }, { 9, 10, 11, 12 }, { 0, 0, 0, 1 } };
            Matrix momo = new Matrix(e);
            Assert.AreEqual(momo.values[0, 0], 1);
            Assert.AreEqual(momo.values[0, 3], 4);
            Assert.AreEqual(momo.values[1, 0], 5.5);
            Assert.AreEqual(momo.values[1, 2], 7.5);
            Assert.AreEqual(momo.values[2, 2], 11);
            Assert.AreEqual(momo.values[3, 0], 0);
            Assert.AreEqual(momo.values[3, 3], 1);
        }
        [TestMethod]
        public void Test53() // matrix 2x2
        {
            double[,] e = new double[2, 2] { { -3, 5 }, { 1, -2 } };
            Matrix momo = new Matrix(e);
            Assert.AreEqual(momo.values[0, 0], -3);
            Assert.AreEqual(momo.values[0, 1], 5);
            Assert.AreEqual(momo.values[1, 0], 1);
            Assert.AreEqual(momo.values[1, 1], -2);
        }
        [TestMethod]
        public void Test54() // matrix 3x3
        {
            double[,] e = new double[3, 3] { { -3, 5, 0 }, { 1, -2, -7 }, { 0, 1, 1 } };
            Matrix momo = new Matrix(e);
            Assert.AreEqual(momo.values[0, 0], -3);
            Assert.AreEqual(momo.values[1, 1], -2);
            Assert.AreEqual(momo.values[2, 2], 1);
        }
        [TestMethod]
        public void Test55() // matrix equality
        {
            double[,] e = new double[4, 4] { { 1, 2, 3, 4 }, { 5, 6, 7, 8 }, { 9, 8, 7, 6 }, { 0, 0, 0, 1 } };
            Matrix m1 = new Matrix(e);
            Matrix m2 = new Matrix(e);
            Assert.IsTrue(m1 == m2);
        }
        [TestMethod]
        public void Test56() // matrix inequality
        {
            double[,] e1 = new double[4, 4] { { 1, 2, 3, 4 }, { 5, 6, 7, 8 }, { 9, 8, 7, 6 }, { 0, 0, 0, 1 } };
            double[,] e2 = new double[4, 4] { { 2, 3, 4, 5 }, { 6, 7, 8, 9 }, { 8, 7, 6, 5 }, { 0, 0, 0, 1 } };
            Matrix m1 = new Matrix(e1);
            Matrix m2 = new Matrix(e2);
            Assert.IsTrue(m1 != m2);
        }
        [TestMethod]
        public void Test57() // matrix multiplication
        {
            double[,] e1 = new double[4, 4] { { 1, 2, 3, 4 }, { 5, 6, 7, 8 }, { 9, 8, 7, 6 }, { 0, 0, 0, 1 } };
            double[,] e2 = new double[4, 4] { { -2, 1, 2, 3 }, { 3, 2, 1, -1 }, { 4, 3, 6, 5 }, { 0, 0, 0, 1 } };
            double[,] e3 = new double[4, 4] { { 16, 14, 22, 20 }, { 36, 38, 58, 52 }, { 34, 46, 68, 60 }, { 0, 0, 0, 1 } };
            Matrix m1 = new Matrix(e1);
            Matrix m2 = new Matrix(e2);
            Matrix check = new Matrix(e3);
            Assert.IsTrue((m1 * m2) == check);
        }
        [TestMethod]
        public void Test58() // matrix x tuple
        {
            double[,] e1 = new double[4, 4] { { 1, 2, 3, 4 }, { 2, 4, 4, 2 }, { 8, 6, 4, 1 }, { 0, 0, 0, 1 } };
            Matrix m1 = new Matrix(e1);
            Tuple res = m1 * new Tuple(1, 2, 3, 1);
            Assert.IsTrue(res == new Tuple(18, 24, 33, 1));
        }
        [TestMethod]
        public void Test59() // multiplication by identity matrix
        {
            double[,] e1 = new double[4, 4] { { 0, 1, 2, 4 }, { 1, 2, 4, 8 }, { 2, 4, 8, 16 }, { 0, 0, 0, 1 } };
            Matrix m1 = new Matrix(e1);
            Matrix m2 = Matrix.identity(m1.height, m1.width);
            Assert.IsTrue((m1 * m2) == m1);
        }
        [TestMethod]
        public void Test60() // identity matrix x tuple
        {
            Matrix m1 = Matrix.identity(4, 4);
            Tuple res = m1 * new Tuple(1, 2, 3, 4);
            Assert.IsTrue(res == new Tuple(1, 2, 3, 4));
        }
        [TestMethod]
        public void Test61() // determinant 2x2
        {
            Matrix m1 = new Matrix(new double[2, 2] { { 1, 5 }, { -3, 2 } });
            Assert.AreEqual(Matrix.getDeterminant(m1), 17);
        }
        [TestMethod]
        public void Test62() // get submatrix
        {
            Matrix m1 = new Matrix(new double[3, 3] { { 1, 5, 0 }, { -3, 2, 7 }, { 0, 6, -3} });
            Matrix m2 = Matrix.getSubmatrix(m1, 1, 2);
            Assert.IsTrue(m2 == new Matrix(new double[2, 2] { { 1, 5 }, { 0, 6 } }));
        }
        [TestMethod]
        public void Test63() // get minor
        {
            Matrix m1 = new Matrix(new double[3, 3] { { 3, 5, 0 }, { 2, -1, -7 }, { 6, -1, 5 } });
           double c = Matrix.getMinor(m1, 1, 0);
            Assert.IsTrue(c == 25);
        }

        [TestMethod]
        public void Test64() // get cofactor
        {
            Matrix m1 = new Matrix(new double[3, 3] { { 3, 5, 0 }, { 2, -1, -7 }, { 6, -1, 5 } });
            double c = Matrix.getCofactor(m1, 0, 0);
            Assert.IsTrue(c == -12);
            double k = Matrix.getCofactor(m1, 1, 0);
            Assert.IsTrue(k == -25);
        }
        [TestMethod]
        public void Test65() // get cofactor
        {
            Matrix m1 = new Matrix(new double[3, 3] { { 1, 2, 6 }, { -5, 8, -4 }, { 2, 6, 4 } });
            double c = Matrix.getCofactor(m1, 0, 0);
            double k = Matrix.getCofactor(m1, 0, 1);
            double t = Matrix.getCofactor(m1, 0, 2);
            double d = Matrix.getDeterminant(m1);
            Assert.IsTrue(c == 56);
            Assert.IsTrue(k == 12);
            Assert.IsTrue(t == -46);
            Assert.IsTrue(d == -196);
        }
        [TestMethod]
        public void Test66() // get determinant
        {
            Matrix m1 = new Matrix(new double[4, 4] { { -2, -8, 3, 5 }, { -3, 1, 7, 3 }, { 1, 2, -9, 6 }, { 0, 0, 0, 1}});
            double d = Matrix.getDeterminant(m1);
            Assert.IsTrue(d == 185);
        }
        [TestMethod]
        public void Test67() // get invertible
        {
            Matrix m1 = new Matrix(new double[4, 4] { { 6, 4, 4, 4 }, { 5, 5, 7, 6 }, { 4, -9, 3, -7 }, { 0, 0, 0, 1 } });
            double d = Matrix.getDeterminant(m1);
            Assert.IsTrue(d == 260);
            Assert.IsTrue(m1.invertible);
        }
        [TestMethod]
        public void Test68() // get invertible
        {
            Matrix m1 = new Matrix(new double[4, 4] { { -4, 2, 0, -3 }, { 9, 6, 0, 6 }, { 0, -5, 0, -5 }, { 0, 0, 0, 1 } });
            double d = Matrix.getDeterminant(m1);
            Assert.IsTrue(d == 0);
            Assert.IsFalse(m1.invertible);
        }
        [TestMethod]
        public void Test69() // get inverse
        {
            Matrix mCheck = new Matrix(new double[4, 4] { { 0.14110, 0.33129, 0.19632, -0.14724 }, { 0.07975, -0.07362, 0.06748, 1.69939 }, { 0.25767, 0.30061, 0.14110, 0.64417 }, { 0.0, 0.0, 0.0, 1.0 } });
            Matrix m1 = new Matrix(new double[4, 4] { { -5, 2, 6, -8 }, { 1, -5, 1, 8 }, { 7, 7, -6, -7 }, { 0, 0, 0, 1 } });
            Matrix m2 = Matrix.getInverse(m1);
            Assert.IsTrue(m2 == mCheck);
        }
        [TestMethod]
        public void Test70() // multiplying product with inverse
        {
            Matrix m1 = new Matrix(new double[4, 4] { { 3, -9, 7, 3 }, { 3, -8, 2, -9 }, { -4, 4, 4, 1 }, { 0, 0, 0, 1 } });
            Matrix m2 = new Matrix(new double[4, 4] { { 8, 2, 2, 2 }, { 3, -1, 7, 0 }, { 7, 0, 5, 4 }, { 0, 0, 0, 1 } });
            Assert.IsTrue((m1 * m2) * Matrix.getInverse(m2) == m1);
        }
        [TestMethod]
        public void Test71() // translating ray
        {
            Ray inRay = new Ray(new Tuple.Point(1, 2, 3), new Tuple.Vector(0, 1, 0));
            Matrix trans = Matrix.getTranslationMatrix(3, 4, 5);
            Ray outRay = Ray.transform(inRay, trans);
            Assert.IsTrue(outRay.origin == new Tuple.Point(4, 6, 8));
            Assert.IsTrue(outRay.direction == new Tuple.Vector(0, 1, 0));
        }
        [TestMethod]
        public void Test72() // scaling ray
        {
            Ray inRay = new Ray(new Tuple.Point(1, 2, 3), new Tuple.Vector(0, 1, 0));
            Matrix scale = Matrix.getScalingMatrix(2, 3, 4);
            Ray outRay = Ray.transform(inRay, scale);
            Assert.IsTrue(outRay.origin == new Tuple.Point(2, 6, 12));
            Assert.IsTrue(outRay.direction == new Tuple.Vector(0, 3, 0));
        }
        [TestMethod]
        public void Test73() // transform in a sphere
        {
            Object.Sphere s = new Object.Sphere();
            Assert.IsTrue(s.transform == Matrix.identity(4, 4));
        }
        [TestMethod]
        public void Test74() // transform in a sphere 2
        {
            Object.Sphere s = new Object.Sphere();
            Matrix trans = Matrix.getTranslationMatrix(2, 3, 4);
            Object.Sphere.set_transform(s, trans);
            Assert.IsTrue(s.transform == trans);
        }
        [TestMethod]
        public void Test75() // intersect with scaled sphere
        {
            Object.Sphere s = new Object.Sphere();
            Matrix trans = Matrix.getScalingMatrix(2, 2, 2);
            Object.Sphere.set_transform(s, trans);
            Ray ray = new Ray(new Tuple.Point(0, 0, -5), new Tuple.Vector(0, 0, 1));
            Intersection[] xs = s.intersectWith(ray);
            Assert.AreEqual(xs.Length, 2);
            Assert.AreEqual(xs[0].t, 3);
            Assert.AreEqual(xs[1].t, 7);
        }
        [TestMethod]
        public void Test76() // intersect with translated matrix 2
        {
            Object.Sphere s = new Object.Sphere();
            Matrix trans = Matrix.getTranslationMatrix(5, 0, 0);
            Object.Sphere.set_transform(s, trans);
            Ray ray = new Ray(new Tuple.Point(0, 0, -5), new Tuple.Vector(0, 0, 1));
            Intersection[] xs = s.intersectWith(ray);
            Assert.AreEqual(xs.Length, 0);
        }
        [TestMethod]
        public void Test77() // creating a world
        {
            World w = new World();
            Assert.AreEqual(w.lights.Length, 0);
            Assert.AreEqual(w.objects.Length, 0);
        }
        [TestMethod]
        public void Test78() // creating a default world
        {
            World w = World.default_world();
            Assert.IsTrue(w.lights[0] == new Light(new Tuple.Point(-10, 10, -10), new Color(1, 1, 1)));
            Assert.IsTrue((Object.Sphere)w.objects[0] == new Object.Sphere(new Material(new Color(.8, 1, .6), .7, .2, .1, 200, 0, 0, 1)));
            Object.Sphere s2 = new Object.Sphere(new Material());
            Object.Sphere.set_transform(s2, Matrix.getScalingMatrix(.5, .5, .5));
            Assert.IsTrue(w.objects[1] == s2);
        }
        [TestMethod]
        public void Test79() // intersect world
        {
            World w = World.default_world();
            Ray r = new Ray(new Tuple.Point(0, 0, -5), new Tuple.Vector(0, 0, 1));
            Intersection[] xs = Ray.intersect(w, r);
            Assert.AreEqual(xs.Length, 4);
            Assert.AreEqual(xs[0].t, 4);
            Assert.AreEqual(xs[1].t, 4.5);
            Assert.AreEqual(xs[2].t, 5.5);
            Assert.AreEqual(xs[3].t, 6);
        }
        [TestMethod]
        public void Test80() // shading an intersection
        {
            World w = World.default_world();
            Ray raymond = new Ray(new Tuple.Point(0, 0, -5), new Tuple.Vector(0, 0, 1));
            Object.Sphere s1 = (Object.Sphere)w.objects[0];
            Intersection i = new Intersection(4, s1, raymond);
            comps computations = Intersection.prepare_computations(i, raymond, new Intersection[1] { i });
            Color c = Intersection.shade_hit(w, computations, 5);
            Assert.IsTrue(c == new Color(.38066, .47583, .2855));
            //Assert.AreEqual(c.r, .38066);
            //Assert.AreEqual(c.g, .47583);
            //Assert.AreEqual(c.b, .2855);
        }
        [TestMethod]
        public void Test81() // shading an intersection from inside
        {
            World w = World.default_world();
            Ray raymond = new Ray(new Tuple.Point(0, 0, 0), new Tuple.Vector(0, 0, 1));
            Object.Sphere s1 = (Object.Sphere)w.objects[1];
            w.lights[0] = Light.point_light(new Tuple.Point(0, 0.25, 0), new Color(1, 1, 1));
            Intersection i = new Intersection(.5, s1, raymond);
            comps computations = Intersection.prepare_computations(i, raymond, new Intersection[1] { i });
            Color c = Intersection.shade_hit(w, computations, 5);
            Assert.IsTrue(c == new Color(.90498, .90498, .90498));
            //Assert.AreEqual(c.r, 0.89442);
            //Assert.AreEqual(c.g, .90498);
            //Assert.AreEqual(c.b, .90498);
        }
        [TestMethod]
        public void Test82() // color_at miss
        {
            World w = World.default_world();
            Ray ray = new Ray(new Tuple.Point(0, 0, -5), new Tuple.Vector(0, 1, 0));
            Color c = Ray.color_at(w, ray, 5);
            Assert.IsTrue(c == new Color(0, 0, 0));
        }
        [TestMethod]
        public void Test83() // color_at hit
        {
            World w = World.default_world();
            Ray ray = new Ray(new Tuple.Point(0, 0, -5), new Tuple.Vector(0, 0, 1));
            Color c = Ray.color_at(w, ray, 5);
            Assert.IsTrue(c == new Color(0.38066, 0.47583, 0.28550));
            //Assert.AreEqual(c.r, .38066);
        }
        [TestMethod]
        public void Test84() // color_at miss
        {
            World w = World.default_world();
            w.objects[1].material.ambient = 1;
            Ray raymond = new Ray(new Tuple.Point(0, 0, .75), new Tuple.Vector(0, 0, -1));
            Color c = Ray.color_at(w, raymond, 5);
            Assert.AreEqual(c.r, w.objects[1].material.color.r);
            Assert.IsTrue(c == w.objects[1].material.color);
            
        }
        [TestMethod]
        public void Test85() // intersection t and object
        {
            Ray raymond = new Ray(new Tuple.Point(0, 0, -5), new Tuple.Vector(0, 0, 1));
            Object.Sphere s = new Object.Sphere();
            Intersection i = new Intersection(3.5, s, raymond);
            Assert.AreEqual(i.t, 3.5);
            Assert.IsTrue(i.obj == s);
        }
        [TestMethod]
        public void Test89() // no shadow when nothing collinear
        {
            World w = World.default_world();
            Tuple.Point p = new Tuple.Point(0, 10, 0);
            Assert.IsFalse(World.is_shadowed(w, w.lights[0], p));
        }
        [TestMethod]
        public void Test90() // shadow when object between point and light
        {
            World w = World.default_world();
            Tuple.Point p = new Tuple.Point(10, -10, 10);
            Assert.IsTrue(World.is_shadowed(w, w.lights[0], p));
        }
        [TestMethod]
        public void Test91() // no shadow when object behind light
        {
            World w = World.default_world();
            Tuple.Point p = new Tuple.Point(-20, 20, -20);
            Assert.IsFalse(World.is_shadowed(w, w.lights[0], p));
        }
        [TestMethod]
        public void Test92() // no shadow when object behind point
        {
            World w = World.default_world();
            Tuple.Point p = new Tuple.Point(-2, 2, -2);
            Assert.IsFalse(World.is_shadowed(w, w.lights[0], p));
        }
        [TestMethod]
        public void Test93() // shade_hit given intersection in shadow
        {
            World w = World.default_world();
            w.lights[0] = Light.point_light(new Tuple.Point(0, 0, -10), new Color(1, 1, 1));
            w.objects[0] = new Object.Sphere();
            w.objects[1] = new Object.Sphere();
            w.objects[1].transform = Matrix.getTranslationMatrix(0, 0, 10);
            Ray raymond = new Ray(new Tuple.Point(0, 0, 5), new Tuple.Vector(0, 0, -1));
            Intersection i = new Intersection(4, w.objects[1], raymond);
            Color c = Intersection.shade_hit(w, Intersection.prepare_computations(i, raymond, new Intersection[1] { i }), 5);
            Assert.AreEqual(c.r, .1);
            Assert.IsTrue(c == new Color(.1, .1, .1));
        }
        [TestMethod]
        public void Test94() // rotation x
        {
            Tuple.Point p = new Tuple.Point(0, 1, 0);
            Matrix halfQ = Matrix.rotationX(Math.PI / 4);
            Matrix fullQ = Matrix.rotationX(Math.PI / 2);
            Assert.IsTrue((halfQ * p) == new Tuple.Point(0, Math.Sqrt(2) / 2, Math.Sqrt(2) / 2));
            Assert.IsTrue((fullQ * p) == new Tuple.Point(0, 0, 1));
        }
        [TestMethod]
        public void Test95() // rotation x inverse
        {
            Tuple.Point p = new Tuple.Point(0, 1, 0);
            Matrix halfQ = Matrix.rotationX(Math.PI / 4);
            Matrix inv = Matrix.getInverse(halfQ);
            Assert.IsTrue((inv * p) == new Tuple.Point(0, Math.Sqrt(2) / 2, -Math.Sqrt(2) / 2));
        }
        [TestMethod]
        public void Test96() // rotation y inverse
        {
            Tuple.Point p = new Tuple.Point(0, 0, 1);
            Matrix halfQ = Matrix.rotationY(Math.PI / 4);
            Matrix fullQ = Matrix.rotationY(Math.PI / 2);
            Assert.IsTrue((halfQ * p) == new Tuple.Point(Math.Sqrt(2) / 2, 0, Math.Sqrt(2) / 2));
            Assert.IsTrue((fullQ * p) == new Tuple.Point(1, 0, 0));
        }
        [TestMethod]
        public void Test97() // rotation z inverse
        {
            Tuple.Point p = new Tuple.Point(0, 1, 0);
            Matrix halfQ = Matrix.rotationZ(Math.PI / 4);
            Matrix fullQ = Matrix.rotationZ(Math.PI / 2);
            Assert.IsTrue((halfQ * p) == new Tuple.Point(-Math.Sqrt(2) / 2, Math.Sqrt(2) / 2, 0));
            Assert.IsTrue((fullQ * p) == new Tuple.Point(-1, 0, 0));
        }
        [TestMethod]
        public void Test98() // plane normal
        {
            Object.Plane p = new Object.Plane();
            Tuple.Vector n1 = p.normalAt(new Tuple.Point(0, 0, 0)); //different form the test case because I didn't implement local_normal_at() because all the transformation between object space and world space are done in this normalAt()
            Tuple.Vector n2 = p.normalAt(new Tuple.Point(10, 0, -10));
            Tuple.Vector n3 = p.normalAt(new Tuple.Point(-5, 0, 150));
            Assert.IsTrue(n1 == new Tuple.Vector(0, 1, 0));
            Assert.IsTrue(n2 == new Tuple.Vector(0, 1, 0));
            Assert.IsTrue(n3 == new Tuple.Vector(0, 1, 0));
        }
        [TestMethod]
        public void Test99() //intersect with a ray parallel to plane
        {
            Object.Plane p = new Object.Plane();
            Ray raymond = new Ray(new Tuple.Point(0, 10, 0), new Tuple.Vector(0, 0, 1));
            Intersection[] xs = p.intersectWith(raymond);
            Assert.AreEqual(xs.Length, 0);
        }
        [TestMethod]
        public void Test100() //intersect with a coplanar ray
        {
            Object.Plane p = new Object.Plane();
            Ray raymond = new Ray(new Tuple.Point(0, 0, 0), new Tuple.Vector(0, 0, 1));
            Intersection[] xs = p.intersectWith(raymond);
            Assert.AreEqual(xs.Length, 0);
        }
        [TestMethod]
        public void Test101() //intersect a plane from above
        {
            Object.Plane p = new Object.Plane();
            Ray raymond = new Ray(new Tuple.Point(0, 1, 0), new Tuple.Vector(0, -1, 0));
            Intersection[] xs = p.intersectWith(raymond);
            Assert.AreEqual(xs.Length, 1);
            Assert.IsTrue(xs[0].obj == p);
        }
        [TestMethod]
        public void Test102() //intersect a plane from below
        {
            Object.Plane p = new Object.Plane();
            Ray raymond = new Ray(new Tuple.Point(0, -1, 0), new Tuple.Vector(0, 1, 0));
            Intersection[] xs = p.intersectWith(raymond);
            Assert.AreEqual(xs.Length, 1);
            Assert.IsTrue(xs[0].obj == p);
            Assert.AreEqual(xs[0].t, 1);
        }
        [TestMethod]
        public void Camera1() // testing camera constructor
        {
            Camera cam = new Camera(160, 120, Math.PI / 2);
            Assert.AreEqual(cam.hsize, 160);
            Assert.AreEqual(cam.vsize, 120);
            Assert.AreEqual(cam.field_of_view, Math.PI / 2);
            Assert.IsTrue(cam.transform == Matrix.identity(4, 4));
        }
        [TestMethod]
        public void Camera2() // testing camera pixelsize
        {
            Camera cam = new Camera(200, 125, Math.PI / 2);
            Assert.IsTrue(Math.Abs(cam.pixelSize() - .01) < Constants.EPSILON);
        }
        [TestMethod]
        public void Camera3() // testing camera pixelsize
        {
            Camera cam = new Camera(125, 200, Math.PI / 2);
            Assert.IsTrue(Math.Abs(cam.pixelSize() - .01) < Constants.EPSILON);
        }
        [TestMethod]
        public void Camera4() // testing camera ray
        {
            Camera cam = new Camera(201, 101, Math.PI / 2);
            Ray r = Camera.rayForPixel(cam, 100, 50);
            Assert.IsTrue(r.origin == new Tuple.Point(0, 0, 0));
            Assert.IsTrue(r.direction == new Tuple.Vector(0, 0, -1));
        }
        [TestMethod]
        public void Camera5() // testing camera ray 2
        {
            Camera cam = new Camera(201, 101, Math.PI / 2);
            Ray r = Camera.rayForPixel(cam, 0, 0);
            Assert.IsTrue(r.origin == new Tuple.Point(0, 0, 0));
            Assert.IsTrue(r.direction == new Tuple.Vector(.66519, .33259, -.66851));
        }
        [TestMethod]
        public void Camera6() // testing camera ray 2
        {
            Camera cam = new Camera(201, 101, Math.PI / 2);
            cam.transform = Matrix.rotationY(Math.PI / 4) * Matrix.getTranslationMatrix(0, -2, 5);
            Ray r = Camera.rayForPixel(cam, 100, 50);
            Assert.IsTrue(r.origin == new Tuple.Point(0, 2, -5));
            Assert.IsTrue(r.direction == new Tuple.Vector(Math.Sqrt(2) / 2, 0, -Math.Sqrt(2) / 2));
        }
        [TestMethod]
        public void Camera7() // testing render
        {
            World w = World.default_world();
            Camera c = new Camera(11, 11, Math.PI / 2);
            Tuple.Point from = new Tuple.Point(0, 0, -5);
            Tuple.Point to = new Tuple.Point(0, 0, 0);
            Tuple.Vector up = new Tuple.Vector(0, 1, 0);
            c.transform = Matrix.view_transformation(from, to, up);
            Canvas image = Camera.render(c, w);
            Assert.IsTrue(Canvas.pixelAt(image, 5, 5) == new Color(.38066, .47583, .2855));
        }
        [TestMethod]
        public void Refraction1()
        {
            Material m = new Material();
            Assert.AreEqual(m.transparency, 0);
            Assert.AreEqual(m.indexOfRefraction, 1);
        }
        [TestMethod]
        public void Refraction2()
        {
            Object.Sphere A = new Object.Sphere();
            A.transform = Matrix.getScalingMatrix(2, 2, 2);
            A.material.transparency = 1;
            A.material.indexOfRefraction = 1.5;

            Object.Sphere B = new Object.Sphere();
            B.transform = Matrix.getTranslationMatrix(0, 0, -.25);
            B.material.transparency = 1;
            B.material.indexOfRefraction = 2;

            Object.Sphere C = new Object.Sphere();
            C.transform = Matrix.getTranslationMatrix(0, 0, .25);
            C.material.transparency = 1;
            C.material.indexOfRefraction = 2.5;

            Ray r = new Ray(new Tuple.Point(0, 0, -4), new Tuple.Vector(0, 0, 1));

            Intersection[] xs = new Intersection[6] { new Intersection(2, A, r), new Intersection(2.75, B, r), new Intersection(3.25, C, r), new Intersection(4.75, B, r), new Intersection(5.25, C, r), new Intersection(6, A, r) };

            Assert.IsTrue(Intersection.hit(xs) == xs[0]);

            comps comps0 = Intersection.prepare_computations(xs[0], r, xs);
            Assert.AreEqual(comps0.n1, 1);
            Assert.AreEqual(comps0.n2, 1.5);

            comps comps1 = Intersection.prepare_computations(xs[1], r, xs);
            Assert.AreEqual(comps1.n1, 1.5);
            Assert.AreEqual(comps1.n2, 2);

            comps comps2 = Intersection.prepare_computations(xs[2], r, xs);
            Assert.AreEqual(comps2.n1, 2);
            Assert.AreEqual(comps2.n2, 2.5);

            comps comps3 = Intersection.prepare_computations(xs[3], r, xs);
            Assert.AreEqual(comps3.n1, 2.5);
            Assert.AreEqual(comps3.n2, 2.5);

            comps comps4 = Intersection.prepare_computations(xs[4], r, xs);
            Assert.AreEqual(comps4.n1, 2.5);
            Assert.AreEqual(comps4.n2, 1.5);

            comps comps5 = Intersection.prepare_computations(xs[5], r, xs);
            Assert.AreEqual(comps5.n1, 1.5);
            Assert.AreEqual(comps5.n2, 1);
        }

    }
}
