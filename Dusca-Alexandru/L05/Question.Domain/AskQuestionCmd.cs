using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Question.Domain.AskQuestionWorkflow
{
    // product = Title * Text * Tags
    public struct AskQuestionCmd
    {
        [Required]
        public string Title { get; private set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public List<string> Tags { get; set; }

        public AskQuestionCmd(string title, string text, List<string> tags)
        {
            Title = title;
            Text = text;
            Tags = tags;
        }
    }
}