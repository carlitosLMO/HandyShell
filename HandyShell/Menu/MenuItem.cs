﻿using Windows.Data.Json;
using Windows.Storage;

namespace HandyShell.Menu
{
    public class MenuItem : BaseModel
    {
        private string _title;
        private string _exe;
        private string _param;
        private string _icon;

        public string Title { get => _title; set => SetProperty(ref _title, value); }
        public string Exe { get => _exe; set => SetProperty(ref _exe, value); }
        public string Param { get => _param; set => SetProperty(ref _param, value); }
        public string Icon { get => _icon; set => SetProperty(ref _icon, value); }
        public StorageFile File { get; set; }

        public static MenuItem ReadFromJson(string content)
        {
            var json = JsonObject.Parse(content);
            return new MenuItem
            {
                Title = json.GetNamedString("title", "no title"),
                Exe = json.GetNamedString("exe", ""),
                Param = json.GetNamedString("param", ""),
                Icon = json.GetNamedString("icon", ""),
            };
        }


        public static string WriteToJson(MenuItem content)
        {
            var json = new JsonObject
            {
                ["title"] = JsonValue.CreateStringValue(content.Title),
                ["exe"] = JsonValue.CreateStringValue(content.Exe ?? string.Empty),
                ["param"] = JsonValue.CreateStringValue(content.Param ?? string.Empty),
                ["icon"] = JsonValue.CreateStringValue(content.Icon ?? string.Empty)
            };
            return json.Stringify();
        }

        public static (bool, string) Check(MenuItem content)
        {
            if (string.IsNullOrEmpty(content.Title))
            {
                return (false, nameof(content.Title));
            }

            if (string.IsNullOrEmpty(content.Exe))
            {
                return (false, nameof(content.Exe));
            }

            if (string.IsNullOrEmpty(content.Param))
            {
                return (false, nameof(content.Param));
            }
            return (true, string.Empty);
        }
    }
}
