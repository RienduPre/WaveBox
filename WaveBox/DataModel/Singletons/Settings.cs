﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WaveBox.DataModel.Model;
using Newtonsoft.Json;
using System.IO;

namespace WaveBox.DataModel.Singletons
{
	public class Settings
	{
		private static Settings instance;
		public static string SETTINGS_PATH = "res/WaveBox.conf";

		private double _version = 1.0;
		public double Version
		{
			get
			{
				return _version;
			}
		}

		private static List<Folder> _mediaFolders;
		public static List<Folder> MediaFolders
		{
			get
			{
				return _mediaFolders;
			}
		}

		private Settings()
		{
			_settingsSetup();
		}

		public static Settings Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new Settings();
				}

				return instance;
			}
		}

		public static void reload()
		{
			_parseSettings();
		}

		private static void _parseSettings()
		{
			_mediaFolders = _populateMediaFolders();
		}

		private static void _settingsSetup()
		{
			if(!File.Exists("WaveBox.conf"))
			{
				try
				{
					Console.WriteLine("[SETTINGS] " + "Setting file doesn't exist; Creating it. (WaveBox.conf)");
					var settingsTemplate = new StreamReader("res/WaveBox.conf");
					var settingsOut = new StreamWriter("WaveBox.conf");

					settingsOut.Write(settingsTemplate.ReadToEnd());

					settingsTemplate.Close();
					settingsOut.Close();
				}

				catch (Exception e)
				{
					Console.WriteLine("[SETTINGS] " + e.ToString());
				}
			}

			reload();
		}

		private static List<Folder> _populateMediaFolders()
		{
			var folders = new List<Folder>();
			Folder mf = null;
			StreamReader reader = new StreamReader("WaveBox.conf");
			string configFile = reader.ReadToEnd();

			dynamic json = JsonConvert.DeserializeObject(configFile);

			Console.WriteLine(json.mediaFolders + "\r\n");

			for (int i = 0; i < json.mediaFolders.Count; i++)
			{
				mf = new Folder(json.mediaFolders[i].ToString(), true);
				if (mf.FolderId == 0)
				{
					mf.addToDatabase(true);
				}
				folders.Add(mf);
			}



			return folders;
		}
		
	}
}