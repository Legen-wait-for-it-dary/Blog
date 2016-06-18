using Blog.Entities;
using System.Collections.Generic;

namespace Blog.DAL
{
    public interface IMediaFileRepository
    {
        List<MediaFile> GetAllMediaFiles();
        MediaFile GetMediaFileById(int mediaFileId);
        void UpdateMediaFile(MediaFile mediaFile);
        void AddMediaFile(MediaFile mediaFile);
        void DeleteMediaFile(int mediaFileId);
        decimal? GetNextIndenitityForMediaFile();
    }
}
