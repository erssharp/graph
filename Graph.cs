using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Graph
{
    public abstract class CGraph //Представление графа программно
    {
        public List<Edge> Edges = new List<Edge>(); //Список ребер
        public List<Vertex> Vertices = new List<Vertex>(); //Список вершин
        public List<List<Vertex>> IncidentList = new List<List<Vertex>>(); //Список инцидентности (не используется)
        public abstract void Add(Vertex v1, Vertex v2); //Соединение вершин графа
    }
}
