using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Reflection;
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
using window_explorer.Class;

namespace window_explorer
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly object dummyNode = null;
        public MainWindow()
        {
            InitializeComponent();
            //窗口初始化
            this.Loaded += Window_Load;
        }
        private void Window_Load(object sender, EventArgs e)
        {
            //在Title显示版本信息
            string versionfile = "";
            object[] attributes = System.Reflection.Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false);
            if (attributes.Length != 0)
            {
                versionfile = ((AssemblyFileVersionAttribute)attributes[0]).Version;
            }
            this.Title = "window_explorer v" + versionfile;
            
            //初始化文件目录
            System.IO.DriveInfo[] drives = System.IO.Directory.GetLogicalDrives().Select(q => new System.IO.DriveInfo(q)).ToArray();
            foreach (var driveInfo in drives)
            {
                if (!driveInfo.IsReady) continue;
                string s = driveInfo.Name;
                TreeViewItem item = new TreeViewItem();
                item.Header = driveInfo.VolumeLabel + " (" + s.Substring(0, s.IndexOf(@":\")) + ":)";
                item.Tag = s;
                item.FontWeight = FontWeights.Normal;
                item.Items.Add(dummyNode);
                item.Expanded += new RoutedEventHandler(folder_Expanded);
                tree2.Items.Add(item);
            }
        }
        void folder_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)sender;
            if (item.Items.Count == 1 && item.Items[0] == dummyNode)
            {
                item.Items.Clear();
                try
                {
                    DirectoryInfo[] dis = Directory.GetDirectories(item.Tag.ToString()).Select(q => new System.IO.DirectoryInfo(q)).ToArray();
                    foreach (DirectoryInfo di in dis)
                    {
                        if ((di.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden) continue;
                        TreeViewItem subitem = new TreeViewItem();
                        string s = di.FullName;
                        subitem.Header = s.Substring(s.LastIndexOf("\\") + 1);
                        subitem.Tag = s;
                        subitem.FontWeight = FontWeights.Normal;
                        subitem.Items.Add(dummyNode);
                        subitem.Expanded += new RoutedEventHandler(folder_Expanded);
                        item.Items.Add(subitem);
                    }
                }
                catch (Exception ex)
                {
                    Trace.Write(ex.ToString());
                }
            }
        }
        private void Tree2_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeViewItem tvi = tree2.SelectedValue as TreeViewItem;
            string root = tvi.Tag.ToString();
            ObservableCollection<sFileInfo>  filelist = new ObservableCollection<sFileInfo>();
            FileInfo[] files = Directory.GetFiles(root).Select(q => new System.IO.FileInfo(q)).ToArray();
            foreach (FileInfo f in files)
            {
                filelist.Add(new sFileInfo()
                {
                    Name = f.Name,
                    Size = f.Length/1000,
                    Path = f.FullName,
                });
            }
            this.fileList.ItemsSource = null;
            this.fileList.ItemsSource = filelist;
        }
        /// <summary>
        /// 获取文本字符长度
        /// </summary>
        /// <param name="text"></param>
        /// <param name="fontSize"></param>
        /// <param name="fontFamily"></param>
        /// <returns></returns>
        private double MeasureTextWidth(string text, double fontSize, string fontFamily)
        {
            FormattedText formattedText = new FormattedText(
            text,
            System.Globalization.CultureInfo.InvariantCulture,
            FlowDirection.LeftToRight,
            new Typeface(fontFamily.ToString()),
            fontSize,
            Brushes.Black
            );
            return formattedText.WidthIncludingTrailingWhitespace;
        }
        /// <summary>
        /// 获取已安装字体
        /// </summary>
        private void getFonts()
        {
            System.Drawing.FontFamily[] fontFamilies;
            InstalledFontCollection installedFontCollection = new InstalledFontCollection();
            fontFamilies = installedFontCollection.Families;
            int count = fontFamilies.Length;
            Trace.Write("fontFamilies.Length=" + fontFamilies.Length);
            for (int i = 0; i < count; i++)
            {
                string fontName = fontFamilies[i].Name;
                Trace.Write("fontName: " + fontName);
            }
        }
    }
}
