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
            SetActiveTask(TaskManager.NewTask);
        }
        public void AddTask(Task task, ItemsControl control = null)
        {
            if (control == null) control = MainTreeView;
            TreeViewItem tvi = new TreeViewItem() { Header = task.Title };
            control.Items.Add(tvi);
            foreach(Task t in task.Subtasks)
            {
                AddTask(t, tvi);
            }
        }
        private void Button_RandomTask_Click(object sender, RoutedEventArgs e)
        {
            SetActiveTask(TaskManager.NewTask);
        }
        public void SetActiveTask(Task t)
        {
            if(t == null) return;
            (string title, string content) deets = t.Details();
            MainTitle.Text = deets.title;
            MainContent.Text = deets.content;
        }
    }
}
