using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Graph
{
    class UndirectedGraph : CGraph
    {
        public override void Add(Vertex v1, Vertex v2) // Соединение вершин графа
        {
            if (!v1.Next.Contains(v2))
            {
                v1.Next.Add(v2);
                v2.Next.Add(v1);
                IncidentList[v1.Name].Add(v2);
                IncidentList[v2.Name].Add(v1);
            }
        }
    }
}
