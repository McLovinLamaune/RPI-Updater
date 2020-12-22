using Newtonsoft.Json;
using RPI_Updater.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RPI_Updater.Managers
{
    public class JsonFileManager<T>
    {
        public T Content { get; set; }

        private StreamReader _reader;
        private string _path;
        public JsonFileManager(string path)
        {
            _path = path;            
        }

        public string Initialize()
        {
            try
            {
                _reader = new StreamReader(_path);
                string doc = _reader.ReadToEnd();
                _reader.Close();
                if(doc.Length < 10)
                {
                    return "Bad file format";
                }
                Content = JsonConvert.DeserializeObject<T>(doc);
                return null;
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
