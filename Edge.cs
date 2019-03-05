using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Graph
{
    public class Edge
    {
        public Edge(Vertex v1, Vertex v2, int Weight, Line ln) //Конструктор для создания ребра
        {
            Start = v1;
            End = v2;
            this.Weight = Weight;
            Line = ln;
        }
        public Line Line { get; private set; } //Линия, представляющая ребро
        public Vertex Start { get; private set; } //Вершина, откуда идет ребро
        public Vertex End { get; private set; } //Вершина, куда идет ребро
        public TextBlock TB { get; set; } //TextBlock, в котором отображается вес ребра
        public int Weight { get; private set; } // Вес ребра
    }
}
