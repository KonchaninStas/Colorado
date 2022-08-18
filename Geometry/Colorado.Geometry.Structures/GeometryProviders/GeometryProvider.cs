using Colorado.Rendering.Materials;
using System;

namespace Colorado.Geometry.Structures.GeometryProviders
{
    public interface IGeometryProvider : IEquatable<IGeometryProvider>
    {
        int Id { get; }
        IMaterial Material { get; }
    }

    public abstract class GeometryProvider : IGeometryProvider
    {
        #region Private fields

        private static int _lastId = 0;

        #endregion Private fields

        #region Constructor

        public GeometryProvider(IMaterial material)
        {
            Id = _lastId++;
            Material = material;
        }

        #endregion Constructor

        #region Properties

        public int Id { get; }

        public IMaterial Material { get; }

        #endregion Properties

        #region Public logic

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (GetType() != obj.GetType())
            {
                return false;
            }

            return Equals((IGeometryProvider)obj);
        }

        public bool Equals(IGeometryProvider other)
        {
            if (other == null)
            {
                return false;
            }

            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        #endregion Public logic
    }
}
