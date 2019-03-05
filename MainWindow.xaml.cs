using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Graph
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        const double elw = 60; //Ширина эллипса
        const double elh = 60; //Высота эллипса
        int N = 0; //Начальный номер для вершин
        char NM = 'A'; //Начальное имя для вершин

        List<Vertex> list = new List<Vertex>(); //Список вершин
        List<Ellipse> ellist = new List<Ellipse>(); //Список эллипсов
        List<Edge> edges = new List<Edge>(); //Список ребер
        CGraph graph = new UndirectedGraph(); //Создание графа

        public MainWindow()
        {
            InitializeComponent();
            graph.Edges = edges;
            graph.Vertices = list;
            WindowState = WindowState.Maximized;
        }
           
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) //Создание новой вершины, либо построение ребра
        {
            Point mp = Mouse.GetPosition(this);

            foreach (Ellipse el in ellist) //В случае, если указатель находится над эллипсом - строится ребро
            {
                if (mp.X >= el.Margin.Left && mp.X <= el.Margin.Left + elw && mp.Y >= el.Margin.Top && mp.Y <= el.Margin.Top + elh)
                {
                    Line line = new Line();
                    line.HorizontalAlignment = HorizontalAlignment.Left;
                    line.VerticalAlignment = VerticalAlignment.Top;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 1.5;
                    grid.Children.Add(line);
                    UpdateLine(line, el); 
                    return;
                }
            }
            //в противном случае - строится вершина
            Ellipse ellipse = new Ellipse(); //Эллипс, представляющий вершину
            ellipse.Width = elw;
            ellipse.Height = elh;
            ellipse.Stroke = Brushes.Black;
            ellipse.Fill = Brushes.White;
            ellipse.HorizontalAlignment = HorizontalAlignment.Left;
            ellipse.VerticalAlignment = VerticalAlignment.Top;
            ellipse.StrokeThickness = 1.5;
            ellipse.Margin = new Thickness(mp.X - (elw / 2), mp.Y - (elh / 2), 0, 0);

            TextBlock tb = new TextBlock(); //TextBlock, в котором находится название вершины
            tb.Text += NM++;
            N++;
            tb.FontSize = 16;
            tb.TextAlignment = TextAlignment.Center;
            tb.Width = elw / 2;
            tb.Height = elh / 2;
            tb.HorizontalAlignment = HorizontalAlignment.Left;
            tb.VerticalAlignment = VerticalAlignment.Top;
            tb.Margin = new Thickness(mp.X - elw / 4, mp.Y - elh / 4 + 4, 0, 0);

            Vertex vertex = new Vertex() //Программное представление вершины
            {
                Ellipse = ellipse,
                Name = N - 1
            };
            list.Add(vertex);

            List<Vertex> l = new List<Vertex>();
            graph.IncidentList.Add(l);
            ellist.Add(ellipse);
            grid.Children.Add(ellipse);
            grid.Children.Add(tb);
        }

        private async Task WaitWhileActive(Window w)
        {
            while (w.IsActive)
                await Task.Delay(5);         
        }

        private async void UpdateLine(Line line, Ellipse el)
        {
            Point mp = Mouse.GetPosition(this);
            line.X1 = mp.X;
            line.Y1 = mp.Y;
            line.X2 = mp.X;
            line.Y2 = mp.Y;

            while (Mouse.LeftButton == MouseButtonState.Pressed) //Пока нажата ЛКМ, конец линии будет иметь координаты курсора мыши
            {
                await Task.Delay(5);
                Point p = Mouse.GetPosition(this);
                line.X2 = p.X;
                line.Y2 = p.Y;

                if (Mouse.LeftButton == MouseButtonState.Released) //Когда ЛКМ отпущена и курсор находится над эллипсом, создается ребро. В противном случае линия будет удалена.
                {
                    foreach (Ellipse elem in ellist)
                    {
                        if (p.X >= elem.Margin.Left && p.X <= elem.Margin.Left + elw && p.Y >= elem.Margin.Top && p.Y <= elem.Margin.Top + elh)
                        {
                            if (elem == el)
                                break;

                            try
                            {
                                Weight wg = new Weight();
                                wg.Show();
                                await WaitWhileActive(wg);
                                string txt = wg.wght.Text;                            
                                Edge edge = new Edge(list[ellist.IndexOf(el)], list[ellist.IndexOf(elem)], int.Parse(txt), line);
                                TextBlock tb = new TextBlock();
                                tb.Text = txt;
                                tb.FontSize = 16;
                                tb.TextAlignment = TextAlignment.Center;
                                tb.Width = 40;
                                tb.Height = 20;
                                tb.HorizontalAlignment = HorizontalAlignment.Left;
                                tb.VerticalAlignment = VerticalAlignment.Top;
                                tb.Margin = new Thickness(line.X2 - (line.X2 - line.X1) / 2, line.Y2 - (line.Y2 - line.Y1) / 2 - 10, 0, 0);
                                edge.TB = tb;
                                grid.Children.Add(tb);
                                graph.Edges.Add(edge);
                                edges.Add(edge);
                            }
                            catch
                            {
                                grid.Children.Remove(line);
                                return;
                            }

                            graph.Add(list[ellist.IndexOf(el)], list[ellist.IndexOf(elem)]);
                            return;
                        }
                    }

                    grid.Children.Remove(line);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OstovTree ot = new OstovTree();
            ot.Prim(graph, grid);
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OstovTree ot = new OstovTree();
            await ot.AnimatedPrim(graph, grid);
        }
    }

}
