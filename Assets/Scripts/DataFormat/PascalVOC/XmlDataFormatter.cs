using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

namespace Personal.Study.Data.Format
{
    using Data.Object;
    public class XmlDataFormatter
    {
        public annotation xmlData = new annotation();
        private Dictionary<int, string> labelDatas = new ();

        public void SetLabelData(SortedList<int, (string category, string name)> labelDataSets)
        {
            foreach (var labelDataSet in labelDataSets)
            {
                labelDatas[labelDataSet.Key] = labelDataSet.Value.name;
            }
        }

        public void AddImageInfo(List<ObjectInfo> objectDatas, Vector2Int resolution, string fileName)
        {
            xmlData.folder = "Images";
            xmlData.filename = fileName;

            xmlData.size.width = resolution.x;
            xmlData.size.height = resolution.y;
            xmlData.size.depth = 3;

            xmlData.segmented = 0;

            objectDatas.ForEach(objectData => 
            {
                int xMax = objectData.BoundingBox.X + objectData.BoundingBox.Width;
                int yMax = objectData.BoundingBox.Y + objectData.BoundingBox.Height;
                if(xMax >= xmlData.size.width)
                    xMax = xmlData.size.width - 1;
                if(yMax >= xmlData.size.height)
                    yMax = xmlData.size.height - 1;

                xmlData.@object.Add(new Object()
                {
                    name = labelDatas[objectData.LabelId],
                    pose = "Unspecified",
                    truncated = objectData.IsCrowd,
                    occluded = 0,
                    difficult = 0,
                    bndbox = new BoungingBox()
                    {
                        xmin = objectData.BoundingBox.X,
                        ymin = objectData.BoundingBox.Y,
                        xmax = xMax,
                        ymax = yMax,
                    },
                });
            });
        } 

        public string GetDataAsXml()
        {
            XmlSerializer xmlSerializer = new (xmlData.GetType());
            XmlSerializerNamespaces emptyNamespaces = new (new[] { XmlQualifiedName.Empty });
            XmlWriterSettings settings = new()
            {
                Indent = true,
                OmitXmlDeclaration = true,
            };

            using(var textWriter = new StringWriter())
            using(var writer = XmlWriter.Create(textWriter, settings))
            {
                xmlSerializer.Serialize(writer, xmlData, emptyNamespaces);
                return textWriter.ToString();
            }
        }

        public void ResetData()
            => xmlData.Reset();
        public void Release()
        {
            xmlData.Release();
            labelDatas = null;
        }
    }
}
