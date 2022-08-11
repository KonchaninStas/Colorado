﻿using Colorado.Geometry.Abstractions.Primitives;
using System;

namespace Colorado.Geometry.Structures.Primitives
{
    public class Point : IPoint
    {
        public Point(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public double X { get; }

        public double Y { get; }

        public double Z { get; }

        public static IPoint Zero => new Point(default(double), default(double), default(double));

        public IPoint Inverse => Multiply(-1);

        public IPoint Multiply(double scaleFactor)
        {
            return new Point(X * scaleFactor, Y * scaleFactor, Z * scaleFactor);
        }

        public static Point operator +(Point point, IVector vector)
        {
            return new Point(point.X + vector.X, point.Y + vector.Y, point.Z + vector.Z);
        }

        public double DistanceTo(IPoint secondPoint)
        {
            return System.Math.Abs(Minus(secondPoint).Length);
        }

        public IVector Minus(IPoint right)
        {
            return new Vector(X - right.X, Y - right.Y, Z - right.Z);
        }

        public IPoint Minus(IVector vector)
        {
            return new Point(X - vector.X, Y - vector.Y, Z - vector.Z);
        }

        public IPoint Plus(IVector vector)
        {
            return new Point(X + vector.X, Y + vector.Y, Z + vector.Z);
        }

        public IPoint Plus(IPoint point)
        {
            return new Point(X + point.X, Y + point.Y, Z + point.Z);
        }

        public IPoint Divide(double number)
        {
            return new Point(X / number, Y / number, Z / number);
        }

        public IVector ToVector()
        {
            return new Vector(X, Y, Z);
        }

        private static readonly Random _random = new Random();

        public static IPoint GetRandomPoint()
        {
            return new Point(_random.Next(-100, 100), _random.Next(-100, 100), _random.Next(-100, 100));
        }

        public override string ToString()
        {
            return $"X = {X}, Y = {Y}, Z = {Z}";
        }
    }
}
