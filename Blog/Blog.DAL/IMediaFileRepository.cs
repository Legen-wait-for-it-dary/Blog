using Blog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DAL
{
    public interface IMediaFileRepository
    {
        List<MediaFile> GetAllMediaFiles();
        MediaFile GetMediaFileById(int id);
        void UpdateMediaFile(MediaFile mediaFile);
        void AddMediaFile(MediaFile mediaFile);
        void DeleteMediaFile(int mediaFileId);
    }
}
