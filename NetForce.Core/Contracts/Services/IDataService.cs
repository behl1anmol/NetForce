using NetForce.Core.Models;

namespace NetForce.Core.Contracts.Services;
public interface IDataService
{
    public HashSet<string> TagsSet { set; get; }

    Task<IEnumerable<TemplateModel>> GetTemplateGridDataAsync(string _templateTable);
}
