
using System.Collections.Generic;

namespace Personal.Study.Data.Format
{
    public class TextDataFormat : DataBase
    {
        public List<TextDataInOneLine> TextDataLines = new();
        public void Reset()
            => TextDataLines.Clear();
        public void Release()
            => TextDataLines = null;
    }
    public struct TextDataInOneLine
    {
        public int category_id;
        public double[] boundingBox;
    }
}
