using AV.Contracts.Models;
using AV.Contracts.Models.Valuation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AV.Contracts.Services
{
    public interface IDocumentStoreService
    {
        Task<DocumentModel> SaveFile(IFormFile documentFile);
        Task<ICollection<DocumentModel>> SaveFiles(ICollection<IFormFile> documents);
        IEnumerable<T> ConvertToInstructionDocument<T>(ICollection<IFormFile> documents) where T : DocumentModel, new();
        DocumentModel GetDocument(Guid documentId);
        IEnumerable<InstructionDocumentModel> GetDocumentByInstructionId(Guid instructionId);
        IEnumerable<CompanyLogoDocumentModel> GetDocumentByAccountId(Guid accountId);

    }
}
