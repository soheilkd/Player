﻿using Player.Models;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;

namespace Player
{
	[Flags]
	public enum MediaType
	{
		None = 0B0,
		File = 0B10,
		Music = File | 0B100,
		Video = File | 0B1000
	}

	[Serializable]
	public class Media : INotifyPropertyChanged
	{
		private string _Name;
		private string _Artist;
		private string _Title;
		private string _Album;
		private string _Dir;
		private int _PlayCount;
		private bool _IsPlaying;
		private TimeSpan _Len;
		public MediaType Type;
		[field: NonSerialized] public BitmapImage _Artwork;
		[field: NonSerialized] public string Lyrics = "";
		[field: NonSerialized] public bool IsLoaded = false;
		[field: NonSerialized] public event PropertyChangedEventHandler PropertyChanged;

		public string Name { get => _Name; set => Set(ref _Name, value); }
		public string Artist { get => _Artist; set => Set(ref _Artist, value); }
		public string Title { get => _Title; set => Set(ref _Title, value); }
		public string Album { get => _Album; set => Set(ref _Album, value); }
		public string Directory { get => _Dir; set => Set(ref _Dir, value); }
		public int PlayCount { get => _PlayCount; set => Set(ref _PlayCount, value); }
		public bool IsPlaying { get => _IsPlaying; set => Set(ref _IsPlaying, value); }
		public TimeSpan Length { get => _Len; set => Set(ref _Len, value); }
		public BitmapImage Artwork { get => _Artwork; set => Set(ref _Artwork, value); }
		public DateTime AdditionDate { get; private set; }
		public string Path { get; set; }
		public bool IsVideo => Type == MediaType.Video;

		public Media() { }
		public Media(string path)
		{
			Path = path;
			AdditionDate = DateTime.Now;
		}

		public override string ToString() => Path;
		
		protected void Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
		{
			field = value;
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public static implicit operator string(Media media) => media.Path;
		public static implicit operator Uri(Media media) => new Uri(media.Path);
		public static implicit operator MediaType(Media media) => media.Type;
	}
}