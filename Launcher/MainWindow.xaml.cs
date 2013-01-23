using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Xml;

namespace Launcher {

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private List<GameInfo> _onlineGamesList;
        private List<GameInfo> _offlineGamesList;
        private List<GameInfo> _showList;

        public MainWindow() {
            InitializeComponent();

            // check if dir excists if not create
            if (Directory.Exists(Directory.GetCurrentDirectory() + "\\Games\\") == false)
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\Games\\");

            // set the location
            Location.Text = Directory.GetCurrentDirectory() + "\\Games\\";

            _onlineGamesList = new List<GameInfo>();
            _offlineGamesList = new List<GameInfo>();
            _showList = new List<GameInfo>();

            checkGames("http://bubblegum.dyndns-ip.com:8080/games/versions.xml");

            fillList();
        }

        private void checkGames(string loc) {

            // get online list

            if (checkForFile(loc)) {
                XmlTextReader xmlReader = new XmlTextReader(loc);

                while (xmlReader.Read()) {
                    GameInfo temp = new GameInfo();
                    if (xmlReader.MoveToContent() == XmlNodeType.Element && xmlReader.Name == "foldername")
                        temp.foldername = xmlReader.ReadElementString();

                    if (xmlReader.MoveToContent() == XmlNodeType.Element && xmlReader.Name == "exeName")
                        temp.exeName = xmlReader.ReadElementString();

                    if (xmlReader.MoveToContent() == XmlNodeType.Element && xmlReader.Name == "title")
                        temp.Title = xmlReader.ReadElementString();

                    if (xmlReader.MoveToContent() == XmlNodeType.Element && xmlReader.Name == "version")
                        temp.Version = xmlReader.ReadElementString();

                    if (xmlReader.MoveToContent() == XmlNodeType.Element && xmlReader.Name == "download_loc")
                        temp.DllLoc = xmlReader.ReadElementString();

                    if (xmlReader.MoveToContent() == XmlNodeType.Element && xmlReader.Name == "description")
                        temp.description = xmlReader.ReadElementString();

                    if (!String.IsNullOrEmpty(temp.Title))
                        _onlineGamesList.Add(temp);
                }
            }

            // check for offline stuff

            string[] offline = Directory.GetDirectories(Location.Text);
            if (offline.Length > 0) {
                foreach (string folder in offline) {
                    if (File.Exists(folder + "\\version.xml")) {
                        XmlTextReader xmlReaderoff = new XmlTextReader(folder + "\\version.xml");
                        while (xmlReaderoff.Read()) {
                            GameInfo temp = new GameInfo();
                            if (xmlReaderoff.MoveToContent() == XmlNodeType.Element && xmlReaderoff.Name == "foldername")
                                temp.foldername = xmlReaderoff.ReadElementString();

                            if (xmlReaderoff.MoveToContent() == XmlNodeType.Element && xmlReaderoff.Name == "exeName")
                                temp.exeName = xmlReaderoff.ReadElementString();

                            if (xmlReaderoff.MoveToContent() == XmlNodeType.Element && xmlReaderoff.Name == "title")
                                temp.Title = xmlReaderoff.ReadElementString();

                            if (xmlReaderoff.MoveToContent() == XmlNodeType.Element && xmlReaderoff.Name == "version")
                                temp.Version = xmlReaderoff.ReadElementString();

                            if (xmlReaderoff.MoveToContent() == XmlNodeType.Element && xmlReaderoff.Name == "download_loc")
                                temp.DllLoc = xmlReaderoff.ReadElementString();

                            if (xmlReaderoff.MoveToContent() == XmlNodeType.Element && xmlReaderoff.Name == "description")
                                temp.description = xmlReaderoff.ReadElementString();

                            if (!String.IsNullOrEmpty(temp.Title))
                                _offlineGamesList.Add(temp);
                        }
                    }
                }
            }
        }

        private void fillList() {
            if (_onlineGamesList.Count > 0) {
                foreach (GameInfo game in _onlineGamesList) {
                    _showList.Add(game);
                }
            }
            if (_offlineGamesList.Count > 0) {
                foreach (GameInfo game in _offlineGamesList)
                    if (_onlineGamesList.Any(item => item.Title == game.Title) == false)
                        _showList.Add(game);
            }

            foreach (GameInfo game in _showList)
                gameList.Items.Add(game.Title);
        }

        private void Changed(object sender, SelectionChangedEventArgs e) {
            GameInfo selectedGame = _showList[gameList.SelectedIndex];
            Description.Content = selectedGame.description;
            DownloadOrPlay.Visibility = Visibility.Visible;
            DownloadOrPlay.Content = checkVersion(selectedGame) ? "Play" : "Download";
        }

        private bool checkVersion(GameInfo game) {
            if (Directory.Exists(Location.Text + game.foldername)) {

                // Console.WriteLine(Location.Text + game.foldername + "\\version.xml");

                if (File.Exists(Location.Text + game.foldername + "\\version.xml")) {
                    XmlTextReader xmlReader = new XmlTextReader(Location.Text + game.foldername + "\\version.xml");

                    while (xmlReader.Read())
                        if (xmlReader.MoveToContent() == XmlNodeType.Element && xmlReader.Name == "version")
                            return xmlReader.ReadElementString() == game.Version;
                }
            }
            return false;
        }

        private void DownloadOrPlay_Click(object sender, RoutedEventArgs e) {
            if (DownloadOrPlay.Content.ToString() == "Play") {
                System.Diagnostics.Process.Start(Location.Text + _showList[gameList.SelectedIndex].foldername + "\\" + _showList[gameList.SelectedIndex].exeName);
            } else {
                MessageBox.Show("Download not implemented yet");
            }
        }

        private bool checkForFile(string url) {
            WebRequest webRequest = WebRequest.Create(url);
            WebResponse webResponse;
            try {
                webResponse = webRequest.GetResponse();
            } catch //If exception thrown then couldn't get response from address
            {
                return false;
            }
            return true;
        }
    }

    internal struct GameInfo {
        public string foldername;
        public string exeName;
        public string Title;
        public string Version;
        public string DllLoc;
        public string description;
    }
}