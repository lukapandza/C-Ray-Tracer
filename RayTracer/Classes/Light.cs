using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    class Light
    {
        public Tuple.Point position;
        public Color intensity;

        public Light(Tuple.Point pos, Color intensity)
        {
            this.position = pos;
            this.intensity = intensity;
        }

        public static Light point_light(Tuple.Point pos, Color intensity)
        {
            return new Light(pos, intensity);
        }

        public static Color lighting(Material mat, Light light, Tuple.Point position, Tuple.Vector eyev, Tuple.Vector normalv, bool in_shadow, Object obj)
        {
            Color cumulativeCol = null;
            if (mat.hasPattern)
            {
                if (mat.stripes != null) cumulativeCol = Pattern.stripes(mat.stripes, obj, position) * light.intensity;
                else if (mat.gradient != null) cumulativeCol = Pattern.gradient(mat.gradient, obj, position) * light.intensity;
                else if (mat.ring != null) cumulativeCol = Pattern.ring(mat.ring, obj, position) * light.intensity;
                else if (mat.checkered != null) cumulativeCol = Pattern.checkered(mat.checkered, obj, position) * light.intensity;
                else if (mat.squareRing != null) cumulativeCol = Pattern.squareRing(mat.squareRing, obj, position) * light.intensity;
            }
            else cumulativeCol = mat.color * light.intensity;

            Tuple.Vector lightv = (light.position - position).normalize();
            Color ambientColor = cumulativeCol * mat.ambient;
            double tmp = Tuple.Vector.dotProduct((light.position - position).normalize(), normalv.normalize());
            
            Color diffuseColor = null;
            Color specularColor = null;
            
            if(tmp < 0)
            {
                diffuseColor = new Color(0, 0, 0);
                specularColor = new Color(0, 0, 0);
            }
            else
            {
                diffuseColor = cumulativeCol * mat.diffuse * tmp;
                Tuple.Vector reflectv = Tuple.Vector.reflect((lightv * (-1)), normalv);
                double reflectDotEye = Tuple.Vector.dotProduct(reflectv, eyev);

                if (reflectDotEye <= 0) specularColor = new Color(0, 0, 0);
                else
                {
                    double factor = Math.Pow(reflectDotEye, mat.shininess);
                    specularColor = light.intensity * mat.specular * factor;
                }
            }

            if (in_shadow) return ambientColor;
            else return ambientColor + diffuseColor + specularColor;
        }

        public static bool operator ==(Light light1, Light light2)
        {
            if(light1.position == light2.position && light1.intensity == light2.intensity) return true;
            else return false;
        }

        public static bool operator !=(Light light1, Light light2)
        {
            if (light1.position != light2.position || light1.intensity != light2.intensity) return true;
            else return false;
        }
    }
}
