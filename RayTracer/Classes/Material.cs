using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    class Material
    {
        public Color color;
        public double diffuse;
        public double specular;
        public double ambient;
        public double shininess;
        public double reflectivity;
        public double transparency;
        public double indexOfRefraction;
        public bool hasPattern;
        public Pattern stripes;
        public Pattern gradient;
        public Pattern ring;
        public Pattern checkered;
        public Pattern squareRing;

        public Material(Color col, double diffuse, double specular, double ambient, double shininess, double reflectivity, double transparency, double indexOfRefraction)
        {
            this.color = col;
            this.diffuse = diffuse;
            this.specular = specular;
            this.ambient = ambient;
            this.shininess = shininess;
            this.reflectivity = reflectivity;
            this.transparency = transparency;
            this.indexOfRefraction = indexOfRefraction;
            this.hasPattern = false;
            this.stripes = null;
            this.gradient = null;
            this.ring = null;
            this.checkered = null;
            this.squareRing = null;
        }
        
        public Material()
        {
            this.color = new Color(1, 1, 1);
            this.diffuse = .9;
            this.specular = .9;
            this.ambient = .1;
            this.shininess = 200.0;
            this.reflectivity = 0;
            this.transparency = 0;
            this.indexOfRefraction = 1;
            this.hasPattern = false;
            this.stripes = null;
            this.gradient = null;
            this.ring = null;
            this.checkered = null;
            this.squareRing = null;
        }

        public static bool operator ==(Material m1, Material m2)
        {
            bool mC = m1.color == m2.color;
            bool mD = Math.Abs(m1.diffuse - m2.diffuse) < Constants.EPSILON;
            bool mS = Math.Abs(m1.specular - m2.specular) < Constants.EPSILON;
            bool mA = Math.Abs(m1.ambient - m2.ambient) < Constants.EPSILON;
            bool mSh = Math.Abs(m1.shininess - m2.shininess) < Constants.EPSILON;
            bool mRe = Math.Abs(m1.reflectivity - m2.reflectivity) < Constants.EPSILON;
            bool mTr = Math.Abs(m1.transparency - m2.transparency) < Constants.EPSILON;
            bool mIoR = Math.Abs(m1.indexOfRefraction - m2.indexOfRefraction) < Constants.EPSILON;

            if (mC && mD && mS && mA && mSh && mRe && mTr && mIoR) return true;
            else return false;
        }

        public static bool operator !=(Material m1, Material m2)
        {
            bool mC = m1.color != m2.color;
            bool mD = Math.Abs(m1.diffuse - m2.diffuse) > Constants.EPSILON;
            bool mS = Math.Abs(m1.specular - m2.specular) > Constants.EPSILON;
            bool mA = Math.Abs(m1.ambient - m2.ambient) > Constants.EPSILON;
            bool mSh = Math.Abs(m1.shininess - m2.shininess) > Constants.EPSILON;
            bool mRe = Math.Abs(m1.reflectivity - m2.reflectivity) > Constants.EPSILON;
            bool mTr = Math.Abs(m1.transparency - m2.transparency) > Constants.EPSILON;
            bool mIoR = Math.Abs(m1.indexOfRefraction - m2.indexOfRefraction) > Constants.EPSILON;

            if (mC || mD || mS || mA || mSh || mRe || mTr || mIoR) return true;
            else return false;
        }
    }
}
