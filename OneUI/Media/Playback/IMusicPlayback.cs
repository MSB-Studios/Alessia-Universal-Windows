using System.Xml.Serialization;
using Windows.Media.Core;
using Windows.Storage;
using System;
using Windows.Storage.FileProperties;

namespace OneUI.Media.Playback
{
    public interface IMusicPlayback
    {
        public string Title { get; set; }

        public string Artist { get; set; }

        public string Album { get; set; }

        public string Genre { get; set; }

        public string Year { get; set; }

        public TimeSpan Duration { get; set; }

        public string Path { get; set; }

        [XmlIgnore]
        public StorageFile File { get; set; }

        [XmlIgnore]
        public MediaSource MediaSource { get; set; }

        [XmlIgnore]
        public StorageItemThumbnail Thumbnail { get; set; }
    }
}
