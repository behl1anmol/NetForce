using NetForce.Core.Models;

namespace NetForce.Core.Contracts.Services;
public interface IDataService
{
    Task<IEnumerable<TemplateModel>> GetTemplateGridDataAsync(string _templateTable);
}
