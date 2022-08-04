﻿using System.Collections.Generic;
using static System.Math;

namespace StadiumTools
{
    public struct Vec3d
    {
        //Properties
        /// <summary>
        /// X component of vector.
        /// </summary>
        public double X { get; set; }
        /// <summary>
        /// Y component of vector.
        /// </summary>
        public double Y { get; set; }
        /// <summary>
        /// Z component of a vector 
        /// </summary>
        public double Z { get; set; }
        /// <summary>
        /// Magnitude of the 3d vector
        /// </summary>
        public double M { get; set; }

        //Constructors
        /// <summary>
        /// Construct a Vec3d object from 3 its three components (x,y,z)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public Vec3d(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.M = Magnitude(x, y, z);
        }

        //Delegates
        /// <summary>
        /// Gets Vector with Default XAxis components (1.0, 0.0, 0.0)
        /// </summary>
        public static Vec3d XAxis => new Vec3d(1.0, 0.0, 0.0);
        /// <summary>
        /// Gets Vector with Default XAxis components (0.0, 1.0, 0.0)
        /// </summary>
        public static Vec3d YAxis => new Vec3d(0.0, 1.0, 0.0);
        
        /// <summary>
        /// Gets Vector with Default XAxis components (0.0, 0.0, 1.0)
        /// </summary>
        public static Vec3d ZAxis => new Vec3d(0.0, 0.0, 1.0);

        //Operator Overloads
        /// <summary>
        /// * returns dot product
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double operator * (Vec3d a, Vec3d b)
        {
            return DotProduct(a, b);
        }

        public static Vec3d operator * (double f, Vec3d v)
        {
            v.Scale(f);
            return v;
        }

        public static Vec3d operator + (Vec3d a, Vec3d b)
        {
            return Add(a, b);
        }
        
        public static Vec3d operator - (Vec3d a, Vec3d b)
        {
            return Subtract(a, b);
        }

        //Methods
        /// <summary>
        /// Returns the magnitude (absolute length) of of a vector given its three components.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns>double</returns>
        private static double Magnitude(double a, double b, double c)
        {
            double x = Abs(Sqrt((a * a) + (b * b) + (c * c)));
            return x;
        }

        /// <summary>
        /// Unitizes an existing 3d vector by reference
        /// </summary>
        /// <param name="v"></param>
        private static void Normalize(ref Vec3d v)
        {
            v.X = v.X / v.M;
            v.Y = v.Y / v.M;
            v.Z = v.Z / v.M;
            v.M = 1;
        }

        /// <summary>
        /// Returns a new normalized version of a 3d vector 
        /// </summary>
        /// <param name="v"></param>
        /// <returns>Vec2d</returns>
        private static Vec3d Normalize(Vec3d v)
        {
            Vec3d vN = new Vec3d();
            vN.X = v.X / v.M;
            vN.Y = v.Y / v.M;
            vN.Z = v.Z / v.M;
            vN.M = 1.0;
            return vN;
        }

        /// <summary>
        /// Scale all components of a vector such that its magnitude is equal to 1
        /// </summary>
        private void Normalize()
        {
            this.X = this.X / this.M;
            this.Y = this.Y / this.M;
            this.Z = this.Z / this.M;
            this.M = 1.0;
        }

        /// <summary>
        /// Uniformly scale all three components of a vector
        /// </summary>
        /// <param name="f"></param>
        public void Scale(double f)
        {
            this.X *= f;
            this.Y *= f;
            this.Z *= f;
            this.M *= f;
        }

        /// <summary>
        /// Returns the numeric dot product of two vectors
        /// </summary>
        /// <param name="u"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static double DotProduct(Vec3d u, Vec3d v)
        {
            return (u.X * v.X) + (u.Y * v.Y) + (u.Z * v.Z);
        }

        /// <summary>
        /// returns the sum of two vectors 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vec3d Add(Vec3d a, Vec3d b)
        {
            Vec3d result = new Vec3d(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
            result.M = a.M + b.M;
            return result;
        }

        /// <summary>
        /// returns the result of subtracting one vector from another
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vec3d Subtract(Vec3d a, Vec3d b)
        {
            Vec3d result = new Vec3d(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
            result.M = a.M - b.M;
            return result;
        }

    }
}
