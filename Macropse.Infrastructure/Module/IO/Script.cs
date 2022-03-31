namespace Macropse.Infrastructure.Module.IO
{
    public class Script
    {
        public string Name { get; private set; }
        public string Content { get; private set; }

        public Script(string name, string content)
        {
            Name = name;
            Content = content;
        }
    }
}
