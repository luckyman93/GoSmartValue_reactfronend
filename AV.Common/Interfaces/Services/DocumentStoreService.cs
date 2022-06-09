using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AV.Common.Entities;
using AV.Common.Interfaces.UnitOfWorks;
using AV.Contracts.Models;
using AV.Contracts.Models.Valuation;
using AV.Contracts.Services;
using Microsoft.AspNetCore.Http;

namespace AV.Common.Interfaces.Services
{
    public class DocumentStoreService : IDocumentStoreService
    {
        private readonly IDocumentStoreUnitOfWork _documentUoW;
        private readonly IMapper _mapper;

        public DocumentStoreService(IDocumentStoreUnitOfWork documentUoW, IMapper mapper)
        {
            _documentUoW = documentUoW;
            _mapper = mapper;
        }

        public async Task<DocumentModel> SaveFile(IFormFile documentFile)
        {
            var fileSize = (documentFile.Length / 1024f / 1024f);
            if (fileSize > 2)
                throw new ArgumentOutOfRangeException($"FileSize limit exceeded. File Size :{fileSize}");
            using (var fileStreamToStore = new MemoryStream())
            {
                documentFile.CopyTo(fileStreamToStore);
                var documentStream = new DocumentStream(fileStreamToStore.ToArray(), documentFile.ContentType);

                var document = new Document(
                    documentFile.Name,
                    documentFile.FileName,
                    documentFile.ContentType,
                    documentStream
                );
                return _mapper.Map<DocumentModel>(await _documentUoW.SaveFile(document));
            }
        }

        public async Task<ICollection<DocumentModel>> SaveFiles(ICollection<IFormFile> documents)
        {
            var savedDocuments = new List<DocumentModel>();
            foreach (var file in documents)
            {
                var result = await SaveFile(file);
                savedDocuments.Add(result);
            }

            return savedDocuments;
        }

        public IEnumerable<T> ConvertToInstructionDocument<T>(ICollection<IFormFile> documents) where T : DocumentModel, new()
        {
            if (documents.Count == 0)
            {
                return new List<T>();
            }
            if (documents.Count(d => (d.Length / 1024f / 1024f) > 4) > 0)
                throw new ArgumentOutOfRangeException(
                    $"FileSize limit exceeded. File Size :{documents.Max(d => (d.Length / 1024f / 1024f))}");

            var convertedDocuments = new List<T>();
            foreach (var documentFile in documents)
            {
                using (var fileStreamToStore = new MemoryStream())
                {
                    documentFile.CopyTo(fileStreamToStore);
                    var documentStream = new DocumentStream(fileStreamToStore.ToArray(), documentFile.ContentType);

                    convertedDocuments.Add(new T {
                        Name = documentFile.Name,
                        FileName = documentFile.FileName,
                        ContentType = documentFile.ContentType,
                        DocumentStream = _mapper.Map<DocumentStreamModel>(documentStream)
                    });
                }
            }

            return convertedDocuments;
        }

        public DocumentModel GetDocument(Guid documentId)
        {
            return _mapper.Map<DocumentModel>(_documentUoW.GetDocument(documentId));
        }

        public IEnumerable<InstructionDocumentModel> GetDocumentByInstructionId(Guid instructionId)
        {
            return _mapper.Map<IEnumerable<InstructionDocumentModel>>(_documentUoW.GetDocumentByInstructionId(instructionId));
        }

        public IEnumerable<CompanyLogoDocumentModel> GetDocumentByAccountId(Guid accountId)
        {
            return _mapper.Map<IEnumerable<CompanyLogoDocumentModel>>(_documentUoW.GetLogoDocumentByAccountId(accountId).ToList());
        }

       
    }
}