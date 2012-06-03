using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using DupliFinder.Properties;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace DupliFinder.Processing
{
    internal class Levenshtein : IComparer<ComparableBitmap>
    {
        public Levenshtein() { }
        /// <SUMMARY>Computes the Levenshtein Edit Distance between two enumerables.</SUMMARY>
        /// <TYPEPARAM name="T">The type of the items in the enumerables.</TYPEPARAM>
        /// <PARAM name="x">The first enumerable.</PARAM>
        /// <PARAM name="y">The second enumerable.</PARAM>
        /// <RETURNS>The edit distance.</RETURNS>
        public int GetDistance<T>(IEnumerable<T> x, IEnumerable<T> y) where T : IEquatable<T>
        {
            // Validate parameters
            if (x == null) throw new ArgumentNullException("x");
            if (y == null) throw new ArgumentNullException("y");

            // Convert the parameters into IList instances
            // in order to obtain indexing capabilities
            IList<T> first = x as IList<T> ?? new List<T>(x);
            IList<T> second = y as IList<T> ?? new List<T>(y);

            // Get the length of both.  If either is 0, return
            // the length of the other, since that number of insertions
            // would be required.
            int n = first.Count, m = second.Count;
            if (n == 0) return m;
            if (m == 0) return n;

            // Rather than maintain an entire matrix (which would require O(n*m) space),
            // just store the current row and the next row, each of which has a length m+1,
            // so just O(m) space. Initialize the current row.
            int curRow = 0, nextRow = 1;
            int[][] rows = new int[][] { new int[m + 1], new int[m + 1] };
            for (int j = 0; j <= m; ++j) rows[curRow][j] = j;

            // For each virtual row (since we only have physical storage for two)
            for (int i = 1; i <= n; ++i)
            {
                // Fill in the values in the row
                rows[nextRow][0] = i;
                for (int j = 1; j <= m; ++j)
                {
                    int dist1 = rows[curRow][j] + 1;
                    int dist2 = rows[nextRow][j - 1] + 1;
                    int dist3 = rows[curRow][j - 1] +
                        (first[i - 1].Equals(second[j - 1]) ? 0 : 1);

                    rows[nextRow][j] = Math.Min(dist1, Math.Min(dist2, dist3));
                }

                // Swap the current and next rows
                if (curRow == 0)
                {
                    curRow = 1;
                    nextRow = 0;
                }
                else
                {
                    curRow = 0;
                    nextRow = 1;
                }
            }

            // Return the computed edit distance
            return rows[curRow][m];
        }



        #region IComparer<string> Members
        ComparableBitmap cBit = new ComparableBitmap();
        public Levenshtein(ComparableBitmap compareBitmap)
        {
            cBit = compareBitmap;
        }
        public int Compare(ComparableBitmap x, ComparableBitmap y)
        {
            return GetDistance<byte>(x, cBit).CompareTo(GetDistance<byte>(y, cBit));
        }

        #endregion
    }

    internal class ComparableBitmap : BitmapGrey, IEnumerable<byte>
    {
        private string m_oPath;

        public string Path
        {
            get { return m_oPath; }
            set { m_oPath = value; }
        }
	
        byte[] data = null;
        internal ComparableBitmap():base(null)
        {
            data = new byte[0];
        }

        internal ComparableBitmap(Bitmap bitmap)
            : base((Bitmap)bitmap.GetThumbnailImage(Settings.Default.CompareImageWidth, Settings.Default.CompareImageWidth * bitmap.Height / bitmap.Width, null, IntPtr.Zero))
        {
            base.MakeGreyUnsafeFaster();
            this.data = getBmpBytes(base.Bitmap);
        }

        public ComparableBitmap(string path):base(null)
        {
            base.bitmap = (Bitmap)Image.FromFile(path);
            base.bitmap = (Bitmap)base.Bitmap.GetThumbnailImage(Settings.Default.CompareImageWidth, Settings.Default.CompareImageWidth * base.Bitmap.Height / base.Bitmap.Width, null, IntPtr.Zero);
            base.MakeGreyUnsafeFaster();
            this.data = getBmpBytes(base.bitmap);
            m_oPath = path;
        }



        byte[] getBmpBytes(Bitmap bmp)
        {
            BitmapData bData = bmp.LockBits(new Rectangle(new Point(), bmp.Size), ImageLockMode.ReadOnly, bmp.PixelFormat);
            int byteCount = bData.Stride * bmp.Height;
            byte[] bmpBytes = new byte[byteCount];

            Marshal.Copy(bData.Scan0, bmpBytes, 0, byteCount);

            bmp.UnlockBits(bData);
            return bmpBytes;

        }

        private int m_sim;

        public int Similarity
        {
            get { return m_sim; }
            set { m_sim = value; }
        }
	

        #region IEnumerable<byte> Members

        public IEnumerator<byte> GetEnumerator()
        {
            if (data == null) return null;
            
            return (IEnumerator<byte>) (new List<byte>(data)).GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {

            if (data == null)
                return null;
            return data.GetEnumerator();
        }

        #endregion
    }
}
