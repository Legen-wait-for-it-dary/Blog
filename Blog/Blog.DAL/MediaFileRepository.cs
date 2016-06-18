using Blog.Entities;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;

namespace Blog.DAL
{
    public class MediaFileRepository:IMediaFileRepository
    {
        private readonly string _connectionString;

        public MediaFileRepository() { }

        public MediaFileRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<MediaFile> GetAllMediaFiles()
        {
            using (ObjectContext context = new ObjectContext(_connectionString))
            {
                ObjectSet<MediaFile> mediaFilesObjectSet = context.CreateObjectSet<MediaFile>();

                return mediaFilesObjectSet.ToList();
            }
        }
        
        public MediaFile GetMediaFileById(int mediaFileId)
        {
            using (ObjectContext context = new ObjectContext(_connectionString))
            {
                ObjectSet<MediaFile> mediaFilesObjectSet = context.CreateObjectSet<MediaFile>();

                return mediaFilesObjectSet.FirstOrDefault(mf => mf.MediaFileId == mediaFileId);
            }
        }

        public void UpdateMediaFile(MediaFile mediaFile)
        {
            throw new NotImplementedException();

        }

        public decimal? GetNextIndenitityForMediaFile()
        {
            using (ObjectContext context = new ObjectContext(_connectionString))
            {
                return context.ExecuteStoreQuery<decimal?>("exec GetNextIndenitityForMediaFile").Single();
            }
        }

        

        public void AddMediaFile(MediaFile mediaFile)
        {
            using (ObjectContext context = new ObjectContext(_connectionString))
            {
                
                ObjectSet<MediaFile> mediaFilesObjectSet = context.CreateObjectSet<MediaFile>();
                mediaFilesObjectSet.AddObject(mediaFile);
                context.SaveChanges();
            }
        }

        public void DeleteMediaFile(int mediaFileId)
        {
            using (ObjectContext context = new ObjectContext(_connectionString))
            {
                ObjectSet<MediaFile> mediaFilesObjectSet = context.CreateObjectSet<MediaFile>();
                mediaFilesObjectSet.DeleteObject(mediaFilesObjectSet.Single(mf => mf.MediaFileId == mediaFileId));
                context.SaveChanges();
            }
        }
    }
}

