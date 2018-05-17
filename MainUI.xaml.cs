﻿using Player.Controls;
using Player.Events;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Linq;
using Forms = System.Windows.Forms;
namespace Player
{
    public partial class MainUI : Window
    {
        private IEnumerable<int> SelectedMediaIndexes
        {
            get =>
                from item
                in QueueListView.SelectedItems.Cast<MediaView>()
                select item.MediaIndex;
        }
        private MediaManager Manager = new MediaManager();
        private List<MediaView> MediaViews = new List<MediaView>();
        private MassiveLibrary Library = new MassiveLibrary();
        private Timer PlayCountTimer = new Timer(100000) { AutoReset = false };
        private Timer SizeChangeTimer = new Timer(50) { AutoReset = true };
        private bool[] IsVisionOn = new bool[] { false, false, false };

        public MainUI()
        {
            InitializeComponent();
            Manager.Change += Manager_Change;
            App.NewInstanceRequested += (_, e) => Manager.Add(e.Args);
            
            var lib = MassiveLibrary.Load();
            for (int i = 0; i < lib.Medias.Length; i++)
                Manager.Add(lib.Medias[i]);
            Width = App.Preferences.LastSize.Width;
            Height = App.Preferences.LastSize.Height;
            Left = App.Preferences.LastLoc.X;
            Top = App.Preferences.LastLoc.Y;
        }
        private void BindUI()
        {
            PlayCountTimer.Elapsed += (_, __) => Manager.AddCount();
            (Resources["SettingsOnBoard"] as Storyboard).CurrentStateInvalidated += (_, __) => SettingsGrid.Visibility = Visibility.Visible;
            (Resources["SettingsOffBoard"] as Storyboard).Completed += (_, __) => SettingsGrid.Visibility = Visibility.Hidden;
            SettingsGrid.Visibility = Visibility.Hidden;
            SizeChangeTimer.Elapsed += delegate
            {
                Dispatcher.Invoke(
                    delegate
                    {
                        if (Player.Magnified)
                            return;
                        for (int i = 0; i < MediaViews.Count; i++)
                            MediaViews[i].Width = QueueListView.ActualWidth > 25 ? QueueListView.ActualWidth - 25 : 25;
                    });
                SizeChangeTimer.Stop();
            };
            User.Keyboard.Events.KeyDown += Keyboard_KeyDown;
            Settings_AncestorCombo.SelectedIndex = App.Preferences.MainKey;
            Settings_OrinateCheck.IsChecked = App.Preferences.VisionOrientation;
            Settings_TimeoutCombo.SelectedIndex = App.Preferences.MouseOverTimeout;
            Settings_AncestorCombo.SelectionChanged += (_, __) => App.Preferences.MainKey = Settings_AncestorCombo.SelectedIndex;
            Settings_TimeoutCombo.SelectionChanged += delegate
            {
                App.Preferences.MouseOverTimeout = Settings_TimeoutCombo.SelectedIndex;
                switch (Settings_TimeoutCombo.SelectedIndex)
                {
                    case 0: App.Preferences.MouseOverTimeout = 500; break;
                    case 1: App.Preferences.MouseOverTimeout = 1000; break;
                    case 2: App.Preferences.MouseOverTimeout = 2000; break;
                    case 3: App.Preferences.MouseOverTimeout = 3000; break;
                    case 4: App.Preferences.MouseOverTimeout = 4000; break;
                    case 5: App.Preferences.MouseOverTimeout = 5000; break;
                    case 6: App.Preferences.MouseOverTimeout = 10000; break;
                    case 7: App.Preferences.MouseOverTimeout = 60000; break;
                    default: App.Preferences.MouseOverTimeout = 5000; break;
                }
            };
            Settings_OrinateCheck.Checked += (_, __) => App.Preferences.VisionOrientation = Settings_OrinateCheck.IsChecked.Value;
            Settings_OrinateCheck.Unchecked += (_, __) => App.Preferences.VisionOrientation = Settings_OrinateCheck.IsChecked.Value;
            SettingsButton.MouseUp += delegate
            {
                if (!IsVisionOn[2])
                {
                    (Resources["SettingsOnBoard"] as Storyboard).Begin();
                }
                else
                {
                    (Resources["SettingsOffBoard"] as Storyboard).Begin();
                }
                IsVisionOn[2] = !IsVisionOn[2];
            };
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BindUI();
            var cml = Environment.GetCommandLineArgs().Where(name => !name.EndsWith(".exe")).ToArray();
            if (cml.Length > 1)
                Manager.Add(cml, true);
            Player.EventHappened += Player_EventHappened;
        }

        private void Player_EventHappened(object sender, InfoExchangeArgs e)
        {
            switch (e.Type)
            {
                case InfoType.DragMoveRequest: DragMove(); break;
                case InfoType.NextRequest: Play(Manager.Next()); break;
                case InfoType.PrevRequest: Play(Manager.Previous()); break;
                case InfoType.Handling:
                    break;
                case InfoType.UserInterface:
                    break;
                case InfoType.LengthFound:
                    Manager.CurrentlyPlaying.Length = (TimeSpan)e.Object;
                    //Revoke Currently Playing Media Views with the new Length
                    MediaViews.FindAll(item => item.MediaIndex == Manager.CurrentlyPlayingIndex).ForEach(eachView => eachView.Revoke(Manager.CurrentlyPlaying));
                    break;
                case InfoType.PlayModeChange: Manager.ActivePlayMode = (PlayMode)e.Object; break;
                default: break;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Preferences.LastSize = new Size(Width, Height);
            App.Preferences.LastLoc = new Point(Left, Top);
            App.Preferences.Volume = Player.Volume;
            App.Preferences.Save();
            Manager.Close();
            Application.Current.Shutdown();
        }
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Space:
                    Player.PlayPause();
                    break;
                default: break;
            }
        }
        private void Window_Drop(object sender, DragEventArgs e) => Manager.Add((string[])e.Data.GetData(DataFormats.FileDrop));
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            SizeChangeTimer.Start();
            Player.Size_Changed(this, null);
        }

        private void Media_DeleteRequested(object sender, InfoExchangeArgs e)
        {
            if (QueueListView.SelectedItems.Count > 1)
            {
                string selectedFilesInString = "";
                OnSelectedMedias(each => selectedFilesInString += $"\r\n{Manager[each].Path}");
                var res = MessageBox.Show($"Sure? this files will be deleted:{selectedFilesInString}", " ", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (res == MessageBoxResult.OK)
                    OnSelectedMedias(each => Manager.Remove(each));
            }
            else
                Manager.RequestDelete(e.Integer);
        }
        private void Media_LocationRequested(object sender, InfoExchangeArgs e)
        {
            OnSelectedMedias(each => Manager.RequestLocation(each));
        }
        private void Media_RemoveRequested(object sender, InfoExchangeArgs e)
        {
            OnSelectedMedias(each => Manager.Remove(each));
        }
        private void Media_PropertiesRequested(object sender, InfoExchangeArgs e)
        {
            OnSelectedMedias(each => PropertiesUI.OpenFor(Manager[each], (_, f) => Manager.Update(f.Object as TagLib.File)));
        }
        private void Media_RepeatRequested(object sender, InfoExchangeArgs e)
        {
            OnSelectedMedias(each => Manager.Repeat(each, (int)e.Object));
        }
        private void Media_DownloadRequested(object sender, InfoExchangeArgs e)
        {
            (sender as MediaView).Download(Manager[e.Integer]);
        }
        private void Media_DownloadCompleted(object sender, InfoExchangeArgs e)
        {
            Manager[e.Integer] = e.Object as Media;
            if (Manager.CurrentlyPlayingIndex == e.Integer)
            {
                var posit = Player.Position;
                Play(Manager[e.Integer]);
                Player.Seek(posit);
            }
        }
        private void Media_ZipDownloaded(object sender, InfoExchangeArgs e)
        {
            Manager.Remove((sender as MediaView).MediaIndex);
            Manager.Add((string[])e.Object);
        }
       
        private async void Manager_Change(object sender, InfoExchangeArgs e)
        {
            switch (e.Type)
            {
                case InfoType.NewMedia:
                    MediaViews.Add(new MediaView(e.Integer, Manager[e.Integer]));
                    var p = MediaViews.Count - 1;
                    QueueListView.Items.Add(MediaViews[p]);
                    MediaViews[p].DoubleClicked += (n, f) => Play(Manager.Next(f.Integer));
                    MediaViews[p].PlayClicked += (n, f) => Play(Manager.Next(f.Integer));
                    MediaViews[p].DeleteRequested += Media_DeleteRequested;
                    MediaViews[p].LocationRequested += Media_LocationRequested;
                    MediaViews[p].RemoveRequested += Media_RemoveRequested;
                    MediaViews[p].PropertiesRequested += Media_PropertiesRequested;
                    MediaViews[p].RepeatRequested += Media_RepeatRequested;
                    MediaViews[p].DownloadRequested += Media_DownloadRequested;
                    MediaViews[p].Downloaded += Media_DownloadCompleted;
                    MediaViews[p].ZipDownloaded += Media_ZipDownloaded;
                    Height--; Height++;
                    break;
                case InfoType.EditingTag:
                    var pos = Player.Position;
                    Player.FullStop();
                    await Task.Delay(500);
                    (e.Object as TagLib.File).Save();
                    Play(Manager[Manager.Find((e.Object as TagLib.File).Name)]);
                    Player.Position = pos;
                    break;
                case InfoType.MediaRemoved:
                    MediaViews.RemoveAll(item => item.MediaIndex == e.Integer);
                    QueueListView.Items.Clear();
                    for (int i = 0; i < MediaViews.Count; i++)
                        QueueListView.Items.Add(MediaViews[i]);
                    for (int i = 0; i < MediaViews.Count; i++)
                        if (MediaViews[i].MediaIndex >= e.Integer)
                            MediaViews[i].MediaIndex--;
                    break;
                case InfoType.MediaRequested:
                    Play(Manager.Next(e.Integer));
                    break;
                case InfoType.MediaUpdate:
                    MediaViews.FindAll(item => item.MediaIndex == e.Integer).ForEach(eachView => eachView.Revoke(e));
                    if (e.Integer == Manager.CurrentlyPlayingIndex)
                    {
                        var q = Player.Position;
                        Play(e.Object as Media);
                        Player.Seek(q);
                    }
                    break;
                default:
                    break;
            }
        }
        private async void Keyboard_KeyDown(object sender, Forms::KeyEventArgs e)
        {
            if (IsActive || IsAncestorKeyDown(e))
            {
                switch (e.KeyCode)
                {
                    case Forms::Keys.Left:
                        Player.SmallSlideLeft();
                        await Task.Delay(200);
                        break;
                    case Forms::Keys.Right:
                        Player.SmallSlideRight();
                        await Task.Delay(200);
                        break;
                    case Forms::Keys.A:
                        var cb = Clipboard.GetText() ?? String.Empty;
                        if (Uri.TryCreate(cb, UriKind.Absolute, out var uri))
                            Manager.Add(uri);
                        else
                            return;
                        QueueListView.ScrollIntoView(QueueListView.Items[QueueListView.Items.Count - 1]);
                        break;
                    default: break;
                }
            }
            switch (e.KeyCode)
            {
                case Forms::Keys.MediaNextTrack: Player.PlayNext(); break;
                case Forms::Keys.MediaPreviousTrack: Player.PlayPrevious(); break;
                case Forms::Keys.MediaPlayPause: Player.PlayPause(); break; 
                default: break;
            }
        }

        private void AnySettingChanged(object sender, RoutedEventArgs e)
        {
            if (!IsLoaded)
                return;
            App.Preferences.Save();
        }

        private void Play(Media media)
        {
            if (!media.IsValid)
            {
                Manager.Remove(media);
                return;
            }
            Player.Play(media);
            MiniArtworkImage.Source = media.Artwork;
            for (int i = 0; i < MediaViews.Count; i++)
                MediaViews[i].IsPlaying = false;
            MediaViews.Find(item => item.MediaIndex == Manager.CurrentlyPlayingIndex).IsPlaying = true;
        }
        private bool IsAncestorKeyDown(Forms::KeyEventArgs e)
        {
            switch (App.Preferences.MainKey)
            {
                case 0: return e.Control;
                case 1: return e.Alt;
                case 2: return e.Shift;
                case 3: return e.Control && e.Alt;
                case 4: return e.Control && e.Shift;
                case 5: return e.Shift && e.Alt;
                case 6: return e.Control && e.Shift && e.Alt;
                default: return false;
            }
        }

        //private void Invoke<T>(Action<T> action, IEnumerable<T> on = null) => on.AsParallel().ForAll(action);
        private void OnSelectedMedias(Action<int> action) => SelectedMediaIndexes.AsParallel().ForAll(action);
        private void OnSelectedMediaViews(Action<MediaView> action) => QueueListView.SelectedItems.Cast<MediaView>().AsParallel().ForAll(action);
    }
}
