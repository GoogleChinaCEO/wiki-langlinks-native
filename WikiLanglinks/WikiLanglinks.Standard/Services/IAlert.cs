using System;

namespace WikiLanglinks
{
    public interface IAlert
    {
        void Short(string message);

        void Long(string message);
    }
}
