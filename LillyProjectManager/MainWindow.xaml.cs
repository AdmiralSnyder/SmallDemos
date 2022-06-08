using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
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

namespace LillyProjectManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //DataContext = this;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTool is null || SelectedProject is null) return;
            
            StartTool(SelectedTool, SelectedProject);
        }

        private void StartTool(Tool tool, Project project)
        {
            switch (tool.CommandType)
            {
                case CommandTypes.Executable:
                    {
                        var path = Environment.ExpandEnvironmentVariables(tool.Command);
                        var process = Process.Start(path);

                        break;
                    }
            }
            LogText += $@"{Environment.NewLine}[{DateTime.Now:g}] opening {tool.Name} for project {project.Name}";
        }

        public Tool SelectedTool { get; set; }

        public Project SelectedProject { get; set; }

        public static readonly DependencyProperty LogTextProperty =
        DependencyProperty.Register(nameof(LogText), typeof(string), typeof(MainWindow), new UIPropertyMetadata("Hello Log"));

        public string LogText
        {
            get => (string)GetValue(LogTextProperty);
            set => SetValue(LogTextProperty, value);
        }
    }

    // CHEATING for now!
    public class ToolList : List<Tool>{ }
    public class ProjectList : List<Project> { }

    public class Project
    {
        public string? Name { get; set; }
    }

    public class Tool
    {
        public string? Name { get; set; }
        public string? Command { get; set; }
        public CommandTypes CommandType { get; set; }
        public string? Icon { get; set; }
        private bool IconResolved = false;
        public string? IconPath
        {
            get
            {
                if (!IconResolved)
                {
                    IconResolved = true;
                    if (Icon is { })
                    {
                        if (!File.Exists(Icon))
                        {
                            var localRelativeFolder = System.IO.Path.Combine(Environment.CurrentDirectory, Icon);
                            if (File.Exists(localRelativeFolder))
                            {
                                Icon = localRelativeFolder;
                            }
                        }
                        else
                        {
                            Icon = System.IO.Path.GetFullPath(Icon);
                        }
                    }
                }

                return Icon;
            }
        }
    }

    public enum CommandTypes
    {
        Executable,
        WindowsStoreApp,
        Website,
    }
}
