using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    class World
    {
        public Light[] lights;
        public Object[] objects;

        public World()
        {
            this.lights = new Light[0];
            this.objects = new Object[0];
        }

        public static World default_world()
        {
            World def = new World();
            def.lights = new Light[1];
            def.lights[0] = Light.point_light(new Tuple.Point(-10, 10, -10), new Color(1, 1, 1));

            def.objects = new Object[2];
            def.objects[0] = new Object.Sphere(new Material(new Color(.8, 1, .6), .7, .2, .1, 200, 0, 0, 1));
            def.objects[1] = new Object.Sphere(new Material());
            def.objects[1].transform = Matrix.getScalingMatrix(.5, .5, .5);
            return def;
        }

        public static bool is_shadowed(World w, Light light, Tuple.Point p)
        {
            Tuple.Vector vec = light.position - p;
            double dist = vec.magnitude();
            Tuple.Vector dir = vec.normalize();
            Ray raymond = new Ray(p, dir);
            Intersection h = Intersection.hit(Ray.intersect(w, raymond));
            if (h != null && h.t < dist) return true;
            else return false;
        } 

        public void addObject(Object obj)
        {
            Object[] newObjects = new Object[this.objects.Length + 1];
            for(int i = 0; i < this.objects.Length; i++)
            {
                newObjects[i] = this.objects[i];
            }
            newObjects[newObjects.Length - 1] = obj;
            this.objects = newObjects;
        }

        public void addLight(Light light)
        {
            Light[] newLights = new Light[this.lights.Length + 1];
            for (int i = 0; i < this.lights.Length; i++)
            {
                newLights[i] = this.lights[i];
            }
            newLights[newLights.Length - 1] = light;
            this.lights = newLights;
        }
    }
}
