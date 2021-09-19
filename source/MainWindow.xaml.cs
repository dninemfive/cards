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

namespace cards
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            TaskManager.LoadTasks();
            foreach (Task t in TaskManager.AllTasks) AddTask(t);
        }
        public void AddTask(Task task)
        {
            //MainTextBlock.Text += task.ToString() + "\n";
            TreeViewItem tvi = new TreeViewItem() { Header = task.Title };
            MainTreeView.Items.Add(tvi);
        }
        private void Button_RandomTask_Click(object sender, RoutedEventArgs e)
        {
            Task newTask = TaskManager.NewTask;
            MainTextBlock.Text = newTask.Title += "\n--------\n\n" + newTask.Content;
        }
    }
}
