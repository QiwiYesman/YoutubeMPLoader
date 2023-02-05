using YoutubeExplode;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams; 
using System;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using Xamarin.MP4Transcoder;
namespace MauiApp1
{
    internal class YoutubeExploder
    {
        public Action<double> progress;
        public YoutubeClient youtube;
        
        public YoutubeExploder()
        {
            youtube = new();
            progress = ProgressMethod;
        }
        void ProgressMethod(double value)
        {

        }
        public async Task<StreamManifest> GetManifestAsync(string link)
        {
            return await youtube.Videos.Streams.GetManifestAsync(link);
        }
        public async Task<Video> GetVideoInfoAsync(string link)
        {
            return await youtube.Videos.GetAsync(link);
        }
        public IStreamInfo GetAudioStream(StreamManifest streamManifest)
        {
            return streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();
        }
        public IStreamInfo GetVideoStream(StreamManifest streamManifest, int resolution)
        {
            return streamManifest.GetVideoStreams()
                                .Where(s => s.Container == Container.Mp4)
                                .Where(s => s.VideoResolution.Height == resolution)
                                .First();
        }
        public async Task DownloadStreamAsync(IStreamInfo streamInfo, string path)
        {
            await youtube.Videos.Streams.DownloadAsync(
                streamInfo,
                path,
                new Progress<double>(progress));
        }
        public async Task ConvertToMP3(string inPath, string outPath)
        {
            await Transcoder
                .For720pFormat()
                .ConvertAsync(new(inPath), new(outPath), progress);
        }
        
        public void removeVideo(string file)
        {
            File.Delete(file);
        }
    }
}
