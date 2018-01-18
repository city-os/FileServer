﻿using CityOs.FileServer.Domain.Entities;
using System.IO;
using System.Threading.Tasks;

namespace CityOs.FileServer.Domain.Contracts
{
    public interface IDocumentRepository
    {
        /// <summary>
        /// Gets the file stream function of its identifier
        /// </summary>
        /// <param name="fileIdentifier">The file identifier</param>
        /// <returns></returns>
        Task<Stream> GetFileStreamByIdentifierAsync(string fileIdentifier);

        /// <summary>
        /// Save the image asynchronously
        /// </summary>
        /// <param name="fileInformation">The file information to use</param>
        /// <returns></returns>
        Task<string> SaveImageAsync(FileInformation fileInformation);
    }
}
