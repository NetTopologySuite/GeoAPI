// Copyright 2006 - 2008: Rory Plaire (codekaizen@gmail.com)
//
// This file is part of GeoAPI.Net.
// GeoAPI.Net is free software; you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
// 
// GeoAPI.Net is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.

// You should have received a copy of the GNU Lesser General Public License
// along with GeoAPI.Net; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA 

using System;
using System.IO;

namespace GeoAPI.IO
{
    /// <summary>
    /// A <see cref="Stream"/> which holds another stream and delegates all calls to it except for 
    /// the call to <see cref="Dispose"/>.
    /// </summary>
    /// <remarks>
    /// This <see cref="Stream"/> is useful when creating a <see cref="BinaryReader"/> on a 
    /// <see cref="MemoryStream"/> or other lightweight, reusable stream, since the 
    /// <see cref="BinaryReader"/> will dispose this stream, and not the underlying stream, 
    /// allowing it to be repositioned and reused.
    /// </remarks>
    public class NondisposingStream : Stream
    {
        private Stream _baseStream;
        private Boolean _isDisposed;

        /// <summary>
        /// Creates a new instance of a <see cref="NondisposingStream"/> with the
        /// given <paramref name="baseStream"/> as the underlying stream to delegate to.
        /// </summary>
        /// <param name="baseStream">
        /// The stream to delegate all calls, 
        /// except <see cref="Dispose"/> to.
        /// </param>
        public NondisposingStream(Stream baseStream)
        {
            _baseStream = baseStream;
        }

        public override Boolean CanRead
        {
            get
            {
                checkState();
                return _baseStream.CanRead;
            }
        }

        public override Boolean CanSeek
        {
            get
            {
                checkState();
                return _baseStream.CanSeek;
            }
        }

        public override Boolean CanWrite
        {
            get
            {
                checkState();
                return _baseStream.CanWrite;
            }
        }

        public override void Flush()
        {
            checkState();
            _baseStream.Flush();
        }

        public override Int64 Length
        {
            get
            {
                checkState();
                return _baseStream.Length;
            }
        }

        public override Int64 Position
        {
            get
            {
                checkState();
                return _baseStream.Position;
            }
            set
            {
                checkState();
                _baseStream.Position = value;
            }
        }

        public override Int32 Read(Byte[] buffer, Int32 offset, Int32 count)
        {
            checkState();
            return _baseStream.Read(buffer, offset, count);
        }

        public override Int64 Seek(Int64 offset, SeekOrigin origin)
        {
            checkState();
            return _baseStream.Seek(offset, origin);
        }

        public override void SetLength(Int64 value)
        {
            checkState();
            _baseStream.SetLength(value);
        }

        public override void Write(Byte[] buffer, Int32 offset, Int32 count)
        {
            checkState();
            _baseStream.Write(buffer, offset, count);
        }

        public override void Close() {}

        protected override void Dispose(Boolean disposing)
        {
            IsDisposed = true;
            _baseStream = null;
        }

        /// <summary>
        /// Gets a value indicating if this stream is in a disposed state 
        /// and therefore is unusable.
        /// </summary>
        public Boolean IsDisposed
        {
            get { return _isDisposed; }
            private set { _isDisposed = value; }
        }

        private void checkState()
        {
            if (IsDisposed)
            {
                throw new ObjectDisposedException(GetType().ToString());
            }
        }
    }
}