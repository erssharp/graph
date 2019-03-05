using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Graph
{
    /* 
     * 27 Вариант
     * Постановка задачи: дана плоская страна и в ней n городов. Нужно соединить соединить все города
     * телефонной связью так, чтобы общая длина телефонных линий была минимальной.
     * Разработать алгоритм решения данной задачи.
     * 
     * Измененный вид задачи: дан взвешенный неориентированный граф. Найти минимальное остовное дерево.
     * 
     * Для решения данной задачи будем использовать алгоритм Прима:
     * На вход алгоритма подается связный взвешенный неориентированный граф.
     * Шаг 1: Берется случайная вершина и ребро минимального веса, инцидентное данной вершине.
     * Шаг 2: Берется ребро минимального веса, соединяющее одну из вершин в дереве и вершину, не принадлежащую дереву.
     * Повторяется до тех пор, пока все вершины не войдут в дерево.
     * Шаг 3: Удаляем все ребра, не принадлежащие дереву.
     */
    class OstovTree
    {
        public void Prim(CGraph graph, Grid grid) 
        {
            List<Vertex> lv = graph.Vertices;
            List<Edge> le = graph.Edges;
            List<Vertex> tree = new List<Vertex>(); //Список, представляющий собой остовное дерево
            List<Edge> tredges = new List<Edge>(); //Список с ребрами остовного дерева
            Vertex vf = lv.First(); //Первая вершина (Шаг 1)
            tree.Add(vf);

            while (tree.Count != lv.Count) //Пока все вершины не вошли в дерево, повторяем действия (Шаг 2)
            {
                List<Edge> incedges = new List<Edge>(); //Список ребер, инцидентных вершинам остовного дерева, идущих к вершинам, не принадлежащим дереву

                int min = int.MaxValue;
                Edge emin = null; //Ребро минимального веса

                foreach (Vertex v in tree) //Наполнения списка incedges
                    foreach (Edge e in le)
                        if (e.Start == v || e.End == v && ((tree.Contains(e.Start) && !tree.Contains(e.End)) || (tree.Contains(e.End) && !tree.Contains(e.Start))))
                            incedges.Add(e);

                foreach (Edge e in incedges) //Нахождение минимального ребра в списке
                    if (e.Weight < min && ((tree.Contains(e.Start) && !tree.Contains(e.End)) || (tree.Contains(e.End) && !tree.Contains(e.Start))))
                    {
                        emin = e;
                        min = e.Weight;
                    }

                tredges.Add(emin); //Добавление ребра к списку ребер оствоного дерева

                if (!tree.Contains(emin.End)) //Добавление вершины в дерево
                    tree.Add(emin.End);
                else
                    tree.Add(emin.Start);
            }

            foreach (Edge e in le)//Удаление ребер, не принадлежащих остовному дереву
            {
                if (!tredges.Contains(e))
                {
                    grid.Children.Remove(e.TB);
                    grid.Children.Remove(e.Line);
                }
            }
        }

        public async Task AnimatedPrim(CGraph graph, Grid grid) //Анимированная версия алгоритма
        {
            List<Vertex> lv = graph.Vertices;
            List<Edge> le = graph.Edges;
            List<Vertex> tree = new List<Vertex>();
            List<Edge> tredges = new List<Edge>();
            Vertex vf = lv.First();
            tree.Add(vf);

            while (tree.Count != lv.Count)
            {
                List<Edge> incedges = new List<Edge>();

                int min = int.MaxValue;
                Edge emin = null;

                foreach (Vertex v in tree)
                    foreach (Edge e in le)
                        if (e.Start == v || e.End == v && ((tree.Contains(e.Start) && !tree.Contains(e.End)) || (tree.Contains(e.End) && !tree.Contains(e.Start))))
                            incedges.Add(e);

                foreach (Edge e in incedges)
                {
                    e.Line.Stroke = Brushes.Red;
                    if (e.Weight < min && ((tree.Contains(e.Start) && !tree.Contains(e.End)) || (tree.Contains(e.End) && !tree.Contains(e.Start))))
                    {
                        emin = e;
                        min = e.Weight;
                    }
                    await Task.Delay(400);
                    e.Line.Stroke = Brushes.Black;
                }
                tredges.Add(emin);
                await Blink(emin.Line);
                if (!tree.Contains(emin.End))
                    tree.Add(emin.End);
                else
                    tree.Add(emin.Start);
            }

            foreach (Edge e in le) 
            {
                await Task.Delay(200);
                if (!tredges.Contains(e))
                {
                    grid.Children.Remove(e.TB);
                    grid.Children.Remove(e.Line);
                }
            }
        }

        private async Task Blink(Line l) //Моргание линии при нахождении минимального ребра
        {
            for (int i = 0; i < 10; i++)
            {
                if (l.Stroke == Brushes.Black)
                    l.Stroke = Brushes.Red;
                else
                    l.Stroke = Brushes.Black;
                await Task.Delay(100);
            }
        }
    }
}
