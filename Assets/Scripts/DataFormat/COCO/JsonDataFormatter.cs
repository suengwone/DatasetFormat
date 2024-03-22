using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Personal.Study.Data.Format
{
    using Data.Object;
    public class JsonDataFormatter
    {
        public JsonDataFormat jsonData = new JsonDataFormat();
        public string fileExtension;

        public JsonDataFormatter(string fileExtension)
        {
            this.fileExtension = fileExtension;
        }

        public void SetInfo(string description, string contributor, string version, string date_created)
        {
            jsonData.info.description = description;
            jsonData.info.version = version;
            jsonData.info.year = int.Parse(date_created.Trim().Substring(0, 4));
            jsonData.info.contributor = contributor;
            jsonData.info.date_created = date_created;
        }

        public void SetCategories(SortedList<int, (string category, string name)> labelDatas)
        {
            if(jsonData.categories.Count > 0)
                jsonData.categories.Clear();

            foreach(var label in labelDatas)
            {
                jsonData.categories.Add(new CategoryInfo()
                {
                    id = label.Key,
                    name = label.Value.name,
                    supercategory = label.Value.category,
                });
            }
        }

        public void AddImageInfo(int imageCount, string fileName, string dateCaptured, Vector2Int resolution)
            => jsonData.images.Add(new ImageInfo()
                {
                    id = imageCount,
                    width = resolution.x,
                    height = resolution.y,
                    file_name = fileName,
                    date_captured = dateCaptured,
                    extension = this.fileExtension,
                });

        public void AddAnnotationInfo(List<ObjectInfo> objectDatas, int captureCount)
            => objectDatas.ForEach(objectData => 
                jsonData.annotations.Add(new AnnotationData()
                {
                    id = jsonData.annotations.Count,
                    image_id = captureCount,
                    category_id = objectData.LabelId,
                    segmentation = objectData.OutlinePoints,
                    area = objectData.MaxArea,
                    bbox = objectData.BoundingBox.ToArray(),
                    isCrowd = objectData.IsCrowd,
                }));
        
        public string GetDataAsJson()
            => JsonConvert.SerializeObject(jsonData, Formatting.Indented);
        
        public void Release()
            => jsonData.Release();
    }
}