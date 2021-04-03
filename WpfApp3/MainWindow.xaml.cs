using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.IO;
using System.Windows.Markup;
using System.Windows.Annotations;
using System.Windows.Annotations.Storage;

namespace WpfApp3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       string path = "mydoc.xaml";
        FileStream fs;
        AnnotationService anService;
        public MainWindow()
        {

            InitializeComponent();
            //     string s = ((Run)p1.Inlines.FirstInline).Text;
            this.Loaded += Window_Loaded;
            this.Unloaded += Window_Unloaded;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            using (FileStream fs = File.Open(path, FileMode.Create))
            {
                if (docViewer.Document != null)
                {
                    XamlWriter.Save(docViewer.Document, fs);
                    MessageBox.Show("Файл сохранен");
                }
            }   
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            docViewer.ClearValue(FlowDocumentScrollViewer.DocumentProperty);
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            using (FileStream fs = File.Open(path, FileMode.Open))
            {
                FlowDocument document = XamlReader.Load(fs) as FlowDocument;
                if (document != null)
                    docViewer.Document = document;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            anService = new AnnotationService(docViewer);
            fs = new FileStream("storage.xml", FileMode.OpenOrCreate);
            AnnotationStore store = new XmlStreamStore(fs);
            store.AutoFlush = true;
            anService.Enable(store);

        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            anService = new AnnotationService(docViewer);
            fs = new FileStream("storage.xml", FileMode.OpenOrCreate);
            AnnotationStore store = new XmlStreamStore(fs);
            store.AutoFlush = true;
            anService.Enable(store);

        }
    }
}
