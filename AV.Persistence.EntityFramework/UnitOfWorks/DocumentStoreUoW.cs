using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AV.Common.Entities;
using AV.Common.Interfaces.Repositories;
using AV.Common.Interfaces.UnitOfWorks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AV.Persistence.EntityFramework.UnitOfWorks
{
    public class DocumentStoreUoW : UnitOfWork,IDocumentStoreUnitOfWork
    {
        private readonly IDocumentRepository _documentRepository;

        public DocumentStoreUoW(IdentityDbContext<User, Role, Guid> dbContext, IDocumentRepository documentRepository) : base(dbContext)
        {
            _documentRepository = documentRepository;
        }

        public async Task<Document> SaveFile(Document document)
        {
            _documentRepository.Add(document);
            Complete();
            await _dbContext.Entry(document).ReloadAsync();
            return document;

        }

        public Document GetDocument(Guid documentId)
        {
            return _documentRepository.Find(d => d.Id == documentId)
                .Include(d => d.DocumentStream)
                .SingleOrDefault();
        }

        public IEnumerable<InstructionDocument> GetDocumentByInstructionId(Guid instructionId)
        {
            return _dbContext.Set<InstructionDocument>()
                .Where(i => i.InstructionId == instructionId)
                .Include(d => d.DocumentStream)
                .ToList();
        }

        public IEnumerable<CompanyLogoDocument> GetLogoDocumentByAccountId(Guid accountId)
        {
            return _dbContext.Set<CompanyLogoDocument>()
                .Where(i => i.AccountId == accountId)
                .Include(d => d.DocumentStream)
                .ToList();
        }

       
    }
}