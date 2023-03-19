using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorViewer.VectorShapes
{
    class ShapeTypeAttribute : Attribute
    {
        public string ShapeType { get; set; }
        public ShapeTypeAttribute(string shapeType)
        {
            ShapeType = shapeType;
        }
    }
}
