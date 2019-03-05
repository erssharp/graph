using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Graph
{
    public class Vertex
    {
        public int Name { get; set; } //Порядок вершины по номеру (ее индекс)
        public Ellipse Ellipse { get; set; } //Эллипс, представляющий собой вершину в GUI
        public List<Vertex> Next = new List<Vertex>(); //Список вершин, в которые можно попасть из этой вершины 
    }
}
