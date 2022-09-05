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
        /// Constructs a Vec3d object from 3 its three vC (x,y,z)
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

        /// <summary>
        /// Constructs a Vec3d object from a Vec2d object and a z component
        /// </summary>
        /// <param name="v"></param>
        /// <param name="z"></param>
        public Vec3d(Vec2d v, double z)
        {
            this.X = v.X;
            this.Y = v.Y;
            this.Z = z;
            this.M = Magnitude(v.X, v.Y, z);
        }

        /// <summary>
        /// Constructs a Vec3d with the same XYZ vC as a Pt3d
        /// </summary>
        /// <param name="pt3d"></param>
        public Vec3d(Pt3d pt3d)
        {
            this.X = pt3d.X;
            this.Y = pt3d.Y;
            this.Z = pt3d.Z;
            this.M = Magnitude(pt3d.X, pt3d.Y, pt3d.Z);
        }

        public Vec3d(Pt3d start, Pt3d end)
        {
            this.X = end.X - start.X;
            this.Y = end.Y - start.Y;
            this.Z = end.Z - start.Z;
            this.M = Magnitude(this.X, this.Y, this.Z);
        }

        //Delegates
        /// <summary>
        /// Gets Vector with Default XAxis vC (1.0, 0.0, 0.0)
        /// </summary>
        public static Vec3d XAxis => new Vec3d(1.0, 0.0, 0.0);
        /// <summary>
        /// Gets Vector with Default XAxis vC (0.0, 1.0, 0.0)
        /// </summary>
        public static Vec3d YAxis => new Vec3d(0.0, 1.0, 0.0);
        
        /// <summary>
        /// Gets Vector with Default XAxis vC (0.0, 0.0, 1.0)
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
        /// Returns the magnitude (absolute length) of of a vector given its three vC.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns>double</returns>
        public static double Magnitude(double a, double b, double c)
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
        public static Vec3d Normalize(Vec3d v)
        {
            Vec3d vN = new Vec3d();
            vN.X = v.X / v.M;
            vN.Y = v.Y / v.M;
            vN.Z = v.Z / v.M;
            vN.M = 1.0;
            return vN;
        }

        /// <summary>
        /// Scale all vC of a vector such that its magnitude is equal to 1
        /// </summary>
        public void Normalize()
        {
            this.X = this.X / this.M;
            this.Y = this.Y / this.M;
            this.Z = this.Z / this.M;
            this.M = 1.0;
        }

        /// <summary>
        /// Uniformly scale all three vC of a vector
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
        /// <returns>Vec3d</returns>
        public static Vec3d Subtract(Vec3d a, Vec3d b)
        {
            Vec3d result = new Vec3d(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
            result.M = a.M - b.M;
            return result;
        }

        /// <summary>
        /// returns a new Vec2d with matching X and Y vC as a Vec3d 
        /// </summary>
        /// <param name="v"></param>
        /// <returns>Vec2d</returns>
        public static Vec2d ToVec2d(Vec3d v)
        {
            return new Vec2d(v);
        }

        /// <summary>
        /// returns the angle between two 3d vectors in radians
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>Vec3d</returns>
        public static double Angle(Vec3d a, Vec3d b)
        {
            Vec3d aN = Normalize(a);
            Vec3d bN = Normalize(b);

            double d = (a.X * b.X) + (a.Y * b.Y) + (a.Z * b.Z);
            if (d > 1.0)
                d = 1.0;
            if (d < -1.0)
                d = -1.0;
            return Acos(d);
        }

        public static double Reflex(Vec3d a, Vec3d b)
        {
            return (2 * PI) - Angle(a, b);
        }

        public static Vec3d PerpTo(Vec3d v)
        {
          double[] vC = new double[3];  
          int i, j, k; 
          double a, b;
          k = 2;
          if (Abs(v.Y) > Abs(v.X)) 
          {
            if (Abs(v.Z) > Abs(v.Y)) 
            {
              i = 2;
              j = 1;
              k = 0;
              a = v.Z;
              b = -v.Y;
            }
            else if (Abs(v.Z) >= Abs(v.X))
            {
              i = 1;
              j = 2;
              k = 0;
              a = v.Y;
              b = -v.Z;
            }
            else 
            {
              i = 1;
              j = 0;
              k = 2;
              a = v.Y;
              b = -v.X;
            }
          }
          else if (Abs(v.Z) > Abs(v.X)) 
          {
            i = 2;
            j = 0;
            k = 1;
            a = v.Z;
            b = -v.X;
          }
          else if (Abs(v.Z) > Abs(v.Y)) 
          {
            i = 0;
            j = 2;
            k = 1;
            a = v.X;
            b = -v.Z;
          }
          else 
          {
            i = 0;
            j = 1;
            k = 2;
            a = v.X;
            b = -v.Y;
          }
          
          vC[i] = b;
          vC[j] = a;
          vC[k] = 0.0;
          
          return new Vec3d(vC[0], vC[0], vC[0]);
        }

        /// <summary>
        /// returns the Vec3d cross product vector of two 2d vectors
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>Vec3d</returns>
        public static Vec3d CrossProduct(Vec2d a ,Vec2d b)
        {
            return new Vec3d(0.0, 0.0, a.X * b.Y - b.X * a.Y);
        } 
         
        /// <summary>
        /// returns the Vec3d cross product vector of two 3d vectors
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>Vec3d</returns>
        public static Vec3d CrossProduct(Vec3d a, Vec3d b)
        {
            return new Vec3d((a.Y * b.Z) - (b.Y * a.Z), 
                             (a.Z * b.X) - (b.Z * a.X), 
                             (a.X * b.Y) - (b.X * a.Y));
        }
        
        /// <summary>
        /// Returns true if two Vec3d vectors are parallel within a given absolute tolerance. Polar opposite vectors are considered parallel
        /// </summary>
        /// <param name="u"></param>
        /// <param name="v"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static bool IsParallel(Vec3d u, Vec3d v, double tolerance)
        {
            double cX = u.Y * v.Z + u.Z * v.Y;
            double cY = u.Z * v.X + u.X * v.Z;
            double cZ = u.X * v.Y + u.Y * v.X;
            double cN = cX * cX + cY * cY + cZ * cZ;
            return cN < tolerance; 
        }

        public static bool IsParallel(Vec3d u, Vec3d v, double tolerance, out bool inverted)
        {
            inverted = false;
            double cX = u.Y * v.Z + u.Z * v.Y;
            double cY = u.Z * v.X + u.X * v.Z;
            double cZ = u.X * v.Y + u.Y * v.X;
            double cN = cX * cX + cY * cY + cZ * cZ;
            if (u.Z * v.Z < 0)
            {
                inverted = true;
            }
            return cN < tolerance;
        }
    }
}
