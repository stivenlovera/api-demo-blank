
namespace api_guardian.Interfaces
{
    public interface IRazorRendererHelper
    {
        string RenderPartialToString<TModel>(string partialName, TModel model);
    }
}