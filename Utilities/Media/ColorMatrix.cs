﻿/*
Copyright (c) 2012 <a href="http://www.gutgames.com">James Craig</a>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.*/

#region Usings

using System;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Drawing.Imaging;

#endregion Usings

namespace Utilities.Media
{
    /// <summary>
    /// Helper class for setting up and applying a color matrix
    /// </summary>
    public class ColorMatrix
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ColorMatrix()
        {
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Matrix containing the values of the ColorMatrix
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        public virtual float[][] Matrix { get; set; }

        #endregion Properties

        #region Public Functions

        /// <summary>
        /// Applies the color matrix
        /// </summary>
        /// <param name="OriginalImage">Image sent in</param>
        /// <returns>An image with the color matrix applied</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public virtual Bitmap Apply(Bitmap OriginalImage)
        {
            Contract.Requires<ArgumentNullException>(OriginalImage != null, "OriginalImage");
            Bitmap NewBitmap = new Bitmap(OriginalImage.Width, OriginalImage.Height);
            using (Graphics NewGraphics = Graphics.FromImage(NewBitmap))
            {
                System.Drawing.Imaging.ColorMatrix NewColorMatrix = new System.Drawing.Imaging.ColorMatrix(Matrix);
                using (ImageAttributes Attributes = new ImageAttributes())
                {
                    Attributes.SetColorMatrix(NewColorMatrix);
                    NewGraphics.DrawImage(OriginalImage,
                        new System.Drawing.Rectangle(0, 0, OriginalImage.Width, OriginalImage.Height),
                        0, 0, OriginalImage.Width, OriginalImage.Height,
                        GraphicsUnit.Pixel,
                        Attributes);
                }
            }
            return NewBitmap;
        }

        #endregion Public Functions
    }
}