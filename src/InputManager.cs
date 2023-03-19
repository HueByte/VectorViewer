using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;
using VectorViewer.Constants;
using VectorViewer.VectorShapes;

namespace VectorViewer
{
    class InputManager
    {
        private string _input = "";
        private string _fileExtension = "";
        public InputManager() 
        {
            var test = string.Join(';', SupportedExtensions.ALL.Select(ext => ext.Insert(0, "*")));

            OpenFileDialog ofd = new()
            {
                Filter = $"Input Files|{string.Join(';', SupportedExtensions.ALL.Select(ext => ext.Insert(0, "*")))}"
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
                case SupportedExtensions.JSON:
                    return JsonSerializer.Deserialize<ViewerShape[]>(_input) ?? Enumerable.Empty<ViewerShape>();
                //case SupportedExtensions.XML
                    //return XmlSerializer...;
                default:
                    throw new Exception("Invalid file extension");
            }
        }
    }
}
