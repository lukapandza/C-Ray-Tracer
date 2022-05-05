using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RayTracer
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void shadowsFromMultipleLights()
        {
            World w = new World();

            w.addLight(Light.point_light(new Tuple.Point(30, 20, 0), new Color(.8, .8, .8)));
            w.addLight(Light.point_light(new Tuple.Point(30, 20, 4), new Color(.6, .6, .6)));
            w.addLight(Light.point_light(new Tuple.Point(10, 20, 30), new Color(.7, .7, .7)));
            w.addLight(Light.point_light(new Tuple.Point(30, 20, 20), new Color(.9, .9, .9)));

            w.addObject(new Object.Plane());
            w.objects[0].material.reflectivity = .2;
            w.objects[0].material.color = new Color(.7, .65, .65);

            double sumR = -2.2;
            for(int i = 1; i <= 10; i++)
            {
                sumR = sumR + 2 * (2.2 - i * .2);
                w.addObject(new Object.Sphere());
                w.objects[i].material.reflectivity = i * .05;
                w.objects[i].material.transparency = i * .05;
                w.objects[i].transform = Matrix.getTranslationMatrix(0, sumR, 0) * Matrix.getScalingMatrix(2.2 - i * .2, 2.2 - i * .2, 2.2 - i * .2);
                w.objects[i].material.color = new Color(1, (i - 1) * .1, (i - 1) * .1);
            }

            double sumG = -2.2;
            for (int i = 11; i <= 20; i++)
            {
                sumG = sumG + 2 * (2.2 - (i - 10) * .2);
                w.addObject(new Object.Sphere());
                w.objects[i].material.reflectivity = (i - 10) * .05;
                w.objects[i].material.transparency = (i - 10) * .05;
                w.objects[i].transform = Matrix.getTranslationMatrix(-8, sumG, -2) * Matrix.getScalingMatrix(2.2 - (i - 10) * .2, 2.2 - (i - 10) * .2, 2.2 - (i - 10) * .2);
                w.objects[i].material.color = new Color((i - 11) * .1, 1, (i - 11) * .1);
            }

            double sumB = -2.2;
            for (int i = 21; i <= 30; i++)
            {
                sumB = sumB + 2 * (2.2 - (i - 20) * .2);
                w.addObject(new Object.Sphere());
                w.objects[i].material.reflectivity = (i - 20) * .05;
                w.objects[i].material.transparency = (i - 20) * .05;
                w.objects[i].transform = Matrix.getTranslationMatrix(-14, sumB, -6) * Matrix.getScalingMatrix(2.2 - (i - 20) * .2, 2.2 - (i - 20) * .2, 2.2 - (i - 20) * .2);
                w.objects[i].material.color = new Color((i - 21) * .1, (i - 21) * .1, 1);
            }

            Camera c = new Camera(1000, 1000, Math.PI / 2);
            Tuple.Point from = new Tuple.Point(12, 14, -18);
            Tuple.Point to = new Tuple.Point(-8, 10, -2);
            Tuple.Vector up = new Tuple.Vector(0, 1, 0);
            c.transform = Matrix.view_transformation(from, to, up);
            Canvas.writePPM(Camera.render(c, w), @"F:\Desktop\RayTracer\Renders\Tests\shadows from multiple lights.ppm");
            Assert.AreEqual(1, 1);
        }
        [TestMethod]
        public void reflectionExample()
        {
            World w = new World();

            w.addObject(new Object.Plane());
            w.objects[0].material.color = new Color(.7, .8, .7);
            w.objects[0].material.reflectivity = .1;
            w.objects[0].translate(0, -1, 0);
            w.objects[0].material.hasPattern = true;
            w.objects[0].material.checkered = new Pattern(Matrix.getScalingMatrix(1.5, 1.5, 1.5), new Color[2] { new Color(0, 0, 0), new Color(1, 1, 1) });

            w.addObject(new Object.Triangle(new Tuple.Point(-4, 0, 0), new Tuple.Point(4, 0, 0), new Tuple.Point(-4, 10, 0)));
            w.objects[1].material.reflectivity = 1;
            w.objects[1].transform = Matrix.getTranslationMatrix(0, -1, -4) * Matrix.rotationY(Math.PI / 16);
            w.objects[1].material.color = new Color(0.1, .1, 0.1);
            w.addObject(new Object.Triangle(new Tuple.Point(-4, 10, 0), new Tuple.Point(4, 0, 0), new Tuple.Point(4, 10, 0)));
            w.objects[2].material.reflectivity = 1;
            w.objects[2].transform = Matrix.getTranslationMatrix(0, -1, -4) * Matrix.rotationY(Math.PI / 16);
            w.objects[2].material.color = new Color(0.1, .1, 0.1);

            w.addObject(new Object.Sphere());
            w.objects[3].material.color = new Color(.7, .2, .3);
            w.objects[3].scale(1.2, 1.2, 1.2);
            w.objects[3].material.reflectivity = .25;

            w.addObject(new Object.Sphere());
            w.objects[4].material.color = new Color(.2, .7, .3);
            w.objects[4].translate(0, -1.25, -2);
            w.objects[4].scale(.5, .5, .5);

            w.addLight(Light.point_light(new Tuple.Point(10, 10, 1), new Color(.8, .8, .8)));
            w.addLight(Light.point_light(new Tuple.Point(-10, 10, 6), new Color(.61, .61, .61)));

            Camera c = new Camera(1000, 1000, Math.PI / 2);
            Tuple.Point from = new Tuple.Point(4, 3, 7);
            Tuple.Point to = new Tuple.Point(0, 1, -3);
            Tuple.Vector up = new Tuple.Vector(0, 1, 0);
            c.transform = Matrix.view_transformation(from, to, up);
            Canvas.writePPM(Camera.render(c, w), @"F:\Desktop\RayTracer\Renders\Tests\reflection_example_mutliple_lights.ppm");
            Assert.AreEqual(1, 1);
        }

        //[TestMethod]
        public void funSceneWithEverything()
        {
            World w = new World();
            w.addLight(Light.point_light(new Tuple.Point(10, 10, 1), new Color(.8, .8, .8)));
            w.addLight(Light.point_light(new Tuple.Point(-10, 10, 6), new Color(1, 1, 1)));

            w.addObject(new Object.Plane());
            w.objects[0].material.color = new Color(.7, .7, .7);
            w.objects[0].material.reflectivity = .2;
            w.objects[0].material.hasPattern = true;
            w.objects[0].material.checkered = new Pattern(Matrix.getScalingMatrix(.5, .5, .5), new Color[2] { new Color(0, 0, 0), new Color(1, 1, 1) });

            w.addObject(new Object.Plane());
            w.objects[1].material.color = new Color(.9, .7, .7);
            w.objects[1].rotateAroundX(Math.PI / 2);
            w.objects[1].material.hasPattern = true;
            w.objects[1].material.squareRing = new Pattern(Matrix.getScalingMatrix(1, 1, 1), new Color[2] { new Color(.85, .46, .44), new Color(.42, .16, .32) });

            w.addObject(new Object.Plane());
            w.objects[2].material.color = new Color(.9, .8, .7);
            w.objects[2].rotateAroundZ(Math.PI / 2);
            w.objects[2].material.hasPattern = true;
            w.objects[2].material.ring = new Pattern(Matrix.rotationY(Math.PI/2) * Matrix.getScalingMatrix(1, 1, 1), new Color[2] { new Color(.42, .16, .32), new Color(.85, .46, .44) });

            w.addObject(new Object.Sphere());
            w.objects[3].material.hasPattern = true;
            w.objects[3].material.checkered = new Pattern(Matrix.getScalingMatrix(.25, .25, .25), new Color[2] { new Color(.6, .1, .1), new Color(.1, .6, .1) });
            w.objects[3].material.transparency = .4;
            w.objects[3].material.reflectivity = .1;
            w.objects[3].translate(1, 1, 1);

            w.addObject(new Object.Sphere());
            w.objects[4].material.color = new Color(.1, .6, .1);
            w.objects[4].transform = Matrix.getTranslationMatrix(.4, .4, 2.2) * Matrix.getScalingMatrix(.4, .4, .4);
            w.objects[4].material.reflectivity = .25;

            w.addObject(new Object.Sphere());
            w.objects[5].material.color = new Color(.1, .1, .6);
            w.objects[5].material.hasPattern = true;
            w.objects[5].material.stripes = new Pattern(Matrix.getScalingMatrix(.2, .2, .2), new Color[2] { new Color(.1, .1, .6), new Color(.85, .75, .75) });
            w.objects[5].transform = Matrix.getTranslationMatrix(3, .8, .8) * Matrix.getScalingMatrix(.8, .8, .8);

            w.addObject(new Object.Sphere());
            w.objects[6].material.color = new Color(0, .05, 0);
            w.objects[6].material.indexOfRefraction = Constants.GLASS;
            w.objects[6].material.transparency = 1;
            w.objects[6].material.reflectivity = 1;
            w.objects[6].transform = Matrix.getTranslationMatrix(5, .8, 5);

            Camera c = new Camera(1000, 1000, Math.PI / 2);
            Tuple.Point from = new Tuple.Point(6, 1, 6);
            Tuple.Point to = new Tuple.Point(0, 1, 0);
            Tuple.Vector up = new Tuple.Vector(0, 1, 0);
            c.transform = Matrix.view_transformation(from, to, up);
            Canvas.writePPM(Camera.render(c, w), @"F:\Desktop\RayTracer\Renders\Tests\fun_scene_with_everything.ppm");
            Assert.AreEqual(1, 1);
        }

        [TestMethod]
        public void stripesPattern()
        {
            World w = new World();

            w.addLight(Light.point_light(new Tuple.Point(0, 10, 10), new Color(1, 1, 1)));

            w.addObject(new Object.Plane());
            w.objects[0].transform = Matrix.rotationX(Math.PI / 2);
            w.objects[0].material.hasPattern = true;
            Color[] colors = new Color[2] { new Color(1, 1, 1), new Color(0, 0, 0) };
            w.objects[0].material.stripes = new Pattern(Matrix.identity(4, 4), colors);

            Camera c = new Camera(1000, 1000, Math.PI / 2);
            Tuple.Point from = new Tuple.Point(0, 0, 5);
            Tuple.Point to = new Tuple.Point(0, 0, 0);
            Tuple.Vector up = new Tuple.Vector(0, 1, 0);
            c.transform = Matrix.view_transformation(from, to, up);
            Canvas.writePPM(Camera.render(c, w), @"F:\Desktop\RayTracer\Renders\Tests\stripes.ppm");
            Assert.AreEqual(1, 1);
        }

        [TestMethod]
        public void gradientPattern()
        {
            World w = new World();

            w.addLight(Light.point_light(new Tuple.Point(0, 10, 10), new Color(1, 1, 1)));

            w.addObject(new Object.Plane());
            w.objects[0].transform = Matrix.rotationX(Math.PI / 2);
            w.objects[0].material.hasPattern = true;
            Color[] colors = new Color[2] { new Color(1, 1, 1), new Color(0, 0, 0) };
            w.objects[0].material.gradient = new Pattern(Matrix.identity(4, 4), colors);

            Camera c = new Camera(1000, 1000, Math.PI / 2);
            Tuple.Point from = new Tuple.Point(0, 0, 5);
            Tuple.Point to = new Tuple.Point(0, 0, 0);
            Tuple.Vector up = new Tuple.Vector(0, 1, 0);
            c.transform = Matrix.view_transformation(from, to, up);
            Canvas.writePPM(Camera.render(c, w), @"F:\Desktop\RayTracer\Renders\Tests\gradient.ppm");
            Assert.AreEqual(1, 1);
        }

        [TestMethod]
        public void ringPattern()
        {
            World w = new World();

            w.addLight(Light.point_light(new Tuple.Point(0, 10, 10), new Color(1, 1, 1)));

            w.addObject(new Object.Plane());
            w.objects[0].transform = Matrix.rotationX(Math.PI / 2);
            w.objects[0].material.hasPattern = true;
            Color[] colors = new Color[2] { new Color(1, 1, 1), new Color(0, 0, 0) };
            w.objects[0].material.ring = new Pattern(Matrix.identity(4, 4), colors);

            Camera c = new Camera(1000, 1000, Math.PI / 2);
            Tuple.Point from = new Tuple.Point(0, 0, 5);
            Tuple.Point to = new Tuple.Point(0, 0, 0);
            Tuple.Vector up = new Tuple.Vector(0, 1, 0);
            c.transform = Matrix.view_transformation(from, to, up);
            Canvas.writePPM(Camera.render(c, w), @"F:\Desktop\RayTracer\Renders\Tests\ring.ppm");
            Assert.AreEqual(1, 1);
        }

        [TestMethod]
        public void checkeredPattern()
        {
            World w = new World();

            w.addLight(Light.point_light(new Tuple.Point(0, 10, 10), new Color(1, 1, 1)));

            w.addObject(new Object.Plane());
            w.objects[0].transform = Matrix.rotationX(Math.PI / 2);
            w.objects[0].material.hasPattern = true;
            Color[] colors = new Color[2] { new Color(1, 1, 1), new Color(0, 0, 0) };
            w.objects[0].material.checkered = new Pattern(Matrix.identity(4, 4), colors);

            Camera c = new Camera(1000, 1000, Math.PI / 2);
            Tuple.Point from = new Tuple.Point(0, 0, 5);
            Tuple.Point to = new Tuple.Point(0, 0, 0);
            Tuple.Vector up = new Tuple.Vector(0, 1, 0);
            c.transform = Matrix.view_transformation(from, to, up);
            Canvas.writePPM(Camera.render(c, w), @"F:\Desktop\RayTracer\Renders\Tests\checkered.ppm");
            Assert.AreEqual(1, 1);
        }

        [TestMethod]
        public void squareRingPattern()
        {
            World w = new World();

            w.addLight(Light.point_light(new Tuple.Point(0, 10, 10), new Color(1, 1, 1)));

            w.addObject(new Object.Plane());
            w.objects[0].transform = Matrix.rotationX(Math.PI / 2);
            w.objects[0].material.hasPattern = true;
            Color[] colors = new Color[2] { new Color(1, 1, 1), new Color(0, 0, 0) };
            w.objects[0].material.squareRing = new Pattern(Matrix.rotationX(Math.PI / 4) * Matrix.identity(4, 4), colors);

            Camera c = new Camera(1000, 1000, Math.PI / 2);
            Tuple.Point from = new Tuple.Point(0, 0, 5);
            Tuple.Point to = new Tuple.Point(0, 0, 0);
            Tuple.Vector up = new Tuple.Vector(0, 1, 0);
            c.transform = Matrix.view_transformation(from, to, up);
            Canvas.writePPM(Camera.render(c, w), @"F:\Desktop\RayTracer\Renders\Tests\square_ring.ppm");
            Assert.AreEqual(1, 1);
        }

        [TestMethod]
        public void glassSphereAndCheckered()
        {
            World w = new World();

            w.addLight(Light.point_light(new Tuple.Point(0, 10, 10), new Color(1, 1, 1)));

            w.addObject(new Object.Plane());
            w.objects[0].transform = Matrix.rotationX(Math.PI / 2);
            w.objects[0].material.hasPattern = true;
            Color[] colors = new Color[2] { new Color(1, 1, 1), new Color(0, 0, 0) };
            w.objects[0].material.checkered = new Pattern(Matrix.getScalingMatrix(.2, .2, .2), colors);

            w.addObject(new Object.Sphere());
            w.objects[1].transform = Matrix.getTranslationMatrix(0, 0, 2);
            w.objects[1].material.color = new Color(.3, .3, .3);
            w.objects[1].material.transparency = 1;
            w.objects[1].material.reflectivity = 1;
            w.objects[1].material.indexOfRefraction = Constants.GLASS;

            Camera c = new Camera(1000, 1000, Math.PI / 2);
            Tuple.Point from = new Tuple.Point(0, 0, 4);
            Tuple.Point to = new Tuple.Point(0, 0, 0);
            Tuple.Vector up = new Tuple.Vector(0, 1, 0);
            c.transform = Matrix.view_transformation(from, to, up);
            Canvas.writePPM(Camera.render(c, w), @"F:\Desktop\RayTracer\Renders\Tests\checkered_plane_through_glass_sphere.ppm");
            Assert.AreEqual(1, 1);
        }

        [TestMethod]
        public void glassSphereWithAirBubbleAndCheckered()
        {
            World w = new World();

            w.addLight(Light.point_light(new Tuple.Point(5, 15, 10), new Color(1, 1, 1)));

            w.addObject(new Object.Plane());
            w.objects[0].transform = Matrix.rotationX(Math.PI / 2) * Matrix.getTranslationMatrix(1, 0, 1);
            w.objects[0].material.hasPattern = true;
            Color[] colors = new Color[2] { new Color(1, 1, 1), new Color(0, 0, 0) };
            w.objects[0].material.checkered = new Pattern(Matrix.getScalingMatrix(.4, .4, .4), colors);

            w.addObject(new Object.Sphere());
            w.objects[1].transform = Matrix.getTranslationMatrix(0, 0, 6);
            w.objects[1].material.color = new Color(0, 0, 0);
            w.objects[1].material.transparency = 1;
            w.objects[1].material.reflectivity = 1;
            w.objects[1].material.indexOfRefraction = Constants.GLASS;

            w.addObject(new Object.Sphere());
            w.objects[2].transform = Matrix.getTranslationMatrix(0, 0, 5.8) * Matrix.getScalingMatrix(.3, .3, .3);
            w.objects[2].material.color = new Color(0, 0, 0);
            w.objects[2].material.transparency = 1;
            w.objects[2].material.reflectivity = 1;
            w.objects[2].material.indexOfRefraction = 1;

            Camera c = new Camera(1000, 1000, Math.PI / 2);
            Tuple.Point from = new Tuple.Point(0, 0, 7.5);
            Tuple.Point to = new Tuple.Point(0, 0, 0);
            Tuple.Vector up = new Tuple.Vector(0, 1, 0);
            c.transform = Matrix.view_transformation(from, to, up);
            Canvas.writePPM(Camera.render(c, w), @"F:\Desktop\RayTracer\Renders\Tests\checkered_plane_through_glass_sphere_with_air_bubble.ppm");
            Assert.AreEqual(1, 1);
        }

        [TestMethod]
        public void glassSphereWithTwoAirBubblesAndCheckered()
        {
            World w = new World();

            w.addLight(Light.point_light(new Tuple.Point(5, 15, 10), new Color(1, 1, 1)));

            w.addObject(new Object.Plane());
            w.objects[0].transform = Matrix.rotationX(Math.PI / 2) * Matrix.getTranslationMatrix(1, 0, 1);
            w.objects[0].material.hasPattern = true;
            Color[] colors = new Color[2] { new Color(1, 1, 1), new Color(0, 0, 0) };
            w.objects[0].material.checkered = new Pattern(Matrix.getScalingMatrix(.4, .4, .4), colors);

            w.addObject(new Object.Sphere());
            w.objects[1].transform = Matrix.getTranslationMatrix(0, 0, 6);
            w.objects[1].material.color = new Color(0, 0, 0);
            w.objects[1].material.transparency = 1;
            w.objects[1].material.reflectivity = 1;
            w.objects[1].material.indexOfRefraction = Constants.GLASS;

            w.addObject(new Object.Sphere());
            w.objects[2].transform = Matrix.getTranslationMatrix(.2, .2, 6.2) * Matrix.getScalingMatrix(.25, .25, .25);
            w.objects[2].material.color = new Color(0, 0, 0);
            w.objects[2].material.transparency = 1;
            w.objects[2].material.reflectivity = 1;
            w.objects[2].material.indexOfRefraction = 2.1;

            w.addObject(new Object.Sphere());
            w.objects[3].transform = Matrix.getTranslationMatrix(-.2, -.2, 5.8) * Matrix.getScalingMatrix(.25, .25, .25);
            w.objects[3].material.color = new Color(0, 0, 0);
            w.objects[3].material.transparency = 1;
            w.objects[3].material.reflectivity = 1;
            w.objects[3].material.indexOfRefraction = 2.4;

            Camera c = new Camera(1000, 1000, Math.PI / 2);
            Tuple.Point from = new Tuple.Point(0, 0, 7.5);
            Tuple.Point to = new Tuple.Point(0, 0, 0);
            Tuple.Vector up = new Tuple.Vector(0, 1, 0);
            c.transform = Matrix.view_transformation(from, to, up);
            Canvas.writePPM(Camera.render(c, w), @"F:\Desktop\RayTracer\Renders\Tests\checkered_plane_through_glass_sphere_with_two_air_bubbles.ppm");
            Assert.AreEqual(1, 1);
        }
    }
}
