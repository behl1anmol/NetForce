﻿using System.Text.RegularExpressions;
using NetForce.Core.Contracts.Services;
using NetForce.Core.Models;

namespace NetForce.Core.Services;
public class DataService : IDataService
{
    readonly ICommandService _service;
    private List<TemplateModel> Model { get; } = new List<TemplateModel>();

    public HashSet<string> TagsSet { set; get; } = new HashSet<string>();

    public DataService()
    {
        _service = new CommandService();
    }

    ICommandService Service => _service;
    void GetTemplate(string _templateTable)
    {
        try
        {
            //var _templateTable = Service.ExecuteCommandAsync("dotnet", null, "new", "--list").Result;
            Regex Splitter = new Regex(@"\r\n");
            List<string> _templateRows = Splitter.Split(_templateTable).ToList();
            _templateRows.RemoveRange(0, 4);
            _templateRows.RemoveRange(_templateRows.Count - 2, 2);
            Splitter = new Regex(@"\s\s+");
            _templateRows.ToList().ForEach(_ =>
            {
                List<string> filterdTags = Splitter.Split(_)[3].ToString().Split('/').ToList();
                
                filterdTags.ForEach(_ =>
                {
                    TagsSet.Add(_);
                });
                Model.Add(new TemplateModel()
                {
                    TemplateName = Splitter.Split(_)[0].ToString().Trim(),
                    ShortName = Splitter.Split(_)[1].ToString().Trim(),
                    Language = Splitter.Split(_)[2].ToString().Replace('[', ' ').Replace(']',' ').Trim(),
                    Tags = string.Join(" ",filterdTags).Trim(),
                }); ;
            });
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());

        }
    }

    string CreateTemplate(string type, string proj, string dir)
    {
        try
        {
            var result = Service.ExecuteCommandAsync("dotnet", dir, "new", type, "-n", proj).Result;
            return result;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
    }

    async Task<IEnumerable<TemplateModel>> IDataService.GetTemplateGridDataAsync(string _templateTable)
    {
        Model.Clear();
        GetTemplate(_templateTable);
        await Task.CompletedTask;
        return Model;
    }
}
