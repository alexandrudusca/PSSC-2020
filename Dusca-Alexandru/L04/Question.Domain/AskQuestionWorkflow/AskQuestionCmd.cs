using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace L04
{
    // product = Title * Text * Tags
    public struct AskQuestionCmd
    {
        [Required]
        [StringLength(20, ErrorMessage = "{0} length must be between {2} and {1}!", MinimumLength = 5)]
        public string Title { get; private set; }
        [Required]
        [StringLength(1000, ErrorMessage = "{0} length must be between {2} and {1}!", MinimumLength = 20)]
        public string Text { get; set; }
        public List<string> Tags { get; set; }

        public AskQuestionCmd(string title, string text, List<string> tags)
        {
            Title = title;
            Text = text;
            Tags = tags;
        }
    }
}