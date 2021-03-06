﻿using System;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class FullObjectDetection : DlibObject
    {

        #region Constructors

        internal FullObjectDetection(IntPtr ptr)
        {
            this.NativePtr = ptr;
            this._Parts = NativeMethods.full_object_detection_num_parts(this.NativePtr);
        }

        #endregion

        #region Properties

        private readonly uint _Parts;

        public uint Parts
        {
            get
            {
                this.ThrowIfDisposed();
                return this._Parts;
            }
        }

        public Rectangle Rect
        {
            get
            {
                this.ThrowIfDisposed();
                var rect = NativeMethods.full_object_detection_get_rect(this.NativePtr);
                return new Rectangle(rect);
            }
        }

        #endregion

        #region Methods

        public Point GetPart(uint index)
        {
            this.ThrowIfDisposed();
            if (!(index < this._Parts))
                throw new ArgumentOutOfRangeException(nameof(index));

            var p = NativeMethods.full_object_detection_part(this.NativePtr, index);
            return new Point(p);
        }

        #region Overrides

        /// <summary>
        /// Releases all unmanaged resources.
        /// </summary>
        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            NativeMethods.full_object_detection_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }
}
