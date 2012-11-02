using System;
using System.Collections.Generic;
using NPack;
using NPack.Interfaces;

namespace GeoAPI.Coordinates
{
    public struct BufferedCoordinate2D : ICoordinate2D, 
        IBufferedVector<DoubleComponent>, IEquatable<BufferedCoordinate2D>, 
        IComparable<BufferedCoordinate2D>, IComputable<BufferedCoordinate2D>, 
        IConvertible
    {
        #region ICoordinate2D Members

        public Double X
        {
            get { throw new NotImplementedException(); }
        }

        public Double Y
        {
            get { throw new NotImplementedException(); }
        }

        public Double Distance(ICoordinate2D other)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ICoordinate Members

        public Boolean ContainsOrdinate(Ordinates ordinate)
        {
            throw new NotImplementedException();
        }

        public Double Distance(ICoordinate other)
        {
            throw new NotImplementedException();
        }

        public Boolean IsEmpty
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Double this[Ordinates ordinate]
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IVector<DoubleComponent> Members

        public IVector<DoubleComponent> Clone()
        {
            throw new NotImplementedException();
        }

        public Int32 ComponentCount
        {
            get { throw new NotImplementedException(); }
        }

        public DoubleComponent[] Components
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

        public IVector<DoubleComponent> Negative()
        {
            throw new NotImplementedException();
        }

        public DoubleComponent this[Int32 index]
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

        #region IMatrix<DoubleComponent> Members

        IMatrix<DoubleComponent> IMatrix<DoubleComponent>.Clone()
        {
            throw new NotImplementedException();
        }

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

        public IMatrix<DoubleComponent> Inverse
        {
            get { throw new NotImplementedException(); }
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

        public IMatrix<DoubleComponent> Transpose()
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

        #region INegatable<IMatrix<DoubleComponent>> Members

        IMatrix<DoubleComponent> INegatable<IMatrix<DoubleComponent>>.Negative()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ISubtractable<IMatrix<DoubleComponent>> Members

        public IMatrix<DoubleComponent> Subtract(IMatrix<DoubleComponent> b)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IHasZero<IMatrix<DoubleComponent>> Members

        public IMatrix<DoubleComponent> Zero
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IAddable<IMatrix<DoubleComponent>> Members

        public IMatrix<DoubleComponent> Add(IMatrix<DoubleComponent> b)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IDivisible<IMatrix<DoubleComponent>> Members

        public IMatrix<DoubleComponent> Divide(IMatrix<DoubleComponent> b)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IHasOne<IMatrix<DoubleComponent>> Members

        public IMatrix<DoubleComponent> One
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IMultipliable<IMatrix<DoubleComponent>> Members

        public IMatrix<DoubleComponent> Multiply(IMatrix<DoubleComponent> b)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEquatable<IMatrix<DoubleComponent>> Members

        public Boolean Equals(IMatrix<DoubleComponent> other)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable<DoubleComponent> Members

        public IEnumerator<DoubleComponent> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ISubtractable<IVector<DoubleComponent>> Members

        public IVector<DoubleComponent> Subtract(IVector<DoubleComponent> b)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IHasZero<IVector<DoubleComponent>> Members

        IVector<DoubleComponent> IHasZero<IVector<DoubleComponent>>.Zero
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IAddable<IVector<DoubleComponent>> Members

        public IVector<DoubleComponent> Add(IVector<DoubleComponent> b)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IDivisible<IVector<DoubleComponent>> Members

        public IVector<DoubleComponent> Divide(IVector<DoubleComponent> b)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IHasOne<IVector<DoubleComponent>> Members

        IVector<DoubleComponent> IHasOne<IVector<DoubleComponent>>.One
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IMultipliable<IVector<DoubleComponent>> Members

        public IVector<DoubleComponent> Multiply(IVector<DoubleComponent> b)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IComparable Members

        public Int32 CompareTo(Object obj)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEquatable<ICoordinate> Members

        public Boolean Equals(ICoordinate other)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IComparable<ICoordinate2D> Members

        public Int32 CompareTo(ICoordinate2D other)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEquatable<ICoordinate2D> Members

        public Boolean Equals(ICoordinate2D other)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IBufferedVector<DoubleComponent> Members

        public IVectorBuffer<TVector, DoubleComponent> GetBuffer<TVector>() where TVector : IBufferedVector<DoubleComponent>
        {
            throw new NotImplementedException();
        }

        public Int32 Index
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IEquatable<BufferedCoordinate2D> Members

        public bool Equals(BufferedCoordinate2D other)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IComparable<BufferedCoordinate2D> Members

        public int CompareTo(BufferedCoordinate2D other)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IComputable<BufferedCoordinate2D> Members

        public BufferedCoordinate2D Abs()
        {
            throw new NotImplementedException();
        }

        public BufferedCoordinate2D Set(double value)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region INegatable<BufferedCoordinate2D> Members

        BufferedCoordinate2D INegatable<BufferedCoordinate2D>.Negative()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ISubtractable<BufferedCoordinate2D> Members

        public BufferedCoordinate2D Subtract(BufferedCoordinate2D b)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IHasZero<BufferedCoordinate2D> Members

        BufferedCoordinate2D IHasZero<BufferedCoordinate2D>.Zero
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IAddable<BufferedCoordinate2D> Members

        public BufferedCoordinate2D Add(BufferedCoordinate2D b)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IDivisible<BufferedCoordinate2D> Members

        public BufferedCoordinate2D Divide(BufferedCoordinate2D b)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IHasOne<BufferedCoordinate2D> Members

        BufferedCoordinate2D IHasOne<BufferedCoordinate2D>.One
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IMultipliable<BufferedCoordinate2D> Members

        public BufferedCoordinate2D Multiply(BufferedCoordinate2D b)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IBooleanComparable<BufferedCoordinate2D> Members

        public bool GreaterThan(BufferedCoordinate2D value)
        {
            throw new NotImplementedException();
        }

        public bool GreaterThanOrEqualTo(BufferedCoordinate2D value)
        {
            throw new NotImplementedException();
        }

        public bool LessThan(BufferedCoordinate2D value)
        {
            throw new NotImplementedException();
        }

        public bool LessThanOrEqualTo(BufferedCoordinate2D value)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IExponential<BufferedCoordinate2D> Members

        public BufferedCoordinate2D Exp()
        {
            throw new NotImplementedException();
        }

        public BufferedCoordinate2D Log()
        {
            throw new NotImplementedException();
        }

        public BufferedCoordinate2D Log(double newBase)
        {
            throw new NotImplementedException();
        }

        public BufferedCoordinate2D Power(double exponent)
        {
            throw new NotImplementedException();
        }

        public BufferedCoordinate2D Sqrt()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IConvertible Members

        public TypeCode GetTypeCode()
        {
            throw new NotImplementedException();
        }

        public bool ToBoolean(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public byte ToByte(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public char ToChar(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public decimal ToDecimal(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public double ToDouble(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public short ToInt16(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public int ToInt32(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public long ToInt64(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public sbyte ToSByte(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public float ToSingle(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public string ToString(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public object ToType(Type conversionType, IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public ushort ToUInt16(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public uint ToUInt32(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public ulong ToUInt64(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
