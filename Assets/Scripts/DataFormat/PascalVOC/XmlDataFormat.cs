using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Personal.Study.Data.Format
{
    [Serializable]
    public class annotation : DataBase
    {
        public string folder;
        public string filename;
        public Size size;
        public byte segmented;
        [XmlElement()]
        public List<Object> @object = new ();
        
        public void Release()
            => @object = null;
        public void Reset()
            => @object.Clear();
    }

    public struct Source
    {
        public string database;
        public string annotation;
        public string image;
        public string flickrid;
    }

    public struct Owner
    {
        public string flickrid;
        public string name;
    }

    public struct Size
    {
        public int width;
        public int height;
        public int depth;
    }

    public struct Object
    {
        public string name;
        public string pose;
        public float truncated;
        public int occluded;
        public byte difficult;
        public BoungingBox bndbox;
    }

    public struct BoungingBox
    {
        public int xmin;
        public int ymin;
        public int xmax;
        public int ymax;
    }
}
