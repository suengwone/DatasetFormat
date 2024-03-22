using System;
using System.Collections.Generic;
using UnityEngine;

namespace Personal.Study.Data.Format
{
    using Data.Object;
    public class TextDataFormatter
    {
        public TextDataFormat textData = new ();

        public string GetDataAsText()
        {
            System.Text.StringBuilder result = new ();
            
            textData.TextDataLines.ForEach(data => 
                result
                    .AppendLine($"{data.category_id} {data.boundingBox[0]} {data.boundingBox[1]} {data.boundingBox[2]} {data.boundingBox[3]}")
            );

            return result.ToString();
        }
        public void ResetData()
            => textData.Reset();
        public void Release()
            => textData.Release();
        
        public void AddImageInfo(List<ObjectInfo> objectDatas, Vector2Int resolution)
            => objectDatas.ForEach(objectData => 
                {
                    AddImageInfo(objectData.LabelId, objectData.BoundingBox.ToArray(), resolution);

                });

        private void AddImageInfo(int categoryId, double[] boundingBox, Vector2Int resolution)
            => textData.TextDataLines.Add(new TextDataInOneLine()
                {
                    category_id = categoryId,
                    boundingBox = ToYolo(boundingBox, resolution),
                });
        
        private double[] ToYolo(double[] bbox, Vector2Int resolution)
            => new double[]
            {
                Math.Round((bbox[0] + bbox[2] * 0.5) / resolution.x, 7),
                Math.Round((bbox[1] + bbox[3] * 0.5) / resolution.y, 7),
                Math.Round(bbox[2] / resolution.x, 7),
                Math.Round(bbox[3] / resolution.y, 7),
            };
    }
}
