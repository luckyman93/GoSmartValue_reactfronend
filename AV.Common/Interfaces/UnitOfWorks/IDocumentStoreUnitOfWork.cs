using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AV.Common.Entities;

namespace AV.Common.Interfaces.UnitOfWorks
{
    public interface IDocumentStoreUnitOfWork : IUnitOfWork
    {
        Task<Document> SaveFile(Document document);
        Document GetDocument(Guid documentId);
        IEnumerable<InstructionDocument> GetDocumentByInstructionId(Guid instructionId);
        IEnumerable<CompanyLogoDocument> GetLogoDocumentByAccountId(Guid accountId);
		
	}
}