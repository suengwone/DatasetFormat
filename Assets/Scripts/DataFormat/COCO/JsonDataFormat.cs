using System;
using System.Collections.Generic;
using UnityEngine;

namespace Personal.Study.Data.Format
{
    [Serializable]
    public class JsonDataFormat : DataBase
    {
        [SerializeField] public DataInfo info = new();
        [SerializeField] public List<ImageInfo> images = new();
        [SerializeField] public List<AnnotationData> annotations = new();
        [SerializeField] public List<CategoryInfo> categories = new();

        public void Release()
        {
            info = null;
            images = null;
            annotations.ForEach(annotation => 
            {
                annotation.Release();
            });
            categories = null;
        }
    }

    [Serializable]
    public class DataInfo : DataBase
    {
        public string description;
        public string version;
        public int year;
        public string contributor;
        public string date_created;
    }

    [Serializable]
    public class ImageInfo : DataBase
    {
        public int id;
        public int width;
        public int height;
        public string file_name;
        public string date_captured;
        public string extension;
        public string site;
    }

    [Serializable]
    public class AnnotationData : DataBase
    {
        public int id;
        public int image_id;
        public int category_id;
        public List<List<double>> segmentation;
        public double area;
        public double[] bbox;
        public byte isCrowd;

        public void Release()
        {
            segmentation.ForEach(x => 
            {
                x = null;
            });
            segmentation = null;
        }
    }

    [Serializable]
    public class CategoryInfo : DataBase
    {
        public string supercategory;
        public int id;
        public string name;
    }
}
