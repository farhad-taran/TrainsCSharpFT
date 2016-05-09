using Trains.Core.Presentation.Commands;

namespace Trains.Core.Presentation
{

    public class MenuItem
    {
        readonly ICommand command;
        readonly string caption;
        readonly string key;

        public MenuItem(string caption,string key,ICommand command)
        {
            this.key = key;
            this.caption = caption;
            this.command = command;
        }

        public string Key => key;
        public ICommand Command => command;
        public string Caption => caption;
    }

}