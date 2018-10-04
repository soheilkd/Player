﻿using System;
using System.Collections.ObjectModel;
using Player.Extensions;

namespace Player.Models
{
	[Serializable]
	public class Playlist : ObservableCollection<Media>
	{
		public SerializableBitmap Thumbnail { get; set; }

		public Playlist(Collection<Media> medias, SerializableBitmap thumbnail = default) : base(medias)
		{
			Thumbnail = thumbnail;
			medias.For(each => Add(each));
		}
	}
}
