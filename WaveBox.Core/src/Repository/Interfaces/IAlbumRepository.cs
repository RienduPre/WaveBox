using System;
using WaveBox.Model;
using System.Collections.Generic;

namespace WaveBox.Model.Repository
{
	public interface IAlbumRepository
	{
		bool InsertAlbum(string albumName, int? artistId, int? releaseYear);
		Album AlbumForName(string albumName, int? artistId, int? releaseYear = null);
		List<Album> AllAlbums();
		int CountAlbums();
		List<Album> SearchAlbums(string field, string query, bool exact = true);
		List<Album> RandomAlbums(int limit = 10);
		List<Album> RangeAlbums(char start, char end);
		List<Album> LimitAlbums(int index, int duration = Int32.MinValue);
	}
}
