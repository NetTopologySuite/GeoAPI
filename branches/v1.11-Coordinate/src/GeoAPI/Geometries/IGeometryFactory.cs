using System.Collections.Generic;

namespace GeoAPI.Geometries
{
    /// <summary>
    /// 
    /// </summary>
    public interface IGeometryFactory
    {
        ICoordinateSequenceFactory CoordinateSequenceFactory { get; }

        int SRID { get; }
        IPrecisionModel PrecisionModel { get; }
                
        IGeometry BuildGeometry(ICollection<IGeometry> geomList);
        IGeometry CreateGeometry(IGeometry g);
        
        IPoint CreatePoint(Coordinate coordinate);
        IPoint CreatePoint(ICoordinateSequence coordinates);        

        ILineString CreateLineString(Coordinate[] coordinates);
        ILineString CreateLineString(ICoordinateSequence coordinates);

        ILinearRing CreateLinearRing(Coordinate[] coordinates);
        ILinearRing CreateLinearRing(ICoordinateSequence coordinates);

        IPolygon CreatePolygon(ILinearRing shell, ILinearRing[] holes);

        IMultiPoint CreateMultiPoint(Coordinate[] coordinates);
        IMultiPoint CreateMultiPoint(IPoint[] point);
        IMultiPoint CreateMultiPoint(ICoordinateSequence coordinates);

        IMultiLineString CreateMultiLineString(ILineString[] lineStrings);
        
        IMultiPolygon CreateMultiPolygon(IPolygon[] polygons);
        
        IGeometryCollection CreateGeometryCollection(IGeometry[] geometries);

        IGeometry ToGeometry(IEnvelope envelopeInternal);
    }
}
