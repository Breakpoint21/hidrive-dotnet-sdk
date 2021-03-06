using System;
using System.Collections.Generic;
using Kyrodan.HiDrive.Models;

namespace Kyrodan.HiDrive.Requests
{
    internal class FileRequestBuilder : BaseRequestBuilder, IFileRequestBuilder
    {
        public FileRequestBuilder(string requestUrl, IBaseClient client)
            : base(requestUrl, client)
        {
        }

        public IReceiveStreamRequest Download(string path = null, string pid = null, string snapshot = null)
        {
            var request = new ReceiveStreamRequest(this.RequestUrl, this.Client);

            if (path != null) request.QueryOptions.Add(new KeyValuePair<string, string>("path", Uri.EscapeDataString(path)));
            if (pid != null) request.QueryOptions.Add(new KeyValuePair<string, string>("pid", pid));
            if (snapshot != null) request.QueryOptions.Add(new KeyValuePair<string, string>("snapshot", snapshot));

            return request;
        }

        public ISendStreamRequest<FileItem> Upload(string name, string dir = null, string dir_id = null, UploadMode mode = UploadMode.CreateOnly)
        {
            var request = new SendStreamRequest<FileItem>(this.RequestUrl, this.Client);

            switch (mode)
            {
                case UploadMode.CreateOnly:
                    request.Method = "POST";
                    break;
                case UploadMode.CreateOrUpdate:
                    request.Method = "PUT";
                    break;
                default:
                    throw new NotImplementedException();
            }

            request.QueryOptions.Add(new KeyValuePair<string, string>("name", Uri.EscapeDataString(name)));
            if (dir != null) request.QueryOptions.Add(new KeyValuePair<string, string>("dir", Uri.EscapeDataString(dir)));
            if (dir_id != null) request.QueryOptions.Add(new KeyValuePair<string, string>("dir_id", dir_id));

            return request;
        }

        public IRequest Delete(string path = null, string pid = null)
        {
            var request = new Request(this.RequestUrl, this.Client)
            {
                Method = "DELETE"
            };

            if (path != null) request.QueryOptions.Add(new KeyValuePair<string, string>("path", Uri.EscapeDataString(path)));
            if (pid != null) request.QueryOptions.Add(new KeyValuePair<string, string>("pid", pid));

            return request;
        }

        public IRequest<FileItem> Copy(string sourcePath = null, string sourceId = null, string destPath = null, string destId = null,
            string snapshot = null)
        {
            var request = new Request<FileItem>(this.AppendSegmentToRequestUrl("copy"), this.Client)
            {
                Method = "POST"
            };

            if (sourcePath != null) request.QueryOptions.Add(new KeyValuePair<string, string>("src", Uri.EscapeDataString(sourcePath)));
            if (sourceId != null) request.QueryOptions.Add(new KeyValuePair<string, string>("src_id", sourceId));
            if (destPath != null) request.QueryOptions.Add(new KeyValuePair<string, string>("dst", Uri.EscapeDataString(destPath)));
            if (destId != null) request.QueryOptions.Add(new KeyValuePair<string, string>("dst_id", destId));
            if (snapshot != null) request.QueryOptions.Add(new KeyValuePair<string, string>("snapshot", snapshot));

            return request;
        }

        public IRequest<FileItem> Rename(string path = null, string pid = null, string name = null)
        {
            var request = new Request<FileItem>(this.AppendSegmentToRequestUrl("rename"), this.Client)
            {
                Method = "POST"
            };

            if (path != null) request.QueryOptions.Add(new KeyValuePair<string, string>("path", Uri.EscapeDataString(path)));
            if (pid != null) request.QueryOptions.Add(new KeyValuePair<string, string>("pid", pid));
            if (name != null) request.QueryOptions.Add(new KeyValuePair<string, string>("name", Uri.EscapeDataString(name)));

            return request;
        }

        public IRequest<FileItem> Move(string sourcePath = null, string sourceId = null, string destPath = null, string destId = null)
        {
            var request = new Request<FileItem>(this.AppendSegmentToRequestUrl("move"), this.Client)
            {
                Method = "POST"
            };

            if (sourcePath != null) request.QueryOptions.Add(new KeyValuePair<string, string>("src", Uri.EscapeDataString(sourcePath)));
            if (sourceId != null) request.QueryOptions.Add(new KeyValuePair<string, string>("src_id", sourceId));
            if (destPath != null) request.QueryOptions.Add(new KeyValuePair<string, string>("dst", Uri.EscapeDataString(destPath)));
            if (destId != null) request.QueryOptions.Add(new KeyValuePair<string, string>("dst_id", destId));

            return request;

        }
    }
}