using System.Collections.Generic;

namespace Personal.Study.Data.Object
{
    public class ObjectInfo
    {
        public int LabelId;
        public BoxData BoundingBox;
        public List<List<double>> OutlinePoints;
        public double MaxArea;
        public byte IsCrowd;
    }

    public struct BoxData
    {
        public int X;
        public int Y;
        public int Width;
        public int Height;

        public double[] ToArray()
            => new double[]
                {
                    this.X,
                    this.Y,
                    this.Width,
                    this.Height
                };
    }
}
