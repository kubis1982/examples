using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using OData.Persistance;
using OData.Persistance.Entities;

namespace OData.Controllers;

[ApiController]
[Route("[controller]")]
[EnableQuery(MaxExpansionDepth = 5)]
public class ODataController(WriteDbContext writeDbContext) : ControllerBase
{
    [HttpGet("contractors")]
    [EndpointName($"{nameof(GetContractors)}")]
    [EndpointSummary(nameof(GetContractors))]
    public Task<IQueryable<Contractor>> GetContractors(CancellationToken cancellationToken) => Task.FromResult((IQueryable<Contractor>)writeDbContext.Contractors);

    [HttpGet("documents")]
    [EndpointName($"{nameof(GetDocuments)}")]
    [EndpointSummary(nameof(GetDocuments))]
    public Task<IQueryable<Document>> GetDocuments(CancellationToken cancellationToken) => Task.FromResult((IQueryable<Document>)writeDbContext.Documents);

    [HttpGet("articles")]
    [EndpointName($"{nameof(GetArticles)}")]
    [EndpointSummary(nameof(GetArticles))]
    public Task<IQueryable<Article>> GetArticles(CancellationToken cancellationToken) => Task.FromResult((IQueryable<Article>)writeDbContext.Articles);
}
