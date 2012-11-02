using System;
using System.Collections.Generic;
using GeoAPI.Coordinates;
using NPack;
using NPack.Interfaces;

namespace GeoAPI.Coordinates
{
    public class AffineTransformMatrix<TCoordinate> : IAffineTransformMatrix<DoubleComponent>, IEquatable<AffineTransformMatrix<TCoordinate>>, IComparable<AffineTransformMatrix<TCoordinate>>
        where TCoordinate : ICoordinate, IEquatable<TCoordinate>, IComparable<TCoordinate>,
                            IComputable<Double, TCoordinate>, IConvertible
    {
        public AffineTransformMatrix<TCoordinate> Inverse
        {
            get { throw new NotImplementedException(); }
        }

        public AffineTransformMatrix<TCoordinate> Clone()
        {
            throw new NotImplementedException();
        }

        public AffineTransformMatrix<TCoordinate> Negative()
        {
            throw new NotImplementedException();
        }

        public AffineTransformMatrix<TCoordinate> Subtract(AffineTransformMatrix<TCoordinate> b)
        {
            throw new NotImplementedException();
        }

        public AffineTransformMatrix<TCoordinate> Zero
        {
            get { throw new NotImplementedException(); }
        }

        public AffineTransformMatrix<TCoordinate> Add(AffineTransformMatrix<TCoordinate> b)
        {
            throw new NotImplementedException();
        }

        public AffineTransformMatrix<TCoordinate> Divide(AffineTransformMatrix<TCoordinate> b)
        {
            throw new NotImplementedException();
        }

        public AffineTransformMatrix<TCoordinate> One
        {
            get { throw new NotImplementedException(); }
        }

        public AffineTransformMatrix<TCoordinate> Multiply(AffineTransformMatrix<TCoordinate> b)
        {
            throw new NotImplementedException();
        }

        #region IAffineTransformMatrix<DoubleComponent> Members

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public void RotateAt(TCoordinate point, TCoordinate axis, Double radians, MatrixOperationOrder order)
        {
            throw new NotImplementedException();
        }

        public void RotateAt(TCoordinate point, TCoordinate axis, Double radians)
        {
            throw new NotImplementedException();
        }

        public void Translate(TCoordinate translateVector, MatrixOperationOrder order)
        {
            throw new NotImplementedException();
        }

        public void Translate(TCoordinate translateVector)
        {
            throw new NotImplementedException();
        }

        public void Translate(Double amount, MatrixOperationOrder order)
        {
            throw new NotImplementedException();
        }

        public void Translate(Double amount)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ITransformMatrix<DoubleComponent> Members

        public void RotateAlong(TCoordinate axis, Double radians, MatrixOperationOrder order)
        {
            throw new NotImplementedException();
        }

        public void RotateAlong(TCoordinate axis, Double radians)
        {
            throw new NotImplementedException();
        }

        public void Scale(TCoordinate scaleVector, MatrixOperationOrder order)
        {
            throw new NotImplementedException();
        }

        public void Scale(TCoordinate scaleVector)
        {
            throw new NotImplementedException();
        }

        public void Scale(Double amount, MatrixOperationOrder order)
        {
            throw new NotImplementedException();
        }

        public void Scale(Double amount)
        {
            throw new NotImplementedException();
        }

        public void Shear(TCoordinate shearVector, MatrixOperationOrder order)
        {
            throw new NotImplementedException();
        }

        public void Shear(TCoordinate shearVector)
        {
            throw new NotImplementedException();
        }

        public AffineTransformMatrix<TCoordinate> TransformMatrix(AffineTransformMatrix<TCoordinate> input)
        {
            throw new NotImplementedException();
        }

        public void TransformVector(Double[] input)
        {
            throw new NotImplementedException();
        }

        public TCoordinate TransformVector(TCoordinate input)
        {
            throw new NotImplementedException();
        }

        public void TransformVectors(IEnumerable<Double[]> input)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TCoordinate> TransformVectors(IEnumerable<TCoordinate> input)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IMatrix<DoubleComponent> Members

        public Int32 ColumnCount
        {
            get { throw new NotImplementedException(); }
        }

        public Double Determinant
        {
            get { throw new NotImplementedException(); }
        }

        public MatrixFormat Format
        {
            get { throw new NotImplementedException(); }
        }

        public IMatrix<DoubleComponent> GetMatrix(Int32[] rowIndexes, Int32 startColumn, Int32 endColumn)
        {
            throw new NotImplementedException();
        }

        public Boolean IsInvertible
        {
            get { throw new NotImplementedException(); }
        }

        public Boolean IsSingular
        {
            get { throw new NotImplementedException(); }
        }

        public Boolean IsSquare
        {
            get { throw new NotImplementedException(); }
        }

        public Boolean IsSymmetrical
        {
            get { throw new NotImplementedException(); }
        }

        public Int32 RowCount
        {
            get { throw new NotImplementedException(); }
        }

        public AffineTransformMatrix<TCoordinate> Transpose()
        {
            throw new NotImplementedException();
        }

        public DoubleComponent this[Int32 row, Int32 column]
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region IEquatable<AffineTransformMatrix<TCoordinate>> Members

        public Boolean Equals(AffineTransformMatrix<TCoordinate> other)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IComparable<AffineTransformMatrix<TCoordinate>> Members

        public Int32 CompareTo(AffineTransformMatrix<TCoordinate> other)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IBooleanComparable<IMatrix<DoubleComponent>> Members

        public Boolean GreaterThan(IMatrix<DoubleComponent> value)
        {
            throw new NotImplementedException();
        }

        public Boolean GreaterThanOrEqualTo(IMatrix<DoubleComponent> value)
        {
            throw new NotImplementedException();
        }

        public Boolean LessThan(IMatrix<DoubleComponent> value)
        {
            throw new NotImplementedException();
        }

        public Boolean LessThanOrEqualTo(IMatrix<DoubleComponent> value)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IAffineTransformMatrix<DoubleComponent> Members

        IAffineTransformMatrix<DoubleComponent> IAffineTransformMatrix<DoubleComponent>.Inverse
        {
            get { throw new NotImplementedException(); }
        }

        void IAffineTransformMatrix<DoubleComponent>.RotateAt(IVector<DoubleComponent> point, IVector<DoubleComponent> axis, Double radians, MatrixOperationOrder order)
        {
            throw new NotImplementedException();
        }

        void IAffineTransformMatrix<DoubleComponent>.RotateAt(IVector<DoubleComponent> point, IVector<DoubleComponent> axis, Double radians)
        {
            throw new NotImplementedException();
        }

        void IAffineTransformMatrix<DoubleComponent>.Translate(IVector<DoubleComponent> translateVector, MatrixOperationOrder order)
        {
            throw new NotImplementedException();
        }

        void IAffineTransformMatrix<DoubleComponent>.Translate(IVector<DoubleComponent> translateVector)
        {
            throw new NotImplementedException();
        }

        void IAffineTransformMatrix<DoubleComponent>.Translate(DoubleComponent amount, MatrixOperationOrder order)
        {
            throw new NotImplementedException();
        }

        void IAffineTransformMatrix<DoubleComponent>.Translate(DoubleComponent amount)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ITransformMatrix<DoubleComponent> Members

        void ITransformMatrix<DoubleComponent>.RotateAlong(IVector<DoubleComponent> axis, Double radians, MatrixOperationOrder order)
        {
            throw new NotImplementedException();
        }

        void ITransformMatrix<DoubleComponent>.RotateAlong(IVector<DoubleComponent> axis, Double radians)
        {
            throw new NotImplementedException();
        }

        void ITransformMatrix<DoubleComponent>.Scale(IVector<DoubleComponent> scaleVector, MatrixOperationOrder order)
        {
            throw new NotImplementedException();
        }

        void ITransformMatrix<DoubleComponent>.Scale(IVector<DoubleComponent> scaleVector)
        {
            throw new NotImplementedException();
        }

        void ITransformMatrix<DoubleComponent>.Scale(DoubleComponent amount, MatrixOperationOrder order)
        {
            throw new NotImplementedException();
        }

        void ITransformMatrix<DoubleComponent>.Scale(DoubleComponent amount)
        {
            throw new NotImplementedException();
        }

        void ITransformMatrix<DoubleComponent>.Shear(IVector<DoubleComponent> shearVector, MatrixOperationOrder order)
        {
            throw new NotImplementedException();
        }

        void ITransformMatrix<DoubleComponent>.Shear(IVector<DoubleComponent> shearVector)
        {
            throw new NotImplementedException();
        }

        IMatrix<DoubleComponent> ITransformMatrix<DoubleComponent>.TransformMatrix(IMatrix<DoubleComponent> input)
        {
            throw new NotImplementedException();
        }

        void ITransformMatrix<DoubleComponent>.TransformVector(DoubleComponent[] input)
        {
            throw new NotImplementedException();
        }

        IVector<DoubleComponent> ITransformMatrix<DoubleComponent>.TransformVector(IVector<DoubleComponent> input)
        {
            throw new NotImplementedException();
        }

        void ITransformMatrix<DoubleComponent>.TransformVectors(IEnumerable<DoubleComponent[]> input)
        {
            throw new NotImplementedException();
        }

        IEnumerable<IVector<DoubleComponent>> ITransformMatrix<DoubleComponent>.TransformVectors(IEnumerable<IVector<DoubleComponent>> input)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IMatrix<DoubleComponent> Members

        IMatrix<DoubleComponent> IMatrix<DoubleComponent>.Clone()
        {
            throw new NotImplementedException();
        }

        IMatrix<DoubleComponent> IMatrix<DoubleComponent>.Inverse
        {
            get { throw new NotImplementedException(); }
        }

        IMatrix<DoubleComponent> IMatrix<DoubleComponent>.Transpose()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEquatable<IMatrix<DoubleComponent>> Members

        Boolean IEquatable<IMatrix<DoubleComponent>>.Equals(IMatrix<DoubleComponent> other)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IComparable<IMatrix<DoubleComponent>> Members

        Int32 IComparable<IMatrix<DoubleComponent>>.CompareTo(IMatrix<DoubleComponent> other)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IComputable<IMatrix<DoubleComponent>> Members

        IMatrix<DoubleComponent> IComputable<IMatrix<DoubleComponent>>.Abs()
        {
            throw new NotImplementedException();
        }

        IMatrix<DoubleComponent> IComputable<IMatrix<DoubleComponent>>.Set(Double value)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region INegatable<IMatrix<DoubleComponent>> Members

        IMatrix<DoubleComponent> INegatable<IMatrix<DoubleComponent>>.Negative()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ISubtractable<IMatrix<DoubleComponent>> Members

        IMatrix<DoubleComponent> ISubtractable<IMatrix<DoubleComponent>>.Subtract(IMatrix<DoubleComponent> b)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IHasZero<IMatrix<DoubleComponent>> Members

        IMatrix<DoubleComponent> IHasZero<IMatrix<DoubleComponent>>.Zero
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IAddable<IMatrix<DoubleComponent>> Members

        IMatrix<DoubleComponent> IAddable<IMatrix<DoubleComponent>>.Add(IMatrix<DoubleComponent> b)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IDivisible<IMatrix<DoubleComponent>> Members

        IMatrix<DoubleComponent> IDivisible<IMatrix<DoubleComponent>>.Divide(IMatrix<DoubleComponent> b)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IHasOne<IMatrix<DoubleComponent>> Members

        IMatrix<DoubleComponent> IHasOne<IMatrix<DoubleComponent>>.One
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IMultipliable<IMatrix<DoubleComponent>> Members

        IMatrix<DoubleComponent> IMultipliable<IMatrix<DoubleComponent>>.Multiply(IMatrix<DoubleComponent> b)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IExponential<IMatrix<DoubleComponent>> Members

        IMatrix<DoubleComponent> IExponential<IMatrix<DoubleComponent>>.Exp()
        {
            throw new NotImplementedException();
        }

        IMatrix<DoubleComponent> IExponential<IMatrix<DoubleComponent>>.Log()
        {
            throw new NotImplementedException();
        }

        IMatrix<DoubleComponent> IExponential<IMatrix<DoubleComponent>>.Log(Double newBase)
        {
            throw new NotImplementedException();
        }

        IMatrix<DoubleComponent> IExponential<IMatrix<DoubleComponent>>.Power(Double exponent)
        {
            throw new NotImplementedException();
        }

        IMatrix<DoubleComponent> IExponential<IMatrix<DoubleComponent>>.Sqrt()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
