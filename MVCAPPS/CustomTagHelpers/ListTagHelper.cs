using Coditas.ECommerce.DataAccess;
using Coditas.ECommerce.Entities;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text.Json;
namespace MVCAPPS.CustomTagHelpers
{
    public class ListGenerator: TagHelper
    {
        public IEnumerable<Object> objects1 { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            // Custom Tag Name
            output.TagName = "List";
            output.TagMode = TagMode.StartTagAndEndTag;
            var table = "<table clas='table table-bordered table-striped table-dark'>";
            foreach(var item in objects1)
            {
                Type type = item.GetType();
                //var data = type.GetProperties();
                //foreach (var v in data)
                //{
                //    table += $"<tr><td>{v.GetValue(data)}<td></tr>";
                //}
               
                    table += $"<tr>";
                foreach (var v in type.GetProperties())
                {
                    if (v.PropertyType != typeof(HashSet<>))
                    {
                        table += $"<td>{v.GetValue(item)}</td>";
                    }

                }
                    table += $"</tr>";
                
            }
            table += "</table>";

             output.PreContent.SetHtmlContent(table);
        }
    }
}
