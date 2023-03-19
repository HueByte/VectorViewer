using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;
using VectorViewer.VectorShapes;

namespace VectorViewer
{
    class InputManager
    {
        private string _input = "";
        private string _fileExtension = "";
        public InputManager() 
        {
            OpenFileDialog ofd = new()
            {
                Filter = "Input Files|*.json;*.xml"
            };

            if (ofd.ShowDialog() == true)
            {
                _fileExtension = Path.GetExtension(ofd.FileName);
                _input = File.ReadAllText(ofd.FileName);
            }
        }

        public IEnumerable<ViewerShape> ParseInput()
        {
            switch(_fileExtension)
            {
                case ".json":
                    return JsonSerializer.Deserialize<ViewerShape[]>(_input) ?? Enumerable.Empty<ViewerShape>();
                case ".xml":
                    //return XmlSerializer...;
                    return Enumerable.Empty<ViewerShape>();
                default:
                    throw new Exception("Invalid file extension");
            }
        }
    }
}
