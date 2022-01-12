using System;
using Hints.domain;

namespace Hints.data
{
    public class CurrentHintInMemoryRepository: ICurrentHintRepository
    {
        private string hintText = String.Empty;
        
        public void SetHintText(string text) => hintText = text;

        public string GetHintText() => hintText;
    }
}